/*
  Copyrights reserved
  Written by Paul Hwang
*/
function PhwangAjaxClass (phwang_object_val) {
    "use strict";
    this.init__ = phwang_object_val => {
        this.thePhwangObject = phwang_object_val;
        this.thePhwangAjaxProtocolObject = new PhwangAjaxProtocolClass();
        this.theTransmitQueueObject = new PhwangQueueClass(this.phwangObject());
        this.thePendingSessionDataQueueObject = new PhwangQueueClass(this.phwangObject());
        this.thePhwangAjaxEngineObject = new PhwangAjaxEngineClass(this);
        this.initSwitchTable();
        this.clearPendingAjaxRequestCommand();
        this.theAjaxPacketId = 0;
        this.debug(false, "init__", "");
    };
    this.parseAndSwitchAjaxResponse = json_response_val => {
        this.debug(true, "parseAndSwitchAjaxResponse", "json_response_val=" + json_response_val);
        var response = JSON.parse(json_response_val);
        if (response.command !== this.phwangAjaxProtocolObject().GET_LINK_DATA_COMMAND()) {
            this.debug(true, "parseAndSwitchAjaxResponse", "command=" + response.command + " data=" + response.data);
        }
        if (response.command !== this.pendingAjaxRequestCommand()) {
            this.abend("parseAndSwitchAjaxResponse", "commands not match: " + this.pendingAjaxRequestCommand() + ", " + response.command);
            return;
        }
        var data = JSON.parse(response.data);
        if (!data) {
            return;
        }
        if ((response.command !== this.phwangAjaxProtocolObject().SETUP_LINK_COMMAND()) && (!this.linkObject().verifyLinkIdIndex(data.link_id))) {
            this.abend("parseAndSwitchAjaxResponse", "command=" + response.command + " link_id=" + data.link_id + " linkId=" + this.linkObject().linkId());
            return;
        }
        var func = this.switchTable()[response.command];
        if (!func) {
            this.abend("parseAndSwitchAjaxResponse", "bad command=" + response.command);
            return;
        }
        this.clearPendingAjaxRequestCommand();
        func.bind(this)(response.data);
    };
    this.initSwitchTable = () => {
        this.theSwitchTable = {
            "setup_link": this.setupLinkResponse,
            "get_link_data": this.getLinkDataResponse,
            "get_name_list": this.getNameListResponse,
            "setup_session": this.setupSessionResponse,
            "setup_session2": this.setupSession2Response,
            "setup_session3": this.setupSession3Response,
            "get_session_data": this.getSessionDataResponse,
            "put_session_data": this.putSessionDataResponse,
        };
    };
    this.setupLink = (link_val, password_val) => {
        var output = JSON.stringify({
            command: this.phwangAjaxProtocolObject().SETUP_LINK_COMMAND(),
            packet_id: this.ajaxPacketId(),
            data: JSON.stringify({
                my_name: link_val.myName(),
                password: password_val,
            }),
        });
        this.debug(true, "setupLink", "output=" + output);
        this.transmitAjaxRequest(output);
    };
    this.setupLinkResponse = input_val => {
        this.debug(true, "setupLinkResponse", "input_val=" + input_val);
        var data = JSON.parse(input_val);
        this.linkObject().setLinkId(data.link_id);
        this.phwangPortObject().receiveSetupLinkResponse();
    };
    this.getLinkData = link_val => {
        var output = JSON.stringify({
            command: this.phwangAjaxProtocolObject().GET_LINK_DATA_COMMAND(),
            packet_id: this.ajaxPacketId(),
            data: JSON.stringify({
                link_id: link_val.linkId(),
            }),
        });
        this.debug(false, "getLinkData", "output=" + output);
        this.transmitAjaxRequest(output);
    };
    this.getLinkDataResponse = input_val => {
        this.debug(false, "getLinkDataResponse", "input_val=" + input_val);
        var input = JSON.parse(input_val);
        if (input) {
            if (input.data) {
                var data = input.data;
                while (data.length > 0) {
                    if (data.charAt(0) === this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_RESPOND_IS_GET_LINK_DATA_NAME_LIST()) {
                        data = data.slice(1);
                        var name_list_tag  = this.phwangObject().decodeNumber(data, this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_NAME_LIST_TAG_SIZE());
                        if (name_list_tag > this.linkObject().nameListTag()) {
                            this.linkObject().setServerNameListTag(name_list_tag);
                        }
                        data = data.slice(this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_NAME_LIST_TAG_SIZE());
                        continue;
                    }
                    if (data.charAt(0) === this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_RESPOND_IS_GET_LINK_DATA_PENDING_SESSION()) {
                        this.debug(true, "getLinkDataResponse", "pending_session_data=" + data);
                        data = data.slice(1);
                        var link_id = data.slice(0, this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_LINK_ID_SIZE());
                        this.debug(false, "getLinkDataResponse", "link_id=" + link_id);
                        data = data.slice(this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_LINK_ID_SIZE());
                        var session_id = data.slice(0, this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_SESSION_ID_SIZE());
                        this.debug(false, "getLinkDataResponse", "session_id=" + session_id);
                        data = data.slice(this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_SESSION_ID_SIZE());
                        var config_len = this.phwangObject().decodeNumber(data.slice(1), 3);
                        var theme_data = data.slice(0, config_len);
                        this.debug(true, "getLinkDataResponse", "theme_data=" + theme_data);
                        var theme = this.themeMgrObject().mallocThemeAndInsert();
                        theme.configObject().decodeConfig(theme_data);
                        data = data.slice(config_len);
                        this.setupSession2(this.linkObject(), theme_data, session_id, theme.themeIdStr());
                        continue;
                    }
                    if (data.charAt(0) === this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_RESPOND_IS_GET_LINK_DATA_PENDING_SESSION3()) {
                        this.debug(true, "getLinkDataResponse", "pending_session_data3=" + data);
                        data = data.slice(1);
                        var theme_id_str = data.slice(0, this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_THEME_ID_SIZE());
                        data = data.slice(this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_THEME_ID_SIZE());
                        var session_id = data.slice(0, this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_SESSION_ID_SIZE());
                        this.debug(true, "getLinkDataResponse", "session_id=" + session_id);
                        data = data.slice(this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_SESSION_ID_SIZE());
                        this.setupSession3(this.linkObject(), session_id);
                        continue;
                    }
                    if (data.charAt(0) === this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_RESPOND_IS_GET_LINK_DATA_PENDING_DATA()) {
                        var link_session_id = data.slice(1, this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_LINK_SESSION_ID_SIZE() + 1);
                        this.debug(true, "getLinkDataResponse", "link_session_id=" + link_session_id);
                        this.pendingSessionDataQueueObject().enqueueData(link_session_id);
                        data = data.slice(this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_LINK_SESSION_ID_SIZE() + 1);
                        continue;
                    }
                    this.abend("getLinkDataResponse", "not supported command: " + data);
                    data = data.slice(data.length);
                }
                if (data.length !== 0) {
                    this.abend("getLinkDataResponse", "length=" + data.length);
                }
            }
        }
    };
    this.getNameList = link_val => {
        var output = JSON.stringify({
            command: this.phwangAjaxProtocolObject().GET_NAME_LIST_COMMAND(),
            packet_id: this.ajaxPacketId(),
            data: JSON.stringify({
                link_id: link_val.linkId(),
                name_list_tag: link_val.nameListTag(),
            }),
        });
        this.debug(true, "getNameList", "output=" + output);
        this.transmitAjaxRequest(output);
    };
    this.getNameListResponse = input_val => {
        this.debug(true, "getNameListResponse", "input_val=" + input_val);
        var data = JSON.parse(input_val);
        if (data) {
            if (data.c_name_list) {
                var name_list_tag  = this.phwangObject().decodeNumber(data.c_name_list, 3);
                this.linkObject().setNameListTag(name_list_tag);
                var name_list = data.c_name_list.slice(3);
                this.debug(true, "getNameListResponse", "name_list_tag=" + name_list_tag);
                this.debug(true, "getNameListResponse", "name_list=" + name_list);
                var array = JSON.parse("[" + name_list + "]");
                this.debug(true, "getNameListResponse", "array=" + array);
                this.linkObject().setNameList(array);
                this.preludeRenderObject().renderNameList();
                this.phwangPortObject().receiveGetNameListResponse();
            }
        }
    };
    this.setupSession = (link_val, his_name_val, theme_data_val) => {
        var output = JSON.stringify({
            command: this.phwangAjaxProtocolObject().SETUP_SESSION_COMMAND(),
            packet_id: this.ajaxPacketId(),
            data: JSON.stringify({
                link_id: link_val.linkId(),
                his_name: his_name_val,
                theme_data: theme_data_val,
            }),
        });
        this.debug(true, "setupSession", "output=" + output);
        this.transmitAjaxRequest(output);
    };
    this.setupSessionResponse = input_val => {
        this.debug(true, "setupSessionResponse", "input_val=" + input_val);
        var data = JSON.parse(input_val);
        if (data) {
            this.phwangPortObject().receiveSetupSessionResponse(data.result);
        }
    };
    this.setupSession2 = (link_val, theme_data_val, session_id_val, theme_id_str_val) => {
        var output = JSON.stringify({
            command: this.phwangAjaxProtocolObject().SETUP_SESSION2_COMMAND(),
            packet_id: this.ajaxPacketId(),
            data: JSON.stringify({
                link_id: link_val.linkId(),
                accept: "yes",
                session_id: session_id_val,
                theme_data: theme_data_val,
                theme_id_str: theme_id_str_val
            }),
        });
        this.debug(true, "setupSession2", "output=" + output);
        this.transmitAjaxRequest(output);
    };
    this.setupSession2Response = json_data_val => {
        this.debug(true, "setupSession2Response", "data=" + json_data_val);
        var data = JSON.parse(json_data_val);
        if (!data) {
            this.abend("setupSession2Response", "null data");
            return;
        }
        this.debug(true, "setupSession2Response", "link_id=" + data.link_id + " session_id=" + data.session_id + " theme_id_str=" + data.theme_id_str);
        this.phwangPortObject().receiveSetupSession2Response();
    };
    this.setupSession3 = (link_val, session_id_val) => {
        var output = JSON.stringify({
            command: this.phwangAjaxProtocolObject().SETUP_SESSION3_COMMAND(),
            packet_id: this.ajaxPacketId(),
            data: JSON.stringify({
                link_id: link_val.linkId(),
                session_id: session_id_val,
            }),
        });
        this.debug(true, "setupSession3", "output=" + output);
        this.transmitAjaxRequest(output);
    };
    this.setupSession3Response = json_data_val => {
        this.debug(true, "setupSession3Response", "data=" + json_data_val);
        var data = JSON.parse(json_data_val);
        if (data) {
            var session = this.linkObject().mallocSessionAndInsert(data.session_id);
            this.debug(true, "setupSession3Response", "sessionId=" + session.sessionId());
            this.phwangPortObject().receiveSetupSession3Response();
            this.rootObject().preludeRenderObject().renderGoGamePage(data.theme_id);
            var theme = this.themeMgrObject().getTheme(data.theme_id);
            theme.bindSession(session);
        }
    };
    this.putSessionData = (session_val, data_val) => {
        var output = JSON.stringify({
            command: this.phwangAjaxProtocolObject().PUT_SESSION_DATA_COMMAND(),
            packet_id: this.ajaxPacketId(),
            data: JSON.stringify({
                link_id: session_val.linkObject().linkId(),
                session_id: session_val.sessionId(),
                xmt_seq: session_val.xmtSeq(),
                data: data_val,
            }),
        });
        session_val.incrementXmtSeq();
        this.debug(true, "putSessionData", "output=" + output);
        this.transmitAjaxRequest(output);
    };
    this.putSessionDataResponse = json_data_val => {
        this.debug(false, "putSessionDataResponse", "data=" + json_data_val);
        var data = JSON.parse(json_data_val);
    };
    this.getSessionData = function(link_session_id_val) {
        var output = JSON.stringify({
            command: this.phwangAjaxProtocolObject().GET_SESSION_DATA_COMMAND(),
            packet_id: this.ajaxPacketId(),
            data: JSON.stringify({
                link_id: link_session_id_val.slice(0, this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_LINK_ID_SIZE()),
                session_id: link_session_id_val.slice(this.phwangAjaxProtocolObject().WEB_FABRIC_PROTOCOL_LINK_ID_SIZE()),
            }),
        });
        this.debug(false, "getSessionData", "output=" + output);
        this.transmitAjaxRequest(output);
    };
    this.getSessionDataResponse = json_data_val => {
        this.debug(true, "getSessionDataResponse", "data=" + json_data_val);
        var data = JSON.parse(json_data_val);
        if (data) {
            var session = this.linkObject().getSession(data.session_id);
            this.debug(true, "getSessionDataResponse", "session=" + session + "session_id=" + session.sessionId());
            if (session) {
                session.receiveData(data.c_data);
            }
            else {
                this.debug(true, "getSessionDataResponse", "null session");
            }
        }
    };
    this.transmitAjaxRequest = output_val => {
        if (this.pendingAjaxRequestCommandExist()) {
            this.transmitQueueObject().enqueueData(output_val);
            return;
        }
        this.xmtAjaxRequest(output_val);
    };
    this.xmtAjaxRequest = output_val => {
        var output = JSON.parse(output_val);
        if (output.command !== this.phwangAjaxProtocolObject().GET_LINK_DATA_COMMAND()) {
            this.debug(true, "xmtAjaxRequest", "output=" + output_val);
        }
        this.setPendingAjaxRequestCommand(output.command);
        this.phwangAjaxEngineObject().sendAjaxRequest(output_val);
    };
    this.startWatchDog = link_val => {
        setInterval(function (link_val) {
            var ajax_object = link_val.phwangAjaxObject();
            if (ajax_object.pendingAjaxRequestCommandExist()) {
                if (ajax_object.pendingAjaxRequestCommand() !== ajax_object.phwangAjaxProtocolObject().GET_LINK_DATA_COMMAND()) {
                    link_val.debug(false, "PhwangAjaxClassWatchDog", ajax_object.pendingAjaxRequestCommand() + " is pending");
                }
                return;
            }
            var output = ajax_object.transmitQueueObject().dequeueData();
            if (output) {
                ajax_object.xmtAjaxRequest(output);
                return;
            }
            var link_session_id = ajax_object.pendingSessionDataQueueObject().dequeueData();
            if (link_session_id) {
                ajax_object.getSessionData(link_session_id);
                return;
            }
            if (link_val.serverNameListTag() > link_val.nameListTag()) {
                ajax_object.getNameList(link_val);
                return;
            }
            ajax_object.getLinkData(link_val);
        }, 100, link_val);
    };
    this.pendingAjaxRequestCommand = () => this.thePendingAjaxRequestCommand;
    this.pendingAjaxRequestCommandExist = () => (this.pendingAjaxRequestCommand() !== "");
    this.clearPendingAjaxRequestCommand = () => {this.thePendingAjaxRequestCommand = "";};
    this.setPendingAjaxRequestCommand = command_val => {if (this.pendingAjaxRequestCommand()) {this.abend("setPendingAjaxRequestCommand", "old=" + this.pendingAjaxRequestCommand() + "new=" + command_val);} this.thePendingAjaxRequestCommand = command_val;};
    this.switchTable = () => this.theSwitchTable;
    this.objectName = () => "PhwangAjaxClass";
    this.phwangAjaxProtocolObject = () => this.thePhwangAjaxProtocolObject;
    this.phwangAjaxEngineObject = () => this.thePhwangAjaxEngineObject;
    this.transmitQueueObject = () => this.theTransmitQueueObject;
    this.pendingSessionDataQueueObject = () => this.thePendingSessionDataQueueObject;
    this.phwangObject = () => this.thePhwangObject;
    this.rootObject = () => this.phwangObject().rootObject();
    this.linkObject = () => this.phwangObject().linkObject();
    this.phwangPortObject = () => this.phwangObject().phwangPortObject();
    this.themeMgrObject = () => this.rootObject().themeMgrObject();
    this.preludeRenderObject = () => this.rootObject().preludeRenderObject();
    this.debug = (debug_val, str1_val, str2_val) => { if (debug_val) {this.logit(str1_val, str2_val);}};
    this.logit = (str1_val, str2_val) => { this.phwangObject().LOG_IT(this.objectName() + "." + str1_val, str2_val);};
    this.abend = (str1_val, str2_val) => { this.phwangObject().ABEND(this.objectName() + "." + str1_val, str2_val);};
    this.ajaxPacketId = () => "" + this.theAjaxPacketId;
    this.incrementAjaxPacketId = () => { this.theAjaxPacketId++; };
    this.init__(phwang_object_val);
}
function PhwangAjaxEngineClass (phwang_ajax_object_val) {
    "use strict";
    this.ajaxRoute = () => "/Ajax/AjaxGetRequest/";
    this.jsonContext = () => "application/json; charset=utf-8";
    this.plainTextContext = () => "text/plain; charset=utf-8";
    this.init__ = phwang_ajax_object_val => {
        this.thePhwangAjaxObject = phwang_ajax_object_val;
        this.theHttpGetRequest = new XMLHttpRequest();
        this.startAjaxResponseProcess();
        this.debug(false, "init__", "");
    };
    this.startAjaxResponseProcess = () => {
        var this0 = this;
        this.httpGetRequest().onreadystatechange = () => {
            if ((this0.httpGetRequest().readyState === 4) &&
                (this0.httpGetRequest().status === 200)) {
                this0.phwangAjaxObject().parseAndSwitchAjaxResponse(this0.httpGetRequest().responseText);
            }
        };
    };
    this.sendAjaxRequest = output_val => {
        this.httpGetRequest().open("GET", this.ajaxRoute(), true);
        this.httpGetRequest().setRequestHeader("X-Requested-With", "XMLHttpRequest");
        this.httpGetRequest().setRequestHeader("Content-Type", this.jsonContext());
        this.httpGetRequest().setRequestHeader("phwangajaxrequest", output_val);
        this.httpGetRequest().setRequestHeader("phwangajaxpacketid", this.ajaxPacketId());
        this.incrementAjaxPacketId();
        this.httpGetRequest().send(null);
    };
    this.objectName = () => "PhwangAjaxEngineClass";
    this.phwangAjaxObject = () => this.thePhwangAjaxObject;
    this.httpGetRequest = () => this.theHttpGetRequest;
    this.phwangObject = () => this.phwangAjaxObject().phwangObject();
    this.debug = (debug_val, str1_val, str2_val) => { if (debug_val) {this.logit(str1_val, str2_val);}};
    this.logit = (str1_val, str2_val) => { this.phwangObject().LOG_IT(this.objectName() + "." + str1_val, str2_val);};
    this.abend = (str1_val, str2_val) => { this.phwangObject().ABEND(this.objectName() + "." + str1_val, str2_val);};
    this.ajaxPacketId = () => this.phwangAjaxObject().ajaxPacketId();
    this.incrementAjaxPacketId = () => {this.phwangAjaxObject().incrementAjaxPacketId();};
    this.init__(phwang_ajax_object_val);
}
function PhwangAjaxProtocolClass () {
    "use strict";
    this.SETUP_LINK_COMMAND = () => "setup_link";
    this.CLEAR_LINK_COMMAND = () => "clear_link";
    this.GET_LINK_DATA_COMMAND = () => "get_link_data";
    this.GET_NAME_LIST_COMMAND = () => "get_name_list";
    this.SETUP_SESSION_COMMAND = () => "setup_session";
    this.CLEAR_SESSION_COMMAND = () => "clear_session";
    this.SETUP_SESSION2_COMMAND = () => "setup_session2";
    this.SETUP_SESSION3_COMMAND = () => "setup_session3";
    this.PUT_SESSION_DATA_COMMAND = () => "put_session_data";
    this.GET_SESSION_DATA_COMMAND = () => "get_session_data";
    this.WEB_FABRIC_PROTOCOL_RESPOND_IS_GET_LINK_DATA_PENDING_SESSION = () => 'S';
    this.WEB_FABRIC_PROTOCOL_RESPOND_IS_GET_LINK_DATA_PENDING_SESSION3 = () => 'T';
    this.WEB_FABRIC_PROTOCOL_RESPOND_IS_GET_LINK_DATA_PENDING_DATA = () => 'D';
    this.WEB_FABRIC_PROTOCOL_RESPOND_IS_GET_LINK_DATA_NAME_LIST = () => 'N';
    this.WEB_FABRIC_PROTOCOL_NAME_LIST_TAG_SIZE = () => 3;
    this.WEB_FABRIC_PROTOCOL_THEME_ID_SIZE = () => 4;
    this.WEB_FABRIC_PROTOCOL_LINK_ID_SIZE = () => 4;
    this.WEB_FABRIC_PROTOCOL_SESSION_ID_SIZE = () => 4;
    this.WEB_FABRIC_PROTOCOL_LINK_SESSION_ID_SIZE = () => this.WEB_FABRIC_PROTOCOL_LINK_ID_SIZE() + this.WEB_FABRIC_PROTOCOL_SESSION_ID_SIZE();
}

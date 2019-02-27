/*
 * Copyrights phwang
 * Written by Paul Hwang
 */
function LinkClass (phwang_object_val) {
    "use strict";
    this.init__ = phwang_object_val => {
        this.thePhwangObject = phwang_object_val;
        this.theNameList = [];
        this.theNameListTag = 0;
        this.theServerNameListTag = 0;
        this.theSessionIndexArray = [0];
        this.theSessionTableArray = [null];
        this.debug(true, "init__", "linkId=" + this.linkId() + " myName=" + this.myName());
    };
    this.resetLinkStorage = () => {
        this.resetLinkId();
        this.resetMyName();
        this.resetPassWord();
    };
    this.mallocSessionAndInsert = session_id_val => {
        if (this.getSession(session_id_val)) {
            this.abend("mallocSessionAndInsert", "session exists: " + session_id_val);
        }
        var session = new PhwangSessionClass(this);
        session.setSessionId(session_id_val);
        this.insertSession(session);
        return session;
    };
    this.insertSession = session_val => {
        this.sessionIndexArray().push(session_val.sessionId());
        this.sessionTableArray().push(session_val);
    };
    this.getSession = session_id_val => {
        this.debug(true, "getSession", "session_id=" + session_id_val);
        var index = this.sessionIndexArray().indexOf(session_id_val);
        if (index === -1) {
            return null;
        } else {
            var session =this.sessionTableArray()[index];
            return session;
        }
    };
    this.myName = () => this.theMyName;
    this.setMyName = val => { this.theMyName = val; };
    this.resetMyName = () => this.setMyName("");
    this.resetPassWord = () => this.setPassWord("");
    this.linkId = () => this.theLinkId;
    this.setLinkId = val => { this.theLinkId = val; };
    this.resetLinkId = () => { this.setLinkId(""); };
    this.sessionIndexArray = () => this.theSessionIndexArray;
    this.sessionTableArray = () => this.theSessionTableArray;
    this.sessionTableArrayLength = () => this.sessionTableArray().length;
    this.sessionTableArrayElement = val => this.sessionTableArray()[val];
    this.verifyLinkIdIndex = id_val => { if (this.linkId() === id_val) return true; else return false; };
    this.nameListTag = () => this.theNameListTag;
    this.setNameListTag = val => {this.theNameListTag = val;};
    this.serverNameListTag = () => this.theServerNameListTag;
    this.setServerNameListTag = val => {this.theServerNameListTag = val;};
    this.nameList = () => this.theNameList;
    this.setNameList = data_val => {this.theNameList = data_val.sort()};
    this.nameListLength = () => this.nameList().length;
    this.nameListElement = index_val => this.nameList()[index_val];
    this.setNameListElement = (index_val, data_val) => { this.nameList()[index_val] = data_val;};
    this.objectName = () => "LinkClass";
    this.phwangObject = () => this.thePhwangObject;
    this.phwangAjaxObject = () => this.phwangObject().phwangAjaxObject();
    this.debug = (debug_val, str1_val, str2_val) => { if (debug_val) {this.logit(str1_val, str2_val);}};
    this.logit = (str1_val, str2_val) => this.phwangObject().LOG_IT(this.objectName() + "." + str1_val, str2_val);
    this.abend = (str1_val, str2_val)=> { this.phwangObject().ABEND(this.objectName() + "." + str1_val, str2_val);};
    this.init__(phwang_object_val);
}

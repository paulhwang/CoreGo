/*
  Copyrights reserved
  Written by Paul Hwang
*/

function GoConfigRootObject() {
    "use strict";
    this.init__ = function() {
        this.thePhwangObject = new PhwangClass(this);
        this.phwangObject().initObject();
        this.phwangAjaxObject().startWatchDog(this.phwangLinkObject());
        this.theConfigStorageObject = new GoConfigStorageObject(this);
        this.theConfigObject = new GoConfigObject(this);
        this.theAjaxObject = new GoConfigAjaxClass(this);
        this.theHtmlObject = new GoConfigHtmlObject(this);
        this.debug(true, "init__", "myName=" + this.phwangLinkObject().myName() + " linkId=" + this.phwangLinkObject().linkId());
    };
    this.objectName = function () { return "GoConfigRootObject";};
    this.phwangObject = function() {return this.thePhwangObject;};
    this.phwangAjaxObject = function() {return this.phwangObject().phwangAjaxObject();};
    this.phwangLinkObject = function() {return this.phwangObject().phwangLinkObject();};
    this.phwangSessionObject = function() {return this.phwangObject().phwangSessionObject();};
    this.configStorageObject = function() {return this.theConfigStorageObject;};
    this.configObject = function() {return this.theConfigObject;};
    this.ajaxObject = function() {return this.theAjaxObject;};
    this.htmlObject = function() {return this.theHtmlObject;};
    this.debug = function(debug_val, str1_val, str2_val) {if (debug_val) {this.logit(str1_val, str2_val);}};
    this.logit = function(str1_val, str2_val) {return this.logit_(this.objectName() + "." + str1_val, str2_val);};
    this.abend = function(str1_val, str2_val) {return this.abend_(this.objectName() + "." + str1_val, str2_val);};
    this.logit_ = function(str1_val, str2_val) {this.phwangObject().LOG_IT(str1_val, str2_val);};
    this.abend_ = function(str1_val, str2_val) {this.phwangObject().ABEND(str1_val, str2_val);};
    this.init__();
}
function GoConfigHtmlObject(root_object_val) {
    "use strict";
    this.componentName = function () { return "go_config_html"; };
    this.gotoNextPage = function () { window.open(this.phwangObject().serverHttpHeader() + "Go/GoPlay", "_self") };
    this.init__ = function(root_object_val) {
        this.theRootObject = root_object_val;
        this.setupHtmlInputFunction();
        this.debug(true, "init__", "");
    };
    this.setupHtmlInputFunction = function() {
        ReactDOM.render(React.createElement(GoConfigComponentClass, null, null), document.getElementById(this.componentName()));
        this.renderNameList();
        var this0 = this;
        $(".config_section .config_button").on("click", function() {
            this0.configStorageObject().setHisName($(".peer_name_paragraph select").val());
            this0.configStorageObject().setMyColor($(".config_section .go_config_section .stone_color").val());
            this0.configStorageObject().setBoardSize($(".config_section .go_config_section .board_size").val());
            this0.configStorageObject().setKomiPoint($(".config_section .go_config_section .komi").val());
            this0.configStorageObject().setHandicapPoint($(".config_section .go_config_section .handicap").val());
            var encoded_config = this0.configStorageObject().encodeConfig(this0.phwangLinkObject().myName());
            this0.debug(true, "setupHtmlInput", "boardSize=" + this0.configStorageObject().boardSize() + " myColor=" + this0.configStorageObject().myColor() + " komi=" + this0.configStorageObject().komiPoint() + " handicap=" + this0.configStorageObject().handicapPoint());
            this0.phwangAjaxObject().setupSession(this0.phwangLinkObject(), this0.configStorageObject().hisName(), encoded_config);
        });
    };
    this.renderNameList = function() {
        for (var i = 0; i < this.phwangLinkObject().nameListLength(); i++) {
            $('.peer_name_paragraph select').append($('<option>', {value:this.phwangLinkObject().nameListElement(i), text:this.phwangLinkObject().nameListElement(i)}));
        }
    };
    this.objectName = function () { return "GoConfigHtmlObject";};
    this.rootObject = function() {return this.theRootObject;};
    this.phwangObject = function() {return this.rootObject().phwangObject();};
    this.phwangAjaxObject = function() {return this.phwangObject().phwangAjaxObject();};
    this.phwangLinkObject = function() {return this.phwangObject().phwangLinkObject();};
    this.phwangSessionObject = function() {return this.phwangObject().phwangSessionObject();};
    this.configObject = function() {return this.rootObject().configObject();};
    this.configStorageObject = function() {return this.rootObject().configStorageObject();};
    this.debug = function(debug_val, str1_val, str2_val) {if (debug_val) {this.logit(str1_val, str2_val);}};
    this.logit = function(str1_val, str2_val) {this.rootObject().logit_(this.objectName() + "." + str1_val, str2_val);};
    this.abend = function(str1_val, str2_val) {this.rootObject().abend_(this.objectName() + "." + str1_val, str2_val);};
    this.init__(root_object_val);
}
function GoConfigAjaxClass(root_object_val) {
    "use strict";
    this.init__ = function (root_object_val) {this.theRootObject = root_object_val;};
    this.receiveSetupLinkResponse = function(result_val) {};
    this.receiveGetNameListResponse = function(result_val) {this.htmlObject().renderNameList();};
    this.receiveSetupSessionResponse = function(result_val) {/*this.htmlObject().gotoNextPage();*/};
    this.receiveSetupSession2Response = function(result_val) {this.htmlObject().gotoNextPage();};
    this.receiveSetupSession3Response = function(result_val) {this.htmlObject().gotoNextPage();};
    this.receivePuttSessionDataResponse = function (result_val) {};
    this.receiveGetSessionDataResponse = function (result_val, data_val) {};
    this.objectName = function () { return "GoConfigAjaxClass";};
    this.rootObject = function() {return this.theRootObject;};
    this.phwangObject = function() {return this.rootObject().phwangObject();};
    this.htmlObject = function() {return this.rootObject().htmlObject();};
    this.debug = function(debug_val, str1_val, str2_val) {if (debug_val) {this.logit(str1_val, str2_val);}};
    this.logit = function(str1_val, str2_val) {return this.phwangObject().LOG_IT(this.objectName() + "." + str1_val, str2_val);};
    this.abend = function(str1_val, str2_val) {return this.phwangObject().ABEND(this.objectName() + "." + str1_val, str2_val);};
    this.init__(root_object_val);
}
var config_main = function() {"use strict"; new GoConfigRootObject();};
//$(document).ready(config_main);
window.onload = config_main;

// Root Html Ajax
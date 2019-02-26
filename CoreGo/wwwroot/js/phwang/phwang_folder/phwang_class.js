/*
  Copyrights reserved
  Written by Paul Hwang
*/

function PhwangClass (root_val) {
    "use strict";
    this.init__ = root_val => {this.theRootObject = root_val;};
    this.initObject = () => {
        this.thePhwangAjaxObject = new PhwangAjaxClass(this);
        this.theLinkObject = new LinkClass(this);
        this.thePhwangPortObject = new PhwangPortClass(this);
        this.debug(true, "initObject", "");
    };
    this.decodeNumber = (input_val, size_val) => {
        var output = 0;
        for (var index = 0; index < size_val; index++) {
            output *= 10;
            output += input_val.charAt(index) - '0';
        }
        return output;
    };
    this.encodeNumber = (number_val, size_val) => {
        var str = number_val.toString();
        var buf = "";
        for (var i = str.length; i < size_val; i++) {
            buf = buf + "0";
        }
        buf = buf + str;
        return buf;
    };
    this.serverHttpHeader = () => "http://" + window.location.hostname + ":" + window.location.port + "/";
    this.serverHttpsHeader = () => "https://" + window.location.hostname + ":" + window.location.port + "/";
    this.objectName = () => "PhwangClass";
    this.rootObject = () => this.theRootObject;
    this.phwangAjaxObject = () => this.thePhwangAjaxObject;
    this.linkObject = () => this.theLinkObject;
    this.phwangPortObject = () => this.thePhwangPortObject;
    this.debug = (debug_val, str1_val, str2_val) => { if (debug_val) {this.logit(str1_val, str2_val);}};
    this.logit = (str1_val, str2_val) => { this.LOG_IT(this.objectName() + "." + str1_val, str2_val);};
    this.abend = (str1_val, str2_val) => { this.ABEND(this.objectName() + "." + str1_val, str2_val);};
    this.LOG_IT = (str1_val, str2_val) => { window.console.log(str1_val + "() " + str2_val); };
    this.ASSERT = (val, str1_val, str2_val) => { if((val == null) || (val == "undefined")) this.ABEND(str1_val, str2_val + "(" + val + ")"); };
    this.ABEND = (str1_val, str2_val) => {window.console.log("***ABEND*** " + str1_val + "() " + str2_val); window.alert("***ABEND*** " + str1_val + "() " + str2_val); var x = junk;};
    this.init__(root_val);
}
function PhwangPortClass (phwang_object_val) {
    "use strict";
    this.init__ = function (phwang_object_val) {this.thePhwangObject = phwang_object_val;};
    this.receiveSetupLinkResponse = function(result_val) {this.rootObject().ajaxResponseObject().receiveSetupLinkResponse(result_val);};
    this.receiveGetNameListResponse = function (result_val) { this.rootObject().ajaxResponseObject().receiveGetNameListResponse(result_val);};
    this.receiveSetupSessionResponse = function (result_val) { this.rootObject().ajaxResponseObject().receiveSetupSessionResponse(result_val);};
    this.receiveSetupSession2Response = function (result_val) { this.rootObject().ajaxResponseObject().receiveSetupSession2Response(result_val);};
    this.receiveSetupSession3Response = function (result_val) { this.rootObject().ajaxResponseObject().receiveSetupSession3Response(result_val);};
    this.receivePutSessionDataResponse = function (result_val) { this.rootObject().ajaxResponseObject().receivePutSessionDataResponse(result_val, data_val);};
    this.receiveGetSessionDataResponse = function (result_val, data_val) { this.rootObject().ajaxResponseObject().receiveSetupSessionResponse(result_val, data_val);};
    this.objectName = function() {return "PhwangPortClass";};
    this.phwangObject = function() {return this.thePhwangObject;};
    this.rootObject = function() {return this.phwangObject().rootObject();};
    this.phwangAjaxObject = function() {return this.phwangObject().phwangAjaxObject();};
    this.phwangSessionObject = function() {return this.phwangObject().phwangSessionObject();};
    this.debug = function(debug_val, str1_val, str2_val) {if (debug_val) {this.logit(str1_val, str2_val);}};
    this.logit = function(str1_val, str2_val) {return this.phwangObject().LOG_IT(this.objectName() + "." + str1_val, str2_val);};
    this.abend = function(str1_val, str2_val) {return this.phwangObject().ABEND(this.objectName() + "." + str1_val, str2_val);};
    this.init__(phwang_object_val);
}
function PhwangQueueClass (phwang_object_val) {
    "use strict";
    this.init__ = phwang_object_val => {
        this.thePhwangObject = phwang_object_val;
        this.theMaxQueueLength = 1;
        this.theQueueLength = 0;
        this.theQueueArray = [this.maxQueueLength()];
    };
    this.enqueueData = data_val => {
        if (this.queueLength() >= this.maxQueueLength()) {this.abend("enqueueData", "queue full"); return;}
        this.queueArray()[this.queueLength()] = data_val;
        this.incrementQueueLength();
    };
    this.dequeueData = () => {
        if (this.queueLength() === 0) { return 0;}
        var data = this.queueArray()[0];
        this.decrementQueueLength();
        for (var i = 0; i < this.queueLength(); i++) {this.queueArray()[i] = this.queueArray()[i + 1];}
        return data;
    };
    this.maxQueueLength = () => this.theMaxQueueLength;
    this.queueLength = () => this.theQueueLength;
    this.incrementQueueLength = () => { this.theQueueLength += 1};
    this.decrementQueueLength = () =>{ this.theQueueLength -= 1};
    this.queueArray = () => this.theQueueArray;
    this.objectName = () => "PhwangQueueClass";
    this.phwangObject = () => this.thePhwangObject;
    this.debug = (debug_val, str1_val, str2_val) => { if (debug_val) { this.logit(str1_val, str2_val);} };
    this.logit = (str1_val, str2_val) => { this.phwangObject().LOG_IT(this.objectName() + "." + str1_val, str2_val);};
    this.abend = (str1_val, str2_val) => { this.phwangObject().ABEND(this.objectName() + "." + str1_val, str2_val);};
    this.init__(phwang_object_val);
}

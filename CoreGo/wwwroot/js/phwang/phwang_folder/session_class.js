/*
 * Copyrights phwang
 * Written by Paul Hwang
 */
function PhwangSessionClass (link_object_val) {
    "use strict";
    this.init__ = link_object_val => {
        this.theLinkObject = link_object_val;
        this.theXmtSeq = 0;
        this.theRcvSeq = 0;
        this.debug(false, "init__", "sessionId=" + this.sessionId());
    };
    this.xmtSeq = () => this.theXmtSeq;
    this.incrementXmtSeq = () => { this.theXmtSeq += 1; };
    this.rcvSeq = () => this.theRcvSeq;
    this.incrementRcvSeq = () => { this.theRcvSeq += 1; };
    this.transmitData = data_val => {this.phwangAjaxObject().putSessionData(this, data_val);};
    this.receiveData = c_data_val => { this.assert(this.themeObject(), "receiveData", "null themeObject"); this.themeObject().receiveData(c_data_val);};
    this.themeObject = () => this.theThemeObject;
    this.setThemeObject = val => { this.assert(val, "setThemeObject", "null val"); this.theThemeObject = val; };
    this.objectName = () => "PhwangSessionClass";
    this.linkObject = () => this.theLinkObject;
    this.phwangObject = () => this.linkObject().phwangObject();
    this.rootObject = () => this.phwangObject().rootObject();
    this.phwangAjaxObject = () => this.phwangObject().phwangAjaxObject();
    this.debug = (debug_val, str1_val, str2_val) => {if (debug_val) {this.logit(str1_val, str2_val);}};
    this.logit = (str1_val, str2_val) => { this.phwangObject().LOG_IT(this.objectName() + "." + str1_val, str2_val);};
    this.assert = (val, str1_val, str2_val) => { this.phwangObject().ASSERT(val, this.objectName() + "." + str1_val, str2_val); };
    this.abend = (str1_val, str2_val) => { this.phwangObject().ABEND(this.objectName() + "." + str1_val, str2_val);};
    this.sessionId = () => this.theSessionId;
    this.setSessionId = val => { this.theSessionId = val; };
    this.init__(link_object_val);
}

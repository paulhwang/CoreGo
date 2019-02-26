/*
  Copyrights reserved
  Written by Paul Hwang
*/
function GoMgrClass(theme_mgr_object_val) {
    "use strict";
    this.init__ = (root_object_val) => {
        this.theThemeMgrObject = theme_mgr_object_val;
    };
    this.objectName = () => "GoMgrClass";
    this.themeMgrObject = () => this.theThemeMgrObject;
    this.rootObject = () => this.themeMgrObject().rootObject();
    this.phwangObject = () => this.rootObject().phwangObject();
    this.debug = function (debug_val, str1_val, str2_val) { if (debug_val) { this.logit(str1_val, str2_val); } };
    this.logit = function (str1_val, str2_val) { return this.rootObject().logit_(this.objectName() + "." + str1_val, str2_val); };
    this.abend = function (str1_val, str2_val) { return this.rootObject().abend_(this.objectName() + "." + str1_val, str2_val); };
    this.init__(root_object_val);
}

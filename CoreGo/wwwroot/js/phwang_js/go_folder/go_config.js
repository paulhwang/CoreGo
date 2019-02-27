/*
 * Copyrights phwang
 * Written by Paul Hwang
 */
function GoConfigObject (root_val) {
    "use strict";
    this.init__ = root_val => {
        this.theRootObject = root_val;
        this.debug(true, "init__", "myColor=" + this.myColor() + " boardSize=" + this.boardSize() + " hisName=" + this.hisName() + " handicapPoint=" + this.handicapPoint() + " komiPoint=" + this.komiPoint());
    };
    this.cacheConfig = () => {
        this.theBoardSize = 19;
        this.setPlayBothSides();
    }
    this.decodeConfig = encoded_val => {
        this.debug(true, "decodeConfig", encoded_val);
        if ((encoded_val === undefined) || (encoded_val === "")) {
            this.setBoardSize(19);
            this.setHandicapPoint(0);
            this.setKomiPoint(0);
            return;
        }
        var data;
        if (encoded_val.charAt(0) != 'G') {
            this.abend("decodeConfig", "not G");
        }
        var index = 4;
        data = (encoded_val.charAt(index++) - '0') * 10
        data += encoded_val.charAt(index++) - '0';
        this.setBoardSize(data);
        data = (encoded_val.charAt(index++) - '0') * 10
        data += encoded_val.charAt(index++) - '0';
        this.setHandicapPoint(data);
        data = (encoded_val.charAt(index++) - '0') * 10
        data += encoded_val.charAt(index++) - '0';
        this.setKomiPoint(data);
        data = encoded_val.charAt(index++) - '0';
        this.setMyColor(data);
        this.setHisName(encoded_val.slice(index));
        this.debug(true, "decodeConfig", "myColor=" + this.myColor() + " boardSize=" + this.boardSize() + " hisName=" + this.hisName() + " handicapPoint=" + this.handicapPoint() + " komiPoint=" + this.komiPoint());
    };
    this.encodeConfig = my_name_val => {
        var len = 11 + my_name_val.length;
        var buf = "G";
        if (len < 100) buf = buf + 0; if (len < 10) buf = buf + 0; buf = buf + len;
        if (this.boardSize() < 10) buf = buf + 0; buf = buf + this.boardSize();
        if (this.handicapPoint() < 10) buf = buf + 0; buf = buf + this.handicapPoint();
        if (this.komiPoint() < 10) buf = buf + 0; buf = buf + this.komiPoint();
        buf = buf + this.hisColor();
        buf = buf + my_name_val;
        return buf;
    };
    this.playBothSides = () => this.thePlayBothSides;
    this.setPlayBothSides = () => {this.thePlayBothSides = (this.linkObject().myName() === this.hisName());};
    this.boardSize = () => this.theBoardSize;
    this.setBoardSize = val => {this.theBoardSize = val;};
    this.myColor = () => this.theMyColor;
    this.setMyColor = val => { this.theMyColor = val; };
    this.setMyColorRaw = val => { if (val === "black") { this.theMyColor = GO.BLACK_STONE(); } else if (val === "white") { this.theMyColor = GO.WHITE_STONE(); } else { this.abend("setMyColorRaw", val); } };
    this.hisColor = () => { if (this.myColor() === GO.BLACK_STONE()) { return GO.WHITE_STONE(); } else { return GO.BLACK_STONE(); } };
    this.setHisName = val => { this.theHisName = val; };
    this.hisName = () => this.theHisName;
    this.handicapPoint = () => this.theHandicapPoint;
    this.setHandicapPoint = val => { this.theHandicapPoint = val; }
    this.komiPoint = () => this.theKomiPoint;
    this.setKomiPoint = val => { this.theKomiPoint = val; }
    this.realKomiPoint = () => { if (!this.komiPoint()) {return 0;} return this.komiPoint() + 0.5; };
    this.isValidCoordinates = (x_val, y_val) => { return this.isValidCoordinate(x_val) && this.isValidCoordinate(y_val); };
    this.isValidCoordinate = coordinate_val => {return (0 <= coordinate_val) && (coordinate_val < this.boardSize());};
    this.objectName = () => "GoConfigObject";
    this.rootObject = () => this.theRootObject;
    this.linkObject = () => this.rootObject().linkObject();
    this.debug = (debug_val, str1_val, str2_val) => {if (debug_val) {this.logit(str1_val, str2_val); }};
    this.logit = (str1_val, str2_val) => { this.rootObject().logit_(this.objectName() + "." + str1_val, str2_val); };
    this.abend = (str1_val, str2_val) => { this.rootObject().abend_(this.objectName() + "." + str1_val, str2_val); };
    this.init__(root_val);
}
var GO = new GoDefineObject;
function GoDefineObject () {
    this.EMPTY_STONE = () => 0;
    this.BLACK_STONE = () => 1;
    this.WHITE_STONE = () => 2;
    this.BOTH_COLOR_STONE = () => 2;
    this.MARK_DEAD_STONE_DIFF = () => 4;
    this.MARK_EMPTY_STONE_DIFF = () => 8;
    this.MARKED_DEAD_BLACK_STONE =  () => {return this.BLACK_STONE() + this.MARK_DEAD_STONE_DIFF();};
    this.MARKED_DEAD_WHITE_STONE = () => {return this.WHITE_STONE() + this.MARK_DEAD_STONE_DIFF();};
    this.MARKED_EMPTY_BLACK_STONE = () => {return this.BLACK_STONE() + this.MARK_EMPTY_STONE_DIFF();};
    this.MARKED_EMPTY_WHITE_STONE = () => {return this.WHITE_STONE() + this.MARK_EMPTY_STONE_DIFF();};
}

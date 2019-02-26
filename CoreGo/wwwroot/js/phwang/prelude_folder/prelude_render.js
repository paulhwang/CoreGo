/*
  Copyrights reserved
  Written by Paul Hwang
*/
function PreludeRenderObject(root_object_val) {
    "use strict";
    this.init__ = root_object_val => {
        this.theRootObject = root_object_val;
    };
    this.renderPreludePage = () => {
        this.preludeHtmlObject().renderPreludeComponent();
        var this0 = this;
        $(".prelude_section .sign_up_button").on("click", function () {
            this0.debug(true, "renderPreludePage click function", ".prelude_section .sign_up_button");
            this0.renderSignUpPage();
        });
        $(".prelude_section .sign_in_button").on("click", function () {
            this0.debug(true, "renderPreludePage click function", ".prelude_section .sign_in_button");
            this0.renderSignInPage();
        });
    };
     this.renderSignUpPage = () => {
        this.preludeHtmlObject().renderSignUpComponent();
        var this0 = this;
        $(".sign_up_section .sign_up_button").on("click", function () {
            this0.debug(true, "renderSignUpPage click function", ".sign_up_section .sign_up_button");
            this0.renderPreludePage();
        });
    };
    this.renderSignInPage = () => {
        this.preludeHtmlObject().renderSignInComponent();
        var this0 = this;
        $(".sign_in_section .sign_in_button").on("click", function () {
            this0.debug(true, "renderSignUpPage click function", ".sign_in_section .sign_in_button");
            this0.linkObject().setMyName($(".sign_in_section .sign_in_name").val());
            var password = $(".sign_in_section .sign_in_password").val();
            this0.debug(true, "renderSignInPage", "myName=" + this0.linkObject().myName() + " password=" + password);
            if (this0.linkObject().myName()) {
                this0.phwangAjaxObject().setupLink(this0.linkObject(), password);
            }
            //this0.renderThemePage();
        });
    };
    this.renderThemePage = () => {
        this.phwangAjaxObject().startWatchDog(this.linkObject());
        this.preludeHtmlObject().renderThemeComponent();
        var this0 = this;
        $(".theme_section .go_button").on("click", function () {
            this0.debug(true, "renderThemePage click function", ".theme_section .go_button");
            this0.renderGoConfigPage();
        });
    };
    this.renderGoConfigPage = () => {
        this.preludeHtmlObject().renderGoConfigComponent();
        this.renderNameList();
        var this0 = this;
        $(".config_section .config_button").on("click", function () {
            var theme = this0.themeMgrObject().mallocThemeAndInsert();
            theme.configObject().setHisName($(".peer_name_paragraph select").val());
            theme.configObject().setMyColorRaw($(".config_section .go_config_section .stone_color").val());
            theme.configObject().setBoardSize($(".config_section .go_config_section .board_size").val());
            theme.configObject().setKomiPoint($(".config_section .go_config_section .komi").val());
            theme.configObject().setHandicapPoint($(".config_section .go_config_section .handicap").val());
            var encoded_config = theme.configObject().encodeConfig(this0.linkObject().myName());
            this0.debug(true, "setupHtmlInput", "boardSize=" + theme.configObject().boardSize() + " myColor=" + theme.configObject().myColor() + " komi=" + theme.configObject().komiPoint() + " handicap=" + theme.configObject().handicapPoint());
            this0.phwangAjaxObject().setupSession(this0.linkObject(), theme.configObject().hisName(), theme.themeIdStr() + encoded_config);
            //this0.renderGoGamePage();
        });
        //window.open(this.phwangObject().serverHttpHeader() + "Go/GoSetup", "_self");
    };
    this.renderNameList = () => {
        $('.peer_name_paragraph select').empty();
        for (var i = 0; i < this.linkObject().nameListLength(); i++) {
            $('.peer_name_paragraph select').append($('<option>', { value: this.linkObject().nameListElement(i), text: this.linkObject().nameListElement(i) }));
        }
    };
    this.renderGoGamePage = theme_id_str_val => {
        this.preludeHtmlObject().renderGoGameComponent();
       var theme = this.themeMgrObject().getTheme(theme_id_str_val);
        theme.init_game();
    };
    this.objectName = () => "PreludeRenderObject";
    this.rootObject = () => this.theRootObject;
    this.preludeHtmlObject = () => this.rootObject().preludeHtmlObject();
    this.phwangObject = () => this.rootObject().phwangObject();
    this.phwangAjaxObject = () => this.phwangObject().phwangAjaxObject();
    this.linkObject = () => this.phwangObject().linkObject();
    this.themeMgrObject = () => this.rootObject().themeMgrObject();
    this.debug = (debug_val, str1_val, str2_val) => { if (debug_val) { this.logit(str1_val, str2_val); } };
    this.logit = (str1_val, str2_val) => { this.logit_(this.objectName() + "." + str1_val, str2_val); };
    this.abend = (str1_val, str2_val) => { this.abend_(this.objectName() + "." + str1_val, str2_val); };
    this.logit_ = (str1_val, str2_val) => { this.phwangObject().LOG_IT(str1_val, str2_val); };
    this.abend_ = (str1_val, str2_val) => { this.phwangObject().ABEND(str1_val, str2_val); };
    this.init__(root_object_val);
}


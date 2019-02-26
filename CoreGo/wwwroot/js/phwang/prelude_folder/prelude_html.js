/*
  Copyrights reserved
  Written by Paul Hwang
*/
var ThePreludeHtmlObject;
function PreludeHtmlObject (root_object_val) {
    "use strict";
    this.init__ = root_object_val => {
        this.theRootObject = root_object_val;
        ThePreludeHtmlObject = this;
        this.debug(true, "init__", "");
        this.initSwitches();
    };
    this.initSwitches = () => {
        this.thePreludeSwitch = false;
        this.theSignUpSwitch = false;
        this.theSignInSwitch = false;
        this.theThemeSwitch = false;
        this.theGoConfigSwitch = false;
        this.theGoGameSwitch = false;
    };
    this.dispalySwitch = () => this.theDispalySwitch;
    this.preludeSwitch = () => ThePreludeHtmlObject.thePreludeSwitch;
    this.signUpSwitch = () => ThePreludeHtmlObject.theSignUpSwitch;
    this.signInSwitch = () => ThePreludeHtmlObject.theSignInSwitch;
    this.themeSwitch = () => ThePreludeHtmlObject.theThemeSwitch;
    this.goConfigSwitch = () => ThePreludeHtmlObject.theGoConfigSwitch;
    this.goGameSwitch = () => ThePreludeHtmlObject.theGoGameSwitch;
    this.theDispalySwitch = {
        prelude_switch: this.preludeSwitch,
        sign_up_switch: this.signUpSwitch,
        sign_in_switch: this.signInSwitch,
        theme_switch: this.themeSwitch,
        go_config_switch: this.goConfigSwitch,
        go_game_switch: this.goGameSwitch,
    };
    this.setPreludeSwitch = val => { this.thePreludeSwitch = val; };
    this.setSignUpSwitch = val => { this.theSignUpSwitch = val; };
    this.setSignInSwitch = val => { this.theSignInSwitch = val; };
    this.setThemeSwitch = val => { this.theThemeSwitch = val; };
    this.setGoConfigSwitch = val => { this.theGoConfigSwitch = val; };
    this.setGoGameSwitch = val => { this.theGoGameSwitch = val; };
    this.setupSwitches = (val1, val2, val3, val4, val5, val6) => {
        this.setPreludeSwitch(val1);
        this.setSignUpSwitch(val2);
        this.setSignInSwitch(val3);
        this.setThemeSwitch(val4);
        this.setGoConfigSwitch(val5);
        this.setGoGameSwitch(val6);
    };
    this.setupPreludeSwitch = () => { this.setupSwitches(true, false, false, false, false, false); };
    this.setupSignUpSwitch = () => { this.setupSwitches(false, true, false, false, false, false); };
    this.setupSignInSwitch = () => { this.setupSwitches(false, false, true, false, false, false); };
    this.setupThemeSwitch = () => { this.setupSwitches(false, false, false, true, false, false); };
    this.setupGoConfigSwitch = () => { this.setupSwitches(false, false, false, false, true, false); };
    this.setupGoGameSwitch = () => { this.setupSwitches(false, false, false, false, false, true); };

    this.randetIt = () => { ReactDOM.render(React.createElement(this.PhwangPreludeComponentClass, this.dispalySwitch()), document.getElementById("phwang_prelude"));}
    this.renderPreludeComponent = () => { this.setupPreludeSwitch(); this.randetIt(); };
    this.renderSignUpComponent = () => { this.setupSignUpSwitch(); this.randetIt(); };
    this.renderSignInComponent = () => { this.setupSignInSwitch(); this.randetIt(); };
    this.renderThemeComponent = () => { this.setupThemeSwitch(); this.randetIt(); };
    this.renderGoConfigComponent = () => { this.setupGoConfigSwitch(); this.randetIt(); };
    this.renderGoGameComponent = () => { this.setupGoGameSwitch(); this.randetIt(); };

    this.PhwangPreludeComponentClass = React.createClass({
        render: function () {
            var this0 = ThePreludeHtmlObject;
            this0.debug(true, "PhwangPreludeComponentClass", "start");
            return React.DOM.section(null,
                React.createElement(this0.preludeComponent, this.props),
                this0.signUpComponent(this.props),
                this0.signInComponent(this.props),
                this0.themeComponent(this.props),
                this0.goConfigComponent(this.props),
                this0.goGameComponent(this.props)
            );
        }
    });
    this.preludeComponent = React.createClass({
        propTypes: {
            prelude_switch: React.PropTypes.func.isRequired,
        },
        componentWillUpdate: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "PreludeComponent", "componentWillUpdate");
            console.log("componentWillUpdate", arguments);
        },
        componentDidUpdate: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "PreludeComponent", "componentDidUpdate");
            console.log("componentDidUpdate", arguments);
        },
        componentWillMount: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "PreludeComponent", "componentWillMount");
            console.log("componentWillMount", arguments);
        },
        componentDidMount: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "PreludeComponent", "componentDidMount");
            console.log("componentDidMount", arguments);
        },
        componentWillUnmount: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "PreludeComponent", "componentWillUnmount");
            console.log("componentWillUnmount", arguments);
        },
        signUpOnClick: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "PreludeComponent", "signUpOnClick");
        },
        signInOnClick: function () {
            var this0 = ThePreludeHtmlObject;
            this0.debug(true, "preludeComponent", "signInOnClick");
        },
        render: function () {
            var this0 = ThePreludeHtmlObject;
            this0.debug(true, "preludeComponent", "start");
            if (this.props.prelude_switch()) {
                return React.DOM.section({ className: "prelude_section" },
                    React.DOM.h1(null, "Let's Play Go!"),
                    React.DOM.p({ className: "lead" }, "Let's Play Go is a free web platform to play Go Game with people in the world."),
                    React.DOM.button({ className: "sign_up_button", onClick: this.signUpOnClick }, "Sign up"),
                    React.DOM.button({ className: "sign_in_button", onClick: this.signInOnClick }, "Sign in"),
                );
            }
            else {
                return null;
            }
        }
    });
    this.signUpComponent = props => {
        if (props.sign_up_switch()) {
            return React.DOM.section({ className: "sign_up_section" },
                React.DOM.h2(null, "Account Sign Up"),
                React.DOM.p(null, "Name:", React.DOM.input({ className: "sign_up_name", placeholder: "Enter your account name" })),
                React.DOM.p(null, "Password:", React.DOM.input({ className: "sign_up_password", placeholder: "Enter your password" })),
                React.DOM.p(null, "email:", React.DOM.input({ className: "sign_up_email", placeholder: "Enter your email" })),
                React.DOM.button({ className: "sign_up_button" }, "Sign up"),
            );
        }
        else {
            return null;
        }
    }
    this.signInComponent = props => {
        if (props.sign_in_switch()) {
            return React.DOM.section({ className: "sign_in_section" },
                React.DOM.h2(null, "Account Sign In"),
                React.DOM.p(null, "Name:", React.DOM.input({ className: "sign_in_name", placeholder: "Enter your account name" })),
                React.DOM.p(null, "Password:", React.DOM.input({ className: "sign_in_password", placeholder: "Enter your password" })),
                React.DOM.button({ className: "sign_in_button" }, "Sign in"),
            );
        }
        else {
            return null;
        }
    }
    this.themeComponent = props => {
        if (props.theme_switch()) {
            return React.createElement("section", { "className": "theme_section" },
                React.DOM.h1(null, "Select the Theme"),
                React.DOM.button({ "className": "go_button" }, "Play Go"),
            );
        }
        else {
            return null;
        }
    }
    this.goConfigComponent = props => {
        if (props.go_config_switch()) {
            return React.DOM.section({ "className": "config_section" },
                React.DOM.h2(null, "GoSetup"),
                React.DOM.p({ "className": "peer_name_paragraph" },
                    "Peer Name:",
                    React.DOM.select(
                        { "name": "opponent" },
                    )
                ),
                React.DOM.p({ "className": "peer_game_paragraph" },
                    "Game",
                    React.DOM.select(null,
                        React.DOM.option({ "value": "Go" }, "Go"),
                        React.DOM.option({ "value": "game1" }, "game1"),
                        React.DOM.option({ "value": "game2" }, "game2"),
                    ),
                    React.DOM.button(null, "Select"),
                ),
                React.DOM.section({ "className": "go_config_section" },
                    React.DOM.p(null,
                        "Board Size:",
                        React.DOM.select({ "className": "board_size" },
                            React.DOM.option({ "value": "19" }, "19x19"),
                            React.DOM.option({ "value": "13" }, "13x13"),
                            React.DOM.option({ "value": "9" }, "9x9"),
                        )
                    ),
                    React.DOM.p(null,
                        "Stone Color:",
                        React.DOM.select({ "className": "stone_color" },
                            React.DOM.option({ "value": "black" }, "Black"),
                            React.DOM.option({ "value": "white" }, "White"),
                        )
                    ),
                    React.DOM.p(null,
                        "Komi:",
                        React.DOM.select({ "className": "komi" },
                            React.DOM.option({ "value": "0" }, 0.5),
                            React.DOM.option({ "value": "4" }, 4.5),
                            React.DOM.option({ "value": "5" }, 5.5),
                            React.DOM.option({ "value": "6" }, 6.5),
                            React.DOM.option({ "value": "7" }, 7.5),
                            React.DOM.option({ "value": "8" }, 8.5),
                        )
                    ),
                    React.DOM.p(null,
                        "Handicap:",
                        React.DOM.select({ "className": "handicap" },
                            React.DOM.option({ "value": "0" }, 0),
                            React.DOM.option({ "value": "2" }, 2),
                            React.DOM.option({ "value": "3" }, 3),
                            React.DOM.option({ "value": "4" }, 4),
                            React.DOM.option({ "value": "5" }, 5),
                            React.DOM.option({ "value": "6" }, 6),
                            React.DOM.option({ "value": "7" }, 7),
                            React.DOM.option({ "value": "8" }, 8),
                            React.DOM.option({ "value": "9" }, 9),
                            React.DOM.option({ "value": "10" }, 10),
                            React.DOM.option({ "value": "11" }, 11),
                            React.DOM.option({ "value": "12" }, 12),
                            React.DOM.option({ "value": "13" }, 13),
                        )
                    )
                ),
                React.DOM.button({ "className": "config_button" }, "Connect"),
            );
        }
        else {
            return null;
        }
    }
    this.goGameComponent = props => {
        if (props.go_game_switch()) {
            return React.DOM.section(null,
                React.DOM.section(null,
                    React.DOM.canvas({ id: "go_canvas" })),
                React.DOM.section(null,
                    React.DOM.p({ "id": "black_score" }, "Black 0"),
                    React.DOM.p({ "id": "white_score" }, "White 0")),
            );
        }
        else {
            return null;
        }
    }
    this.objectName = () => "PreludeHtmlObject";
    this.rootObject = () => this.theRootObject;
    this.phwangObject = () => this.rootObject().phwangObject();
    this.debug = (debug_val, str1_val, str2_val) => { if (debug_val) { this.logit(str1_val, str2_val); } };
    this.logit = (str1_val, str2_val) => { this.logit_(this.objectName() + "." + str1_val, str2_val); };
    this.abend = (str1_val, str2_val) => { this.abend_(this.objectName() + "." + str1_val, str2_val); };
    this.logit_ = (str1_val, str2_val) => { this.phwangObject().LOG_IT(str1_val, str2_val); };
    this.abend_ = (str1_val, str2_val) => { this.phwangObject().ABEND(str1_val, str2_val); };
    this.init__(root_object_val);
}
/*
this.preludeComponent_old = props => {
    if (props.prelude_switch()) {
        return React.DOM.section({ className: "prelude_section" },
            React.DOM.h1(null, "Let's Play Go!"),
            React.DOM.p({ className: "lead" }, "Let's Play Go is a free web platform to play Go Game with people in the world."),
            React.DOM.button({ className: "sign_up_button" }, "Sign iup"),
            React.DOM.button({ className: "sign_in_button" }, "Sign in"),
            React.DOM.button({ className: "theme_button" }, "Theme"),
            React.createElement("p", null, React.createElement("a", { "href": "http://localhost:8168/Account/AccountSignIn", "className": "btn btn-primary btn-lg" }, "Sign in")),
            //<p><a href="http://localhost:8168/Account/AccountSignUp" class="btn btn-primary btn-lg">Sign up &raquo;</a></p>
        );
    }
    else {
        return null;
    }
}
*/
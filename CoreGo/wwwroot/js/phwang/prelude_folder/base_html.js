/*
  Copyrights reserved
  Written by Paul Hwang
*/
var TheBaseHtmlObject;
function BaseHtmlObject(root_object_val) {
    "use strict";
    this.init__ = root_object_val => {
        this.theRootObject = root_object_val;
        TheBaseHtmlObject = this;
        this.debug(true, "init__", "");
    };
    this.randerBaseComponent = () => {
        this.thePreludeComponent = ReactDOM.render(React.createElement(this.PreludeComponent, null), document.getElementById("prelude_base"));
        this.theSignUpComponent = ReactDOM.render(React.createElement(this.SignUpComponent, null), document.getElementById("sign_up_base"));
        this.theSignInComponent = ReactDOM.render(React.createElement(this.SignInComponent, null), document.getElementById("sign_in_base"));
        this.theThemeComponent = ReactDOM.render(React.createElement(this.ThemeComponent, null), document.getElementById("theme_base"));
        this.theGoConfigComponent = ReactDOM.render(React.createElement(this.GoConfigComponent, null), document.getElementById("go_config_base"));
        this.theGoGameComponent = ReactDOM.render(React.createElement(this.GoGameComponent, null), document.getElementById("go_game_base"));
        //ReactDOM.render(React.createElement(this.BaseComponent, null), document.getElementById("phwang_prelude"));
    };
    this.BaseComponent = React.createClass({
        render: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "BaseComponent", "start");
            return React.DOM.section(null,
                React.createElement(this0.PreludeComponent, null),
                React.createElement(this0.SignUpComponent, null),
                React.createElement(this0.SignInComponent, null),
                React.createElement(this0.ThemeComponent, null),
                React.createElement(this0.GoConfigComponent, null),
                React.createElement(this0.GoGameComponent, null),
            );
        }
    });
    this.PreludeComponent = React.createClass({
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
        sign_up_onClick: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "PreludeComponent", "sign_up_onClick");
        },
        sign_in_onClick: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "PreludeComponent", "sign_in_onClick");
        },
        theme_onClick: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "PreludeComponent", "theme_onClick");
        },
        render: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "PreludeComponent", "start");
            return React.DOM.section({ className: "prelude_section" },
                React.DOM.h1(null, "Let's Play Go!"),
                React.DOM.p({ className: "lead" }, "Let's Play Go is a free web platform to play Go Game with people in the world."),
                React.DOM.button({ className: "sign_up_button", onClick: this.sign_up_onClick  }, "Sign iup"),
                React.DOM.button({ className: "sign_in_button", onClick: this.sign_in_onClick }, "Sign in"),
                React.DOM.button({ className: "theme_button", onClick: this.theme_onClick  }, "Theme"),
            );
        }
    });
    this.SignUpComponent = React.createClass({
        sign_up_onClick: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "SignUpComponent", "sign_up_onClick");
        },
        render: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "SignUpComponent", "start");
            return React.DOM.section({ className: "sign_up_section" },
                React.DOM.h2(null, "Account Sign Up"),
                React.DOM.p(null, "Name:", React.DOM.input({ className: "sign_up_name", placeholder: "Enter your account name" })),
                React.DOM.p(null, "Password:", React.DOM.input({ className: "sign_up_password", placeholder: "Enter your password" })),
                React.DOM.p(null, "email:", React.DOM.input({ className: "sign_up_email", placeholder: "Enter your email" })),
                React.DOM.button({ className: "sign_up_button", onClick: this.sign_up_onClick }, "Sign up"),
            );
        }
    });
    this.SignInComponent = React.createClass({
        sign_in_onClick: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "SignInComponent", "sign_in_onClick");
        },
        render: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "signInComponent", "start");
            return React.DOM.section({ className: "sign_in_section" },
                React.DOM.h2(null, "Account Sign In"),
                React.DOM.p(null, "Name:", React.DOM.input({ className: "sign_in_name", placeholder: "Enter your account name" })),
                React.DOM.p(null, "Password:", React.DOM.input({ className: "sign_in_password", placeholder: "Enter your password" })),
                React.DOM.button({ className: "sign_in_button", onClick: this.sign_in_onClick }, "Sign in"),
            );
        }
    });
    this.ThemeComponent = React.createClass({
        play_go_onClick: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "ThemeComponent", "play_go_onClick");
        },
        render: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "ThemeComponent", "start");
            return React.createElement("section", { "className": "theme_section" },
                React.DOM.h1(null, "Select the Theme"),
                React.DOM.button({ "className": "go_button", onClick: this.play_go_onClick }, "Play Go"),
            );
        }
    });
    this.GoConfigComponent = React.createClass({
        play_onClick: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "GoConfigComponent", "play_onClick");
        },
        render: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "GoConfigComponent", "start");
            return React.DOM.section(
                { "className": "config_section" },
                React.DOM.h2(null, "GoSetup"),
                React.DOM.p(
                    { "className": "peer_name_paragraph" },
                    "Peer Name:",
                    React.DOM.select(
                        { "name": "opponent" },
                        null
                    )
                ),
                React.DOM.p(
                    { "className": "peer_game_paragraph" },
                    "Game",
                    React.DOM.select(
                        null,
                        React.DOM.option({ "value": "Go" }, "Go"),
                        React.DOM.option({ "value": "game1" }, "game1"),
                        React.DOM.option({ "value": "game2" }, "game2")
                    ),
                    React.DOM.button(null, "Select"),
                ),
                React.DOM.section(
                    { "className": "go_config_section" },
                    React.DOM.p(
                        null,
                        "Board Size:",
                        React.DOM.select(
                            { "className": "board_size" },
                            React.DOM.option({ "value": "19" }, "19x19"),
                            React.DOM.option({ "value": "13" }, "13x13"),
                            React.DOM.option({ "value": "9" }, "9x9"),
                        )
                    ),
                    React.DOM.p(
                        null,
                        "Stone Color:",
                        React.DOM.select(
                            { "className": "stone_color" },
                            React.DOM.option({ "value": "black" }, "Black"),
                            React.DOM.option({ "value": "white" }, "White"),
                        )
                    ),
                    React.DOM.p(
                        null,
                        "Komi:",
                        React.DOM.select(
                            { "className": "komi" },
                            React.DOM.option({ "value": "0" }, 0.5),
                            React.DOM.option({ "value": "4" }, 4.5),
                            React.DOM.option({ "value": "5" }, 5.5),
                            React.DOM.option({ "value": "6" }, 6.5),
                            React.DOM.option({ "value": "7" }, 7.5),
                            React.DOM.option({ "value": "8" }, 8.5),
                        )
                    ),
                    React.DOM.p(
                        null,
                        "Handicap:",
                        React.DOM.select(
                            { "className": "handicap" },
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
                React.DOM.button({ "className": "config_button", onClick: this.play_onClick }, "Play"),
           );
        }
    });
    this.GoGameComponent = React.createClass({
        render: function () {
            var this0 = TheBaseHtmlObject;
            this0.debug(true, "GoGameComponent", "start");
            return React.DOM.body(null,
                React.DOM.section(null,
                    React.DOM.canvas({ id: "go_canvas" })),
                React.DOM.section(null,
                    React.DOM.p({ "id": "black_score" }, "Black 0"),
                    React.DOM.p({ "id": "white_score" }, "White 0")),
            );
        }
    });
    this.objectName = () => "BaseHtmlObject";
    this.rootObject = () => this.theRootObject;
    this.phwangObject = () => this.rootObject().phwangObject();
    this.debug = (debug_val, str1_val, str2_val) => { if (debug_val) { this.logit(str1_val, str2_val); } };
    this.logit = (str1_val, str2_val) => { this.logit_(this.objectName() + "." + str1_val, str2_val); };
    this.abend = (str1_val, str2_val) => { this.abend_(this.objectName() + "." + str1_val, str2_val); };
    this.logit_ = (str1_val, str2_val) => { this.phwangObject().LOG_IT(str1_val, str2_val); };
    this.abend_ = (str1_val, str2_val) => { this.phwangObject().ABEND(str1_val, str2_val); };
    this.init__(root_object_val);
}
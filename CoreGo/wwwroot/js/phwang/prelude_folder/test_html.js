/*
  Copyrights reserved
  Written by Paul Hwang
*/
var TheTestHtmlObject;
function TestHtmlObject(root_object_val) {
    "use strict";
    this.init__ = root_object_val => {
        this.theRootObject = root_object_val;
        TheTestHtmlObject = this;
        this.debug(true, "init__", "");
    };
    this.TestAreaComponent1 = React.createClass({
        getDefaultProps: function () {
            return {
                text: "",
            };
        },
        propTypes: {
            text: React.PropTypes.string,
        },
        getInitialState: function () {
            return {
                text: this.props.text,
            };
        },
        _textChange: function (ev) {
            var this0 = TheTestHtmlObject;
            this0.debug(true, "TestAreaComponent1", "_textChange");
            this.setState({
                text: ev.target.value,
            });
        },
        _onClick: function () {
            var this0 = ThePreludeHtmlObject;
            this0.debug(true, "TestAreaComponent1", "_onClick");
        },
        render: function () {
            return React.DOM.div(null,
                React.DOM.textarea({
                    value: this.state.text,
                    onChange: this._textChange,
                }),
                React.DOM.h3(null, this.state.text.length),
                React.DOM.button({ onClick: this._onClick }, "Sign in"),
            )
        }
    });
    this.randerTestComponent1 = () => {
        ReactDOM.render(React.createElement(this.TestAreaComponent1, { text: "Jose" }), document.getElementById("phwang_prelude"));
    };
    this.TestAreaComponent2 = React.createClass({
        getDefaultProps: function () {
            return {
                text: "",
            };
        },
        render: function () {
            return React.DOM.div(null,
                React.DOM.textarea({
                    defaultValue: this.props.text,
                }),
                React.DOM.h3(null, this.props.text.length)
            )
        }
    });
    this.randerTestComponent2 = () => {
        ReactDOM.render(React.createElement(this.TestAreaComponent2, { text: "David" }), document.getElementById("phwang_prelude"));
    };
    this.objectName = () => "TestHtmlObject";
    this.rootObject = () => this.theRootObject;
    this.phwangObject = () => this.rootObject().phwangObject();
    this.debug = (debug_val, str1_val, str2_val) => { if (debug_val) { this.logit(str1_val, str2_val); } };
    this.logit = (str1_val, str2_val) => { this.logit_(this.objectName() + "." + str1_val, str2_val); };
    this.abend = (str1_val, str2_val) => { this.abend_(this.objectName() + "." + str1_val, str2_val); };
    this.logit_ = (str1_val, str2_val) => { this.phwangObject().LOG_IT(str1_val, str2_val); };
    this.abend_ = (str1_val, str2_val) => { this.phwangObject().ABEND(str1_val, str2_val); };
    this.init__(root_object_val);
}
/*
  Copyrights reserved
  Written by Paul Hwang
*/

class LoginHtmlClass extends React.Component {
    render() {
        return React.createElement(
            "body",
            null,
            React.createElement(
                "h2",
                null,
                "Account Sign In"
            ),
            React.createElement(
                "section",
                { "className": "login_section" },
                React.createElement(
                    "p",
                    null,
                    "Name:",
                    React.createElement("input", {
                        type: "text",
                        "className": "login_name",
                        placeholder: "Enter your name"
                    })
                ),
                React.createElement(
                    "p",
                    null,
                    "Password:",
                    React.createElement("input", {
                        type: "text",
                        "className": "login_password",
                        placeholder: "Enter your password"
                    })
                ),
                React.createElement(
                    "button",
                    { "className": "login_button" },
                    "Login"
                )
            )
        );
    }
}


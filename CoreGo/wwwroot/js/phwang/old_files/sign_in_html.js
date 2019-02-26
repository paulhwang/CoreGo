/*
  Copyrights reserved
  Written by Paul Hwang
*/

class SignInComponentClass extends React.Component {
    render() {
        return React.createElement(
            "section",
            { "className": "sign_in_section" },
            React.createElement(
                "h2",
                null,
                "Account Sign In"
            ),

            React.createElement(
                "p",
                null,
                "Name:",
                React.createElement("input", {
                    type: "text",
                    "className": "sign_in_name",
                    placeholder: "Enter your account name"
                })
            ),

            React.createElement(
                "p",
                null,
                "Password:",
                React.createElement("input", {
                    type: "text",
                    "className": "sign_in_password",
                    placeholder: "Enter your password"
                })
            ),

            React.createElement(
                "button",
                { "className": "sign_in_button" },
                "Sign in"
            )
        );
    }
}

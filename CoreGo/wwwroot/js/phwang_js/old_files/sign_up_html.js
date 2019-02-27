/*
  Copyrights reserved
  Written by Paul Hwang
*/

class SignUpComponentClass extends React.Component {
    render() {
        return React.createElement(
            "section",
            { "className": "sign_up_section" },
            React.createElement(
                "h2",
                null,
                "Account Sign Up"
            ),

            React.createElement(
                "p",
                null,
                "Name:",
                React.createElement("input", {
                    type: "text",
                    "className": "sign_up_name",
                    placeholder: "Enter your account name"
                })
            ),

            React.createElement(
                "p",
                null,
                "Password:",
                React.createElement("input", {
                    type: "text",
                    "className": "sign_up_password",
                    placeholder: "Enter your password"
                })
            ),

            React.createElement(
                "p",
                null,
                "email:",
                React.createElement("input", {
                    type: "text",
                    "className": "sign_up_email",
                    placeholder: "Enter your email"
                })
            ),

            React.createElement(
                "button",
                { "className": "sign_up_button" },
                "Sign up"
            )
        );
    }
}

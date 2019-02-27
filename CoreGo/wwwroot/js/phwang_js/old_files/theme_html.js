/*
  Copyrights reserved
  Written by Paul Hwang
*/

class ThemeComponentClass extends React.Component {
    render() {
        return React.createElement("section", { "className": "theme_section" },
            React.createElement("h1", null, "Select the Theme"),
            React.createElement("button", { "className": "go_button" }, "Play Go"),
       );
    }
}


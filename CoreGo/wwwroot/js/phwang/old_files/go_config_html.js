/*
  Copyrights reserved
  Written by Paul Hwang
*/

class GoConfigComponentClass extends React.Component {
    render() {
        return React.createElement(
            "body",
            null,
            React.createElement(
                "h2",
                null,
                "GoSetup"
            ),

            React.createElement(
                "section",
                { "className": "config_section" },
                React.createElement(
                    "p",
                    { "className": "peer_name_paragraph" },
                    "Peer Name:",
                    React.createElement(
                        "select",
                        { "name": "opponent" },
                        null
                    )
                ),

                React.createElement(
                    "p",
                    { "className": "peer_game_paragraph" },
                    "Game",
                    React.createElement(
                        "select",
                        null,
                        React.createElement("option", { "value": "Go" }, "Go"),
                        React.createElement("option", { "value": "game1" }, "game1"),
                        React.createElement("option", { "value": "game2" }, "game2")
                    ),
                    React.createElement(
                        "button",
                        null,
                        "Select"
                    )
                ),

                React.createElement(
                    "section",
                    { "className": "go_config_section" },
                    React.createElement(
                        "p",
                        null,
                        "Board Size:",
                        React.createElement(
                            "select",
                            { "className": "board_size" },
                            React.createElement("option", { "value": "19" }, "19x19"),
                            React.createElement("option", { "value": "13" }, "13x13"),
                            React.createElement("option", { "value":  "9" }, "9x9")
                        )
                    ),

                    React.createElement(
                        "p",
                        null,
                        "Stone Color:",
                        React.createElement(
                            "select",
                            { "className": "stone_color" },
                            React.createElement("option", { "value": "black" }, "Black"),
                            React.createElement("option", { "value": "white" }, "White"),
                        )
                    ),

                    React.createElement(
                        "p",
                        null,
                        "Komi:",
                        React.createElement(
                            "select",
                            { "className": "komi" },
                            React.createElement("option", { "value": "0" }, 0.5),
                            React.createElement("option", { "value": "4" }, 4.5),
                            React.createElement("option", { "value": "5" }, 5.5),
                            React.createElement("option", { "value": "6" }, 6.5),
                            React.createElement("option", { "value": "7" }, 7.5),
                            React.createElement("option", { "value": "8" }, 8.5)
                        )
                    ),

                    React.createElement(
                        "p",
                        null,
                        "Handicap:",
                        React.createElement(
                            "select",
                            { "className": "handicap" },
                            React.createElement("option", { "value": "0" }, 0),
                            React.createElement("option", { "value": "2" }, 2),
                            React.createElement("option", { "value": "3" }, 3),
                            React.createElement("option", { "value": "4" }, 4),
                            React.createElement("option", { "value": "5" }, 5),
                            React.createElement("option", { "value": "6" }, 6),
                            React.createElement("option", { "value": "7" }, 7),
                            React.createElement("option", { "value": "8" }, 8),
                            React.createElement("option", { "value": "9" }, 9),
                            React.createElement("option", { "value": "10" }, 10),
                            React.createElement("option", { "value": "11" }, 11),
                            React.createElement("option", { "value": "12" }, 12),
                            React.createElement("option", { "value": "13" }, 13),
                        )
                    )
                ),

                React.createElement(
                    "button",
                    { "className": "config_button" },
                    "Connect"
                )
            )
        );
    }
}


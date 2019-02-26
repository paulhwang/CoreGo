/*
  Copyrights reserved
  Written by Paul Hwang
*/
/*
<body>
    <section class="go_canvas_section">
        <canvas id="go_canvas">
    </section>
        <section>
            <p id="black_score">Black: 0</p>
        </section>
        <section>
            <p id="white_score">White: 0</p>
        </section>
</body>
*/


class GoGameComponentClass extends React.Component {
    render() {
        return React.createElement(
            "body",
            null,
            React.createElement(
                "section",
                null,
                React.createElement(
                    "canvas",
                    { "id": "go_canvas" },
                    null
                )
            ),

            React.createElement(
                "section",
                null,
                React.createElement(
                    "p",
                    { "id": "black_score" },
                    "Black 0"
                ),
                React.createElement(
                    "p",
                    { "id": "white_score" },
                   "White 0"
                )
            )
        );
    }
}



const uri = "api/teams";
let todos = null;
function getCount(data) {
    const el = $("#counter");
    let name = "Team Has Played the Crazy88!";
    if (data) {
        if (data > 1) {
            name = "Teams Have Played the Crazy88!";
        }
        el.text(data + " " + name);
    } else {
        el.text("No " + name);
    }
}

$(document).ready(function () {
    getData();
});

function getData() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function (data) {
            const tBody = $("#todos");

            $(tBody).empty();

            getCount(data.length);
            var count = 1;
            $.each(data, function (key, item) {
                    const tr = $("<tr></tr>")
                    .append($("<td style='font-size: 30px; font-weight: bold;';></td>").text(count++))
                    .append($("<td></td>").text(item.teamName))
                    .append($("<td></td>").text(item.session))
                    .append($("<td></td>").text(item.score))
                    .append($("<img></img>").attr('src', item.photo))

                    .append(
                        $("<td></td>").append(
                        $("<button class='button'>Delete</button>").on("click", function () {
                                deleteItem(item.id);
                            })
                        )
                    );

                tr.appendTo(tBody);
            });

            todos = data;
        }
    });
}

function deleteItem(id) {
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        success: function (result) {
            var message = "Are you sure you want to delete this Team?";
            if (confirm(message)) {
                getData();
            }
        }
    });
}

$(".my-form").on("submit", function () {
    const item = {
        name: $("#edit-name").val(),
        isComplete: $("#edit-isComplete").is(":checked"),
        id: $("#edit-id").val()
    };

    $.ajax({
        url: uri + "/" + $("#edit-id").val(),
        type: "PUT",
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(item),
        success: function (result) {
            getData();
        }
    });

    closeInput();
    return false;
});

function closeInput() {
    $("#spoiler").css({ display: "none" });
}
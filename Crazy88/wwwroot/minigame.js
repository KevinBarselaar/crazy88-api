const uri = "api/minigames";

$(document).ready(function () {
    getData();
});

function getData() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function (data) {
            const tBody = $("#minigame-table");

            $(tBody).empty();


            $.each(data, function (key, item) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(item.id))
                    .append($("<td></td>").text(item.name))
                    .append($("<td></td>").text(item.description))
                    .append($("<td></td>").text(item.maxScore))
                    .append($("<td></td>").text(item.minScore))
                    .append($("<td></td>").text(item.qrValue))
                    .append($("<td></td>").text(item.location))
                    .append($("<td></td>").text(item.active))
                    //.append(
                    //    $("<td></td>").append(
                    //    $("<button class='button'>Edit</button>").on("click",
                    //            function() {
                    //                editField(item.id, item.expiringDateTime);
                    //            })
                    //    )
                    //)
                    .append(
                        $("<td></td>").append(
                        $("<button id='edit-button' class='button'>Delete</button>").on("click",
                                function () {
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

function editField(id, expiringDateTime) {
    document.getElementById(id)
        .innerHTML =
            "<form class = \"edit-form\" id = \"edit-form\">" +
                "<input type = 'hidden' id = 'edit-id' value = '" + id + "'>" +
                "<input type = 'datetime-local' id = 'edit-datetime' value = '" + expiringDateTime + "'><br>" +
                "<input type = 'submit' value = 'Save' class = 'button'>" +
                "<input type = 'button' value = 'Discard' class = 'button'>" +
            "</form>";
}

function addItem() {
    const item = {
        name: $("#name").val(),
        description: $("#description").val(),
        maxScore: $("#maxscore").val(),
        minScore: $("#minscore").val(),
        qrValue: $("#qrvalue").val(),
        location: $("#location").val(),
        active: true
    };

    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri,
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong!");
        },
        success: function (result) {
            getData();
            $("#name").val(),
            $("#description").val(),
            $("#maxscore").val(),
            $("#minscore").val(),
            $("#qrvalue").val(),
            $("#location").val()
        }
    });
}

function deleteItem(id) {
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        success: function (result) {
            var message = "Are you sure you want to delete these Minigame Settings?";
            if (confirm(message)) {
                getData();
            }
        }
    });
}

function editItem(id) {
    $.each(todos, function (key, item) {
        if (item.id === id) {
            $("#edit-datetime").val(item.expiringDateTime);
            $("#edit-id").val(item.id);
        }
    });
    $("#spoiler").css({ display: "inline-block" });
}

$(".edit-form").on("submit", function () {
    const item = {
        expiringDateTime: $("#edit-datetime").val(),
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
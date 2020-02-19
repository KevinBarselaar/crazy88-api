const uri = "api/sessions";

$(document).ready(function () {
    getData();
});

function getData() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function (data) {
            const tBody = $("#session-table");

            $(tBody).empty();


            $.each(data, function (key, item) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(item.id))
                    .append($("<td class='expiring-date'></td>").text(item.expiringDateTime))
                    .append($("<td></td>").text(item.playCode))
                    .append(
                        $("<td></td>").append(
                            $("<button class='button'>Edit</button>").on("click",
                                function () {
                                    editItem(item.id);
                                })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button class='button'>Delete</button>").on("click",
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

function addItem() {
    const item = {
        duration: $("#duration").val()
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
            $("#duration").val("");
        }
    });
}

function deleteItem(id) {
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        success: function (result) {
            getData();
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
        //isComplete: $("#edit-isComplete").is(":checked"),
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
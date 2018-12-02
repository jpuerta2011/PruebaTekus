var clientViews = {
    endpoint: "",
    clientID: 0,
    initClientList: function (endpoint) {

        clientViews.endpoint = endpoint;
        clientViews.drawDataTable();
    },

    drawDataTable: function () {
        $.get(clientViews.endpoint + "/client/", function (d) {
            $('#clientList').DataTable({
                "data": d,
                "language": global.lang,
                "columns": [
                    { "data": "Name" },
                    { "data": "NIT" },
                    { "data": "Email" },
                    {
                        render: function (data, type, row) {
                            var div = $("<div></div>");
                            div.append($("<button></button>").addClass("btn btn-primary edit-client").html("Editar").attr("data-id", row.Id));
                            return div.html();
                        }
                    }
                ]
            });

            $(".edit-client").each(function (i, item) {
                var id = $(item).attr("data-id");
                $(item).on("click", function () {
                    window.location.href = "/Client/EditClient/" + id;
                });
            })

        });
    },

    saveClient: function () {
        var form = $("#formSaveClient");
        $.validator.unobtrusive.parse(form);
        form.validate({
            messages: {
                Name: "Debe ingresar el nombre del cliente",
                NIT: "Debe ingresar el NIT del cliente",
                Email: {
                    required: "Debe ingresar el email del cliente",
                    email: "Por favor ingrese un email valido",
                }
            }
        });
        if (form.valid()) {
            var data = form.serializeObject();
            data.ClientId = clientViews.clientID;

            $.post(clientViews.endpoint + "/client/", data,
                function (response) {
                    if (response.Success) {
                        window.location.href = "/Client/";
                    } else {
                        alert(response.Message);
                    }
                });

        }
    },

    initEdit: function () {
        $.get(clientViews.endpoint + "/client/" + clientViews.clientID,
            function (data) {
                $("#Name").val(data.Name);
                $("#NIT").val(data.NIT);
                $("#Email").val(data.Email);
            }).fail(function () {
                // TODO: i must add the fail process
            });
    }
}
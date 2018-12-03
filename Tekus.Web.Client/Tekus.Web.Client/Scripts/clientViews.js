var clientViews = {
    endpoint: "",
    clientID: 0,
    countriesServices: [],
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
    },

    initClientServices: function () {
        $.get(clientViews.endpoint + "/ClientService/?clientId=" + clientViews.clientID,
            function (data) {
                $('#clientServices').DataTable({
                    "data": data,
                    "language": global.lang,
                    "columns": [
                        { "data": "ServiceName" },
                        { "data": "HourValue" },
                        {
                            render: function (data, type, row) {

                            }
                        },
                        {
                            render: function (data, type, row) {
                                var div = $("<div></div>");
                                div.append($("<button></button>").addClass("btn btn-primary edit-client").html("Editar").attr("data-id", row.Id));
                                return div.html();
                            }
                        }
                    ]
                });
            }).fail(function () {
                // TODO: i must add the fail process
            });

        $("#btnGuardar").on("click", function () {

        });
    },

    initAddClientService: function () {

        $("#Country").html("");
        $("#Service").html("");
        $("#tableBody").html("");

        $.get(clientViews.endpoint + "/client/" + clientViews.clientID,
            function (data) {
                $("#Client").val(data.Name);
            }
        );

        $.get(clientViews.endpoint + "/service/", function (data) {            
            $.each(data, function (i, value) {
                $("#Service").append("<option value='" + value.Id +"'>" + value.Name + "</option>");
            });
        });

        $.get(clientViews.endpoint + "/country/", function (data) {
            
            $.each(data, function (i, value) {
                $("#Country").append("<option value='" + value.CountryId + "'>" + value.Name + "</option>");
            });
        });

        $("#btnGuardar").on("click", function () {
            clientViews.saveClientService();
        });

        $("#btnAgregarPais").on("click", function () {
            clientViews.addCountry();
        });

        $("#formSaveClientService").submit(function (e) {
            e.preventDefault();
        });        
    },

    saveClientService: function () {
        var form = $("#formSaveClientService");
        $.validator.unobtrusive.parse(form);
        form.validate({
            messages: {
                Service: "Debe seleccionar el servicio",
                HourValue: "Debe ingresar el valor por hora del servicio"
            }
        });
        if (form.valid()) {
            var data = form.serializeObject();
            data.ClientId = clientViews.clientID;
            data.ServiceId = data.Service;
            data.Countries = clientViews.countriesServices;

            $.post(clientViews.endpoint + "/ClientService/", data,
                function (response) {
                    if (response.Success) {
                        window.location.href = "/Client/";
                    } else {
                        alert(response.Message);
                    }
                });

        }
    },

    addCountry: function () {
        if ($("#Country").val()) {
            clientViews.countriesServices.push({ "CountryId": $("#Country").val() });

            var tr = $("<tr></tr>");
            var tdCountryName = $("<td></td>").html($("#Country option[value='" + $("#Country").val() + "']").text());
            var tdCountryDelete = $("<td></td>").append($("<button></button>")
                .addClass("btn btn-danger delete-country").html("Eliminar").attr("data-id", (clientViews.countriesServices.length - 1))
                .on("click", function () {
                    clientViews.deleteCountry(this);
                })
            );
            tr.append(tdCountryName).append(tdCountryDelete);

            $("#tableBody").append(tr);
        }
    },

    deleteCountry: function (element) {
        var index = $(element).attr("data-id");
        array.splice(index, 1);
        $(elemen).parent().parent().remove();
    }
}
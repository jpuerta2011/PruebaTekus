var serviceViews = {
    endpoint: "",
    serviceID: 0,
    initServiceList: function (endpoint) {
        serviceViews.endpoint = endpoint;
        serviceViews.drawDataTable();
    },

    drawDataTable: function () {
        $.get(serviceViews.endpoint + "/service/", function (d) {
            $('#serviceList').DataTable({
                "data": d,
                "language": global.lang,
                "columns": [
                    { "data": "Name" },
                    {
                        render: function (data, type, row) {
                            var div = $("<div></div>");
                            div.append($("<button></button>").addClass("btn btn-primary edit-service").html("Editar").attr("data-id", row.Id));
                            return div.html();
                        }
                    }
                ]
            });

            $(".edit-service").each(function (i, item) {
                var id = $(item).attr("data-id");
                $(item).on("click", function () {
                    window.location.href = "/Service/EditService/" + id;
                });
            })

        });
    },

    saveService: function () {
        var form = $("#formSaveService");
        $.validator.unobtrusive.parse(form);
        form.validate({
            messages: {
                Name: "Debe ingresar el nombre del servicio"
            }
        });
        if (form.valid()) {
            var data = form.serializeObject();
            data.Id = serviceViews.serviceID;

            $.post(serviceViews.endpoint + "/service/", data,
                function (response) {
                    if (response.Success) {
                        window.location.href = "/service/";
                    } else {
                        alert(response.Message);
                    }
                });

        }
    },

    initEdit: function () {
        $.get(serviceViews.endpoint + "/service/" + serviceViews.serviceID,
            function (data) {
                $("#Name").val(data.Name);
            }).fail(function () {
                // TODO: i must add the fail process
            });
    }
}
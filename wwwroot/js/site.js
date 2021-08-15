$(function () {
    $("form#formCustomer").submit(function (e) {
        e.preventDefault();

        if ($(this).valid()) {
            var action = $(this).attr("action");
            var msg = "";

            var formData = {
                Codigo: parseInt($("#cliInputCod").val()),
                Nome: $("#cliInputName").val(),
                Cnpj: $("#cliInputCpf").val(),
                RamoAtividade: $("#cliInputRamoAtividade").val(),
                Funcionarios: $("#cliInputNumFunc").val(),
                Faturamento: $("#cliInputRenda").val(),
                Telefone: $("#cliInputTelefone").val(),
                TelefoneMovel: $("#cliInputMovel").val(),
                Endereco: $("#cliInputLogradouro").val(),
                CEP: $("#cliInputCodigoPostal").val(),
                Bairro: $("#cliInputBairro").val(),
                Cidade: $("#cliInputCidade").val(),
                Estado: $("#cliSelectEstado").val()
            };

            if (isNaN(formData.Codigo)) {
                formData.Codigo = 0;
            }
            
            $.ajax({
                type: "post",
                data: JSON.stringify(formData),
                url: action,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                accepts: "application/json",
                cache: false,
                beforeSend: function (request) {
                    request.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
                },
                success: function (response) {
                    if (response.success) {
                        parent.location.reload();
                    } else {
                        if (response.statusText != null) {
                            msg += "<div class=\"alert alert-warning\">";
                            msg += "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>";
                            msg += "<i class=\"icon fas fa-warning\"></i><span>" + response.statusText + "</span></div>";

                            $("#messageRequest").html(msg);
                        }
                    }
                },
                error: function (response) {
                    msg += "<div class=\"alert alert-warning\">";
                    msg += "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>";
                    msg += "<i class=\"icon fas fa-warning\"></i><span>" + response.responseText + "</span></div>";

                    $("#messageRequest").html(msg);
                }
            });
        }
    });

    $("form#formDelete").submit(function (e) {
        e.preventDefault();

        if ($(this).valid()) {
            $.ajax({
                type: "POST",
                url: "deleta-confirma",
                data: { codigo: $("#delValueCod").val() },
                success: function (response) {
                    if (response.success) {
                        parent.location.reload();
                    }
                },
                error: function (response) {
                    alert("Ocorreu um erro inesperado: " + response.responseText);
                }
            });
        }
    });
});
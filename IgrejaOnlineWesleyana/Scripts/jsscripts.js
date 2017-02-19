$(document).ready(function () {
    $("#CPFBusca").inputmask("mask", { "mask": "999.999.999-99" }, { reverse: true });
    $("#myModal")
        .on("shown.bs.modal",
            function() {
                $("#CPFBusca").focus();
            });
    $("#btnCPF").click(function (event) {
        event.preventDefault();
        $("#info-cpf").remove();
        $("#btnCPF").prop("disabled", true);
        $(".modal-body").append("<div class=\"loader\"></div>");
        $.ajax({
                url: "/Membros/BuscarIDPorCPF",
                type: "get",
                contentType: "application/json; charset=utf-8",
                data: { cpf: $("#CPFBusca").val() },
                success: function (data) {
                    console.log("success");
                    console.log(data.data);
                    window.location.href = "/Membros/AlterarFicha/"+data.data;
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    console.log("error");
                    console.log(xmlHttpRequest);
                    var a = xmlHttpRequest.responseJSON;
                    console.log(a);
                    $(".modal-body").append("<br><span id=\"info-cpf\" class=\"text-danger\" data-bind=\"text: erroValidacao\">CPF inválido ou inexistente</span>");
                },
                complete: function() {
                    console.log("oi");
                    $("#btnCPF").prop("disabled", false);
                    $(".loader").remove();

                }
            });
    });
});

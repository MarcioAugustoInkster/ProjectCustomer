$(function () {
    $(".pg-rel").on("click", function () {
        window.location.reload();
    });

    $(".new-cli").on("click", function() {
        var id = $(this).data("codigo");
        
        if (id > 0) {
            $("#regcustomerModal").load("adiciona-novo?codigo=" + id, function() {
                $("#regcustomerModal").modal();
            });
        } else {
            $("#regcustomerModal").load("adiciona-novo", function() {
                $("#regcustomerModal").modal();
            });
        }
    });

    $(".del-cli").on("click", function () {
        var id = $(this).data("regis");

        if (id > 0) {
            $("#delcustomerModal").load("deleta-cliente?codigo=" + id, function () {
                $("#delcustomerModal").modal();
            });
        }
    });
});
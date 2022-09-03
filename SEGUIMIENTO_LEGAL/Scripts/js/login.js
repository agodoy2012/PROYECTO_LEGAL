


$("#btn_ingreso").click(function (e) {
    e.preventDefault();

    var datos = { login: $("#username").val(), pass: $("#pass").val()};

    $.ajax({
        type: "POST",
        url: "Ajax/Conexion_Ajax.aspx/validar_ingreso",
        data: JSON.stringify(datos),
        contentType: "application/json; chartset=utf-8",
        processData: false,
        dataType: "json",
        success: function (respuesta) {
            debugger;
            var respuest= respuesta;
            if (respuesta["d"] == true) {

                window.location.href = 'Inicio';

            } else {

                swal({
                    type: 'error',
                    title: 'Oops...',
                    text: 'No tienes los permisos para acceder!'
                });

                $("#username").val("");
                $("#pass").val("")
            }

        }

    });
   


})
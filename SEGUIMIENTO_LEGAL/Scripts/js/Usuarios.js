init();

function init() {
    $("#control_usuarios").addClass("active");
    $("#inicio").removeClass("active");
}

$("#btn_ingresar").click(function (e) {
    e.preventDefault();

    var datos = { nombre_completo: $("#nombre_completo").val(), usuario: $("#usuario").val(), clave: $("#clave").val(), perfil: $("#perfil_usuario").val(), pais: $("#pais").val()};

    $.ajax({
        type: "POST",
        url: "Ajax/Conexion_Ajax.aspx/registrar_usuario",
        data: JSON.stringify(datos),
        contentType: "application/json; chartset=utf-8",
        processData: false,
        dataType: "json",
        success: function (respuesta) {

            console.log(respuesta);

            if (respuesta["d"] > 0) {

                swal({
                    title: 'Buen Trabajo!',
                    text: 'Se ha registrado un nuevo usuario!',
                    type: 'success'
                }).then((result) => {
                    location.reload(true);
                });

                

            } else {
                swal({
                    title: 'Disculpas!',
                    text: 'No se logró registrar al usuario!',
                    type: 'dander'
                }).then((result) => {
                    location.reload(true);
                });
            }

        }

    });


})
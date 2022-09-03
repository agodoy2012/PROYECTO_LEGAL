init();

function init() {
    $("#listas").addClass("active");
    $("#list_rob").addClass("active");
    $("#inicio").removeClass("active");

    cargar_informacion();
    cargar_etapas()
}

function cargar_informacion() {

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Ajax/Conexion_Ajax.aspx/mostrar_registros_robot",
        contentType: "application/json; chartset=utf-8",
        processData: false,
        success: function (data) {

            datatable = $('#lista_robot').DataTable({
                destroy: true,
                dom: 'Bfrtip',
                buttons: [
                    'pdf'
                ],
                data: data["d"],
                columns: [
                    { 'data': 'numero_operacion' },
                    { 'data': 'nombre_cliente' },
                    { 'data': 'identificacion' },
                    { 'data': 'lugar_trabajo' },
                    { 'data': 'mes' },
                    { 'data': 'anio' },
                    {
                        data: null, render: function (row) {
                            return '<a id="btn_aplicar" class="btn btn-primary" onclick=tramitar("' + row.numero_expediente + '")  ><i class="fa fa-sign-out"></i> Tramitar</a> ';

                            //return '<a id="btn_aplicar" class="btn btn-primary" onclick=tramitar("' + row.numero_expediente + '") data-toggle="modal" data-target="#tramitar_proceso"  ><i class="fa fa-sign-out"></i> Tramitar</a> ';

                        }

                    }

                ],

                "language": {

                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"

                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    }

                }

            });
        }
    });

}

function tramitar(numero_expediente) {


    swal({
        type: 'warning',
        title: 'Oops...',
        text: 'Esta opción se encuentra inhabilitada!'
    });

    /*

    $("#etapa").val(1);
    $("#num_expediente").val(numero_expediente);

    cargar_subetapas_iniciales($("#etapa").val());

    datos_extra_judicial();

    datos_presentacion_demanda();

    mostrar_oficiales();*/
}

function cargar_etapas() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Ajax/Conexion_Ajax.aspx/obtener_etapas",
        contentType: "application/json; chartset=utf-8",
        processData: false,
        success: function (data) {

            //console.log(data);

            $("#etapa").empty();

            for (var i = 0; i < data["d"].length; i++) {

                document.getElementById("etapa").innerHTML += "<option value='" + data["d"][i]["id_etapa"] + "'>" + data["d"][i]["nombre"] + "</option>";

            }

            cargar_subetapas_iniciales(data["d"][0]["id_etapa"]);

        }
    });
}

function cargar_subetapas_iniciales(etapa) {

    var datos = { etapa: etapa };

    $.ajax({
        type: "POST",
        url: "Ajax/Conexion_Ajax.aspx/obtener_reaciones_iniciales",
        data: JSON.stringify(datos),
        contentType: "application/json; chartset=utf-8",
        processData: false,
        dataType: "json",
        success: function (respuesta) {

            // console.log(respuesta["d"]);

            $("#subetapa").empty();

            for (var i = 0; i < respuesta["d"].length; i++) {

                document.getElementById("subetapa").innerHTML += "<option value='" + respuesta["d"][i]["id_subetapa"] + "'>" + respuesta["d"][i]["nombre"] + "</option>";

            }

        }

    })
}

function datos_extra_judicial() {

    if ($("#etapa").val() == 2) {

        $("#datos_arreglo_extra_judicial").show();

        mostar_tipo_pago();

    } else {

        $("#datos_arreglo_extra_judicial").hide();
    }
}

function datos_presentacion_demanda() {

    if ($("#etapa").val() == 3 && $("#subetapa").val() == 7) {

        $("#datos_presentacion_demanda").show();

        mostar_tipo_de_proceso();

        mostar_juzgados();

    } else {
        $("#datos_presentacion_demanda").hide();
    }
}

function mostrar_oficiales() {

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Ajax/Conexion_Ajax.aspx/mostrar_oficiales",
        contentType: "application/json; chartset=utf-8",
        processData: false,
        success: function (data) {

            console.log(data);

            $("#oficial").empty();

            for (var i = 0; i < data["d"].length; i++) {

                document.getElementById("oficial").innerHTML += "<option value='" + data["d"][i] + "'>" + data["d"][i] + "</option>";

            }

        }
    });

}

function mostar_tipo_pago() {

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Ajax/Conexion_Ajax.aspx/mostrar_tipo_pago",
        contentType: "application/json; chartset=utf-8",
        processData: false,
        success: function (data) {

            $("#tipo_pago").empty();

            for (var i = 0; i < data["d"].length; i++) {

                document.getElementById("tipo_pago").innerHTML += "<option value='" + data["d"][i]["id_tipo_pago"] + "'>" + data["d"][i]["nombre"] + "</option>";

            }

        }
    });
}

function mostar_tipo_de_proceso() {

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Ajax/Conexion_Ajax.aspx/mostrar_procesos_jodicial",
        contentType: "application/json; chartset=utf-8",
        processData: false,
        success: function (data) {

            $("#proceso_judicial").empty();

            for (var i = 0; i < data["d"].length; i++) {

                document.getElementById("proceso_judicial").innerHTML += "<option value='" + data["d"][i]["id"] + "'>" + data["d"][i]["nombre"] + "</option>";

            }

        }
    });


}

function mostar_juzgados() {

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Ajax/Conexion_Ajax.aspx/mostrar_juzgados",
        contentType: "application/json; chartset=utf-8",
        processData: false,
        success: function (data) {

            console.log(data);

            $("#juzgado").empty();

            for (var i = 0; i < data["d"].length; i++) {

                document.getElementById("juzgado").innerHTML += "<option value='" + data["d"][i] + "'>" + data["d"][i] + "</option>";

            }

        }
    });

}

$("#etapa").change(function () {

    cargar_subetapas_iniciales($("#etapa").val());

});

$("#etapa").change(function () {

    if ($("#etapa").val() == 2) {

        $("#datos_arreglo_extra_judicial").show();

        mostar_tipo_pago();


    } else {

        $("#datos_arreglo_extra_judicial").hide();
    }

});

$("#subetapa").change(function () {

    if ($("#etapa").val() == 3 && $("#subetapa").val() == 7) {

        $("#datos_presentacion_demanda").show();

        mostar_tipo_de_proceso();

        mostar_juzgados();

    } else {
        $("#datos_presentacion_demanda").hide();
    }

});

$("#btn_registrar").click(function (e) {
    e.preventDefault();



    /*int numero_expediente, string numero_operacion, string nombre_cliente, string nit, string dpi, int correlativo, int etapa, int subetapa,
            string archivos, int tipo_pago, string via_pago, decimal monto_cancelar, int tractos, int plazo, DateTime fecha_fin_pago, int proceso,
            string nombre_juzgado, string numero_proceso, string oficial, string observaciones*/

    if ($("#etapa").val() == 2) {

        if ($("#tipo_pago").val() != 1 || $("#monto_cancelar").val() != "") {

            var datos = {
                numero_expediente: $("#num_expediente").val(), etapa: $("#etapa").val(), subetapa: $("#subetapa").val(), archivos: $("#archivos").val(),
                tipo_pago: $("#tipo_pago").val(), via_pago: "ARREGLO EXTRA JUDICIAL", monto_cancelar: $("#monto_cancelar").val(), tractos: $("#tractos").val(),
                plazo: $("#plazo").val(), fecha_fin_pago: $("#fecha_fin_pago").val(), observaciones: $("#detalles").val()
            };

            $.ajax({
                type: "POST",
                url: "Ajax/Conexion_Ajax.aspx/actualizar_expediente_convenio",
                data: JSON.stringify(datos),
                contentType: "application/json; chartset=utf-8",
                processData: false,
                dataType: "json",
                success: function (respuesta) {

                    if (respuesta["d"] != -1) {

                        swal({
                            type: 'success',
                            title: 'Bien hecho!!',
                            text: 'Se ha confirmado la actualización del expediente!'
                        }).then((result) => {
                            location.reload(true);
                        });

                    } else {

                        swal({
                            type: 'warning',
                            title: 'Oops...',
                            text: 'No se logro almacenar la información!'
                        });
                    }

                }
            });

        } else {

            swal({
                type: 'warning',
                title: 'Oops...',
                text: 'Debes llenar la información solicitada!'
            });

        }

    } else if ($("#etapa").val() == 3 && $("#subetapa").val() == 7) {

        if ($("#proceso_judicial").val() != 1 || $("#juzgado").val() != 1 || $("#oficial").val() != 1) {

            var datos = {
                numero_expediente: $("#num_expediente").val(), etapa: $("#etapa").val(), subetapa: $("#subetapa").val(), archivos: $("#archivos").val(),
                proceso: $("#proceso_judicial").val(), nombre_juzgado: $("#juzgado").val(), numero_proceso: $("#numero_proceso").val(), oficial: $("#oficial").val(),
                observaciones: $("#detalles").val()
            };

            $.ajax({
                type: "POST",
                url: "Ajax/Conexion_Ajax.aspx/actualizar_expediente_demanda",
                data: JSON.stringify(datos),
                contentType: "application/json; chartset=utf-8",
                processData: false,
                dataType: "json",
                success: function (respuesta) {

                    if (respuesta["d"] != -1) {

                        swal({
                            type: 'success',
                            title: 'Bien hecho!!',
                            text: 'Se ha confirmado la actualización del expediente!'
                        }).then((result) => {
                            location.reload(true);
                        });

                    } else {

                        swal({
                            type: 'warning',
                            title: 'Oops...',
                            text: 'No se logro almacenar la información!'
                        });
                    }

                }
            });

        } else {

            swal({
                type: 'warning',
                title: 'Oops...',
                text: 'Debes llenar la información solicitada!'
            });

        }


    } else {

        var datos = {
            numero_expediente: $("#num_expediente").val(), etapa: $("#etapa").val(), subetapa: $("#subetapa").val(),
            archivos: $("#archivos").val(), observaciones: $("#detalles").val()
        };

        $.ajax({
            type: "POST",
            url: "Ajax/Conexion_Ajax.aspx/actualizar_expediente_etapas",
            data: JSON.stringify(datos),
            contentType: "application/json; chartset=utf-8",
            processData: false,
            dataType: "json",
            success: function (respuesta) {

                if (respuesta["d"] != -1) {

                    swal({
                        type: 'success',
                        title: 'Bien hecho!!',
                        text: 'Se ha confirmado la actualización del expediente!'
                    }).then((result) => {
                        location.reload(true);
                    });

                } else {

                    swal({
                        type: 'warning',
                        title: 'Oops...',
                        text: 'No se logro almacenar la información!'
                    });
                }

            }
        });


    }
});
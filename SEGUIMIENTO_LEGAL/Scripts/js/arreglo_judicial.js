init();

function init() {
    $("#listas").addClass("active");
    $("#list_pro").addClass("active");
    $("#inicio").removeClass("active");

    $("#pagos").hide();
    $("#datos_presentacion_demanda").hide();
    $("#mont_recib").hide();

    cargar_informacion();
    cargar_etapas();
}

function cargar_informacion() {

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Ajax/Conexion_Ajax.aspx/mostrar_arreglo_judicial",
        contentType: "application/json; chartset=utf-8",
        processData: false,
        success: function (data) {

            datatable = $('#lista_arreglo').DataTable({
                destroy: true,
                dom: 'Bfrtip',
                buttons: [
                    'pdf'
                ],
                data: data["d"],
                columns: [
                    { 'data': 'id' },
                    { 'data': 'numero_operacion' },
                    { 'data': 'nombre_cliente' },
                    { 'data': 'subetapa' },
                    {
                        data: null, render: function (row) {

                            return '<a id="btn_aplicar" class="btn btn-primary" onclick=tramitar("' + row.id + '") data-toggle="modal" data-target="#tramitar_proceso"  ><i class="fa fa-sign-out"></i> Tramitar</a>';

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
    $("#etapa").val(1);
    $("#num_expediente").val(numero_expediente);

    cargar_subetapas_iniciales($("#etapa").val());

    datos_extra_judicial();

    datos_presentacion_demanda();

    mostrar_oficiales();
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

    if ($("#etapa").val() == 2 || $("#etapa").val() == 3 || $("#etapa").val() == 4) {

        $("#pagos").show();

        mostar_tipo_pago();

    } else {

        $("#pagos").hide();
    }
}

//function datos_presentacion_demanda() {

//    if ($("#etapa").val() == 3 && $("#subetapa").val() == 7) {

//        $("#datos_presentacion_demanda").show();

//        mostar_tipo_de_proceso();

//        mostar_juzgados();

//    } else {
//        $("#datos_presentacion_demanda").hide();
//    }
//}

function mostrar_oficiales() {

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Ajax/Conexion_Ajax.aspx/mostrar_oficiales",
        contentType: "application/json; chartset=utf-8",
        processData: false,
        success: function (data) {


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

            //console.log(data);

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

        $("#pagos").show();

        mostar_tipo_pago();

        $("#mont_recib").hide();


    } else if ($("#etapa").val() == 4) {

        $("#pagos").show();

        mostar_tipo_pago();

        $("#mont_recib").hide();

    } else {

        $("#pagos").hide();

        $("#mont_recib").hide();
    }

});

//SUETAPA
$("#subetapa").change(function () {

    if ($("#etapa").val() == 3 && $("#subetapa").val() == 6) {

        $("#datos_presentacion_demanda").show();
        $("#mont_recib").hide();

        mostar_tipo_de_proceso();

        mostar_juzgados();

    } else if ($("#etapa").val() == 4 && $("#subetapa").val() == 14) {

        $("#pagos").hide();
        $("#mont_recib").hide();


    } else if ($("#etapa").val() == 4 && $("#subetapa").val() == 12) {

        $("#pagos").show();
        $("#mont_recib").hide();

        mostar_tipo_pago();

    }

    else if ($("#etapa").val() == 4 && $("#subetapa").val() == 13 || $("#subetapa").val() == 15 || $("#subetapa").val() == 16) {

        $("#mont_recib").show();

        $("#datos_presentacion_demanda").hide();
        $("#pagos").hide();

    } else {

        $("#datos_presentacion_demanda").hide();
        $("#mont_recib").hide();

    }

});


$("#btn_registrar").click(function (e) {
    debugger;
    if ($("#etapa").val() == 2) {

        if ($("#tipo_pago").val() != 1 || $("#monto_cancelar").val() != "") {

            var datos = {
                numero_expediente: $("#num_expediente").val(), etapa: $("#etapa").val(), subetapa: $("#subetapa").val(), archivos: $("#archivos").val(),
                tipo_pago: $("#tipo_pago").val(), via_pago: "ARREGLO EXTRA JUDICIAL", monto_mensual: $("#monto_mensual").val(), monto_cancelar: $("#monto_cancelar").val(), tractos: $("#tractos").val(),
                fecha_inicio_pago: $("#fecha_inicio_pago").val(), fecha_fin_pago: $("#fecha_fin_pago").val(), observaciones: $("#detalles").val()
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

    } else if ($("#etapa").val() == 4 && $("#subetapa").val() == 12) {

        if ($("#tipo_pago").val() != 1 || $("#monto_cancelar").val() != "") {

            var datos = {
                numero_expediente: $("#num_expediente").val(), etapa: $("#etapa").val(), subetapa: $("#subetapa").val(), archivos: $("#archivos").val(),
                tipo_pago: $("#tipo_pago").val(), via_pago: "ARREGLO EXTRA JUDICIAL", monto_mensual: $("#monto_mensual").val(), monto_cancelar: $("#monto_cancelar").val(), tractos: $("#tractos").val(),
                plazo: $("#plazo").val(), fecha_inicio_pago: $("#fecha_inicio_pago").val(), fecha_fin_pago: $("#fecha_fin_pago").val(), observaciones: $("#detalles").val()
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



    } else if ($("#etapa").val() == 4 && $("#subetapa").val() == 13 || $("#subetapa").val() == 15 || $("#subetapa").val() == 16) {

        //$("#mont_recib").show();

        var datos = {
            numero_expediente: $("#num_expediente").val(), etapa: $("#etapa").val(), subetapa: $("#subetapa").val(), monto: $("#monto_recibido").val(),
            archivos: $("#archivos").val(), observaciones: $("#detalles").val()
        };

        $.ajax({
            type: "POST",
            url: "Ajax/Conexion_Ajax.aspx/actualizar_expediente_monto_recibido",
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
    else if ($("#etapa").val() == 3 && $("#subetapa").val() == 7) {

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
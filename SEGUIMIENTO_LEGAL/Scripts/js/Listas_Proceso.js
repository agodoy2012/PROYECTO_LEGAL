init();

var etapa_consulta;
var opciones_desestimiento = new Array();
var opciones_notificacion = new Array();

function init() {

    etapa_consulta = getParameterByName('etapa');
    

    $("#listas").addClass("active");
    $("#list_pro").addClass("active");
    $("#inicio").removeClass("active");

    $("#pagos").hide();
    $("#datos_presentacion_demanda").hide();
    $("#mont_recib").hide();
    $("#campos_notificacion").hide();
    $("#mont_transaccion").hide();
    $("#campos_sentencia").hide();
    $(".admitida_detalles").addClass("hidden"); campos_notificacion



    if (etapa_consulta != "") {

        //var fecha_inicial = '1990-01-01';
        //var fecha_final = moment().format('YYYY-MM-DD');

        //var d = moment().format('YYYY-MM-DD');

       // console.log(d);
        var subetapa = 0;
        var estado = 1;

        //cargar_informacion_filtro(fecha_inicial, fecha_final, etapa_consulta, subetapa, estado)
        cargar_informacion_filtro('', '', '', etapa_consulta, subetapa, estado)

    } else {
        cargar_informacion();
    }

    
    cargar_etapas();
    hoyFecha();
}

function cargar_informacion() {

    $("#cargando").removeClass("hidden");

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Ajax/Conexion_Ajax.aspx/mostrar_expedientes_legales",
        contentType: "application/json; chartset=utf-8",
        processData: false,
        success: function (data) {

          //  console.log(data);

            datatable = $('#lista_procesos').DataTable({
                destroy: true,                
                data: data["d"],
                //dom: 'Bfrtip',
                //buttons: [
                //     'excel', 'pdf'
                //],
                columns: [
                    { 'data': 'numero_expediente' },
                    { 'data': 'numero_operacion' },
                    { 'data': 'numero_proceso' },
                    { 'data': 'nombre_cliente' },
                    { 'data': 'etapa' },
                    { 'data': 'subetapa' },
                    { 'data': 'fecha_incluye' },
                    {
                        data: null, render: function (row) {

                            var opciones = '';

                            if (row.estado == 1) {

                                opciones += '<a id="btn_aplicar" class="btn btn-primary" onclick=tramitar("' + row.numero_expediente + '") data-toggle="modal" data-target="#tramitar_proceso"  ><i class="fa fa-sign-out"></i> Tramitar</a>\n';

                            }

                            opciones += '<div class="btn-group">' +
                                '<button type = "button" class="btn btn-danger" > Opciones </button >' +
                                '<button type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-expanded="false">' +
                                '<span class="caret"></span>' +
                                '<span class="sr-only">Toggle Dropdown</span>' +
                                '</button>' +
                                '<ul class="dropdown-menu" role="menu">' +
                                '<li><a onclick=mostrar_historial("' + row.numero_expediente + '","' + row.numero_operacion + '") data-toggle="modal" data-target="#ver_historial">Historial</a></li>' +
                                '<li><a onclick=mostrar_informacion_demanda("' + row.numero_expediente + '","' + row.numero_operacion + '")  data-toggle="modal" data-target="#ver_detalles_demanda">Info. Demanda</a></li>';

                            if (row.estado == 1) {
                                opciones += '<li class="opciones-menu"><a onclick=anular_caso("' + row.numero_expediente + '","' + row.numero_operacion + '")  data-toggle="modal" data-target="#ventana_anular_caso">Rechazar Caso</a></li>' +
                                    '<li><a onclick=cerrar_caso("' + row.numero_expediente + '","' + row.numero_operacion + '")  data-toggle="modal" data-target="#ventana_cerrar_caso">Cerrar Caso</a></li>';
                            }
                                       
                                                    
                            opciones +=      '</ul>' +
                                        '</div >';


                            return opciones;

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
            $('label').addClass('form-inline');
            $('input[type="search"]').addClass('form-control redondeando  input-md');
            $('select').addClass('form-control input-md');

            $("#cargando").addClass("hidden");
        }
    });
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
            $("#etapa_filtro").empty();

            document.getElementById("etapa_filtro").innerHTML += "<option value=''>SELECCIONE...</option>";

            for (var i = 0; i < data["d"].length; i++) {

                document.getElementById("etapa").innerHTML += "<option value='" + data["d"][i]["id_etapa"] + "'>" + data["d"][i]["nombre"] + "</option>";
                document.getElementById("etapa_filtro").innerHTML += "<option value='" + data["d"][i]["id_etapa"] + "'>" + data["d"][i]["nombre"] + "</option>";
            }

            if (etapa_consulta != "") {

                $("#etapa_filtro").val(etapa_consulta);
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
            $("#subetapa_filtro").empty();

            document.getElementById("subetapa_filtro").innerHTML += "<option value=''>SELECCIONE...</option>";

            for (var i = 0; i < respuesta["d"].length; i++) {

                document.getElementById("subetapa").innerHTML += "<option value='" + respuesta["d"][i]["id_subetapa"] + "'>" + respuesta["d"][i]["nombre"] + "</option>";
                document.getElementById("subetapa_filtro").innerHTML += "<option value='" + respuesta["d"][i]["id_subetapa"] + "'>" + respuesta["d"][i]["nombre"] + "</option>";
            }

        }

    })
}

$("#etapa").change(function () {

    cargar_subetapas_iniciales($("#etapa").val());

});

$("#etapa_filtro").change(function () {

    if ($("#etapa_filtro").val() != "") {

        cargar_subetapas_iniciales($("#etapa_filtro").val());

    } else {
        $("#subetapa_filtro").empty();
        document.getElementById("subetapa_filtro").innerHTML += "<option value=''>SELECCIONE...</option>";
    }
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

function datos_extra_judicial() {

    if ($("#etapa").val() == 2 || $("#etapa").val() == 3 || $("#etapa").val() == 4) {

        $("#pagos").show();

        mostar_tipo_pago();

    } else {

        $("#pagos").hide();
    }
}
//SUETAPA

$("#subetapa").change(function () {

    if ($("#etapa").val() == 3 && $("#subetapa").val() == 6) {

        $("#datos_presentacion_demanda").show();
        $("#mont_recib").hide();
        $("#campos_notificacion").hide();
        $("#campos_sentencia").hide();
        $("#mont_transaccion").hide();

        mostar_tipo_de_proceso();

        mostar_juzgados();

    } else if ($("#etapa").val() == 3 && $("#subetapa").val() == 8) {

        $("#campos_notificacion").show();
    }

    else if ($("#etapa").val() == 4 && $("#subetapa").val() == 13 || $("#subetapa").val() == 14) {

        $("#pagos").hide();
        $("#mont_recib").hide();
        $("#campos_notificacion").hide();
        $("#campos_sentencia").hide();
        $("#mont_transaccion").hide();


    }
    //else if ($("#etapa").val() == 4 && $("#subetapa").val() == 12) {

    //    $("#pagos").show();
    //    $("#mont_recib").hide();
    //    $("#campos_notificacion").hide();

    //    mostar_tipo_pago();

    //}

    else if ($("#etapa").val() == 4 && $("#subetapa").val() == 12 || $("#subetapa").val() == 15) {

        $("#mont_recib").show();

        $("#mont_transaccion").hide();
        $("#datos_presentacion_demanda").hide();
        $("#pagos").hide();
        $("#campos_notificacion").hide(); 
        $(".admitida_detalles").removeClass("hidden");
        $("#campos_sentencia").hide();
        $("#mont_transaccion").hide();

    }

    else if ($("#etapa").val() == 4 && $("#subetapa").val() == 16 ) {

        $("#mont_transaccion").show();

        $("#mont_recib").hide();
        $("#datos_presentacion_demanda").hide();
        $("#pagos").hide();
        $("#campos_notificacion").hide();
        $("#campos_sentencia").hide();

    }
    else if ($("#etapa").val() == 5 && $("#subetapa").val() == 23) {

        $("#campos_sentencia").show();

        $("#mont_transaccion").hide();
        $("#mont_recib").hide();
        $("#datos_presentacion_demanda").hide();
        $("#pagos").hide();
        $("#campos_notificacion").hide();
    }
    else {

        $("#datos_presentacion_demanda").hide();
        $("#mont_recib").hide();
        $("#campos_notificacion").hide();
        $("#campos_sentencia").hide();
        $("#mont_transaccion").hide();

    }

});

$("#admitida").change(function () {

    if ($("#admitida").val() == 1) {
        $(".admitida_detalles").removeClass("hidden");
    } else {
        $(".admitida_detalles").addClass("hidden");
    }
})

$("input[name='optionsAdmitida']").click(function () {

    var opciones = new Array();
    var n = jQuery(".checkboxClassAd:checked").length;
    if (n > 0) {
        jQuery(".checkboxClassAd:checked").each(function () {

            opciones.push($(this).val());

            if ($(this).val() == "Otro") {

                $("#option_otro_admitida").removeAttr("Disabled", "");

            } else {

                $("#option_otro_admitida").attr("Disabled", "");

            }

        });
    } else {

        $("#option_otro_admitida").attr("Disabled", "");

    }

    opciones_notificacion = opciones;
})

/*Obtiene los valores seleccionados en un checkbox*/
$("input[name='optionsDesestimiento']").click(function () {
    var opciones = new Array();
    var n = jQuery(".checkboxClass:checked").length;
    if (n > 0) {
        jQuery(".checkboxClass:checked").each(function () {

            opciones.push($(this).val());

            if ($(this).val() == "Otro") {

                $("#option_otro_desestimiento").removeAttr("Disabled", "");

            } else {

                $("#option_otro_desestimiento").attr("Disabled", "");
            }

        });
    } else {
        $("#option_otro_desestimiento").attr("Disabled", "");
    }

    opciones_desestimiento = opciones;
    //console.log(opciones);
})


function datos_presentacion_demanda() {

    if ($("#etapa").val() == 3 && $("#subetapa").val() == 6) {

        $("#datos_presentacion_demanda").show();

        mostar_tipo_de_proceso();

        mostar_juzgados();

    } else {
        $("#datos_presentacion_demanda").hide();
    }
}

function tramitar(numero_expediente) {
    $("#etapa").val(1);
    $("#num_expediente").val(numero_expediente);

    $("#pagos").hide();
    $("#datos_presentacion_demanda").hide();
    $("#mont_recib").hide();
    $("#campos_notificacion").hide();
    $("#mont_transaccion").hide();
    $("#campos_sentencia").hide();
    $(".admitida_detalles").addClass("hidden");

    cargar_subetapas_iniciales($("#etapa").val());

    datos_extra_judicial();

    datos_presentacion_demanda();

    mostrar_oficiales();
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

function mostrar_historial(numero_expediente, numero_operacion) {
    debugger;

    $("#num_exp").html(numero_expediente);
    $("#num_ope").html(numero_operacion);
    //$("#client").html(nombre_cliente);

    var datos = { numero_expediente: numero_expediente };

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Ajax/Conexion_Ajax.aspx/mostrar_historial",
        data: JSON.stringify(datos),
        contentType: "application/json; chartset=utf-8",
        processData: false,
        success: function (data) {

            datatable_historial = $('#historial').DataTable({
                destroy: true,
                order: false,
                data: data["d"],
                columns: [
                    { 'data': 'nombre_etapa' },
                    { 'data': 'nombre_subetapa' },
                    { 'data': 'detalles'},
                    { 'data': 'fecha_registro' }
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
            $('label').addClass('form-inline');
            $('input[type="search"]').addClass('form-control redondeando  input-md');
            $('select').addClass('form-control input-md');
        }
    });

}

function mostrar_informacion_demanda(numero_expediente, numero_operacion) {

    var datos = { numero_operacion: numero_operacion }

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Ajax/Conexion_Ajax.aspx/mostrar_informacion_demanda",
        data: JSON.stringify(datos),
        contentType: "application/json; chartset=utf-8",
        processData: false,
        success: function (data) {

            console.log(data)

            $("#num_exp_").val(numero_expediente);
            $("#num_ope_").val(numero_operacion);
            $("#nom_cli_").val(data["d"]["nombre_cliente"]);
            $("#etapa_").val(data["d"]["etapa"]);
            $("#subetapa_").val(data["d"]["subetapa"]);

            $("#tipo_pago_").val(data["d"]["nombre_tipo_pago"]);
            $("#monto_canc_").val(data["d"]["monto_cancelar"]);
            $("#monto_mens_").val(data["d"]["monto_mensual"]);
            $("#tract_").val(data["d"]["tratos"]);
            $("#fin_ini_").val(data["d"]["fecha_ini_pago"]);
            $("#fin_pag_").val(data["d"]["fecha_fin_pago"]);

            $("#num_proc_").val(data["d"]["numero_proceso"]);
            $("#nomb_juzg_").val(data["d"]["nombre_juzgado"]);            
            $("#tipo_proc_").val(data["d"]["nombre_proceso_judicial"]);
            $("#ofic_acarg_").val(data["d"]["oficial"]);

            $("#observaciones_").html(data["d"]["detalles"]);

        }

    });

}

/* REGISTRO DE CASOS */
$("#btn_registrar").click(function (e) {
    //e.preventDefault();

    debugger;


    var etapa = $("#etapa").val();
    var subetapa = $("#subetapa").val()

    var data = new FormData();
    data.append("num_expediente", $("#num_expediente").val());
    data.append("etapa", etapa);
    data.append("subetapa", subetapa);
    data.append("detalles", $("#detalles").val());


    var files = $("#archivos").get(0).files;
    // Agregar los documentos al form data
    if (files.length > 0) {

        for (var i = 0; i < files.length; i++) {

            data.append("Uploaded_Document", files[i]);
        }

    } else {
        data.append("Uploaded_Document", "");
    }

    console.log('hola');
    switch (etapa) {
        case '1':
            debugger;
            if (subetapa == 1 || subetapa == 2 || subetapa == 3 || subetapa == 24) {

                $.ajax({
                    type: "POST",
                    url: "Ajax/Servicio_Conexion.asmx/modificar_caso_sin_campos_extra",
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (respuesta) {

                        if (respuesta != "") {

                            swal({
                                type: 'success',
                                title: 'Bien hecho!!',
                                text: 'Se ha modificado el caso correctamente'
                            }).then((result) => {
                                location.reload(true);
                            });

                        } else {

                            swal({
                                type: 'error',
                                title: 'Oops...',
                                text: 'No se logró modificar el expediente!'
                            });
                        }
                    }
                    });
            }
            
            break;
        case '2':

            data.append("tipo_pago", $("#tipo_pago").val());
            data.append("via_pago", $("#via_pago").val());
            data.append("monto_mensual", $("#monto_mensual").val());
            data.append("monto_cancelar", $("#monto_cancelar").val());
            data.append("tractos", $("#tractos").val());
            data.append("fecha_inicio_pago", $("#fecha_inicio_pago").val());
            data.append("fecha_fin_pago", $("#fecha_fin_pago").val());

            $.ajax({
                type: "POST",
                url: "Ajax/Servicio_Conexion.asmx/modificar_caso_convenio",
                data: data,
                contentType: false,
                processData: false,
                success: function (respuesta) {

                    if (respuesta != "") {

                        swal({
                            type: 'success',
                            title: 'Bien hecho!!',
                            text: 'Se ha modificado el caso correctamente'
                        }).then((result) => {
                            location.reload(true);
                        });

                    } else {

                        swal({
                            type: 'error',
                            title: 'Oops...',
                            text: 'No se logró modificar el expediente!'
                        });
                    }

                }
            }); 

            break;
        case '3':
            if (subetapa == 5 || subetapa == 7 || subetapa == 9 || subetapa == 10) {

                $.ajax({
                    type: "POST",
                    url: "Ajax/Servicio_Conexion.asmx/modificar_caso_sin_campos_extra",
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (respuesta) {

                        if (respuesta != "") {

                            swal({
                                type: 'success',
                                title: 'Bien hecho!!',
                                text: 'Se ha modificado el caso correctamente'
                            }).then((result) => {
                                location.reload(true);
                            });

                        } else {

                            swal({
                                type: 'error',
                                title: 'Oops...',
                                text: 'No se logró modificar el expediente!'
                            });
                        }
                    }
                });

            } else if (subetapa == 8) {

                /*Modificacion de la notificacion*/
                var banco = 0;
                var salario = 0;
                var otro;

                var admitida = $("#admitida").val();

                data.append("admitida", admitida);

                if (admitida == 1) {

                    for (var i = 0; i < opciones_notificacion.length; i++) {

                        if (opciones_notificacion[i] == "Bancos") {
                            banco = 1;
                        }
                        if (opciones_notificacion[i] == "Salario") {
                            salario = 1;
                        }
                        if (opciones_notificacion[i] == "Otro") {
                            otro = $("#option_otro_admitida").val();
                        }
                    }

                    data.append("banco", banco);
                    data.append("salario", salario);
                    data.append("otro", otro);

                    $.ajax({
                        type: "POST",
                        url: "Ajax/Servicio_Conexion.asmx/modificar_caso_notificacion",
                        data: data,
                        contentType: false,
                        processData: false,
                        success: function (respuesta) {

                            if (respuesta != "") {

                                swal({
                                    type: 'success',
                                    title: 'Bien hecho!!',
                                    text: 'Se ha modificado el caso correctamente'
                                }).then((result) => {
                                    location.reload(true);
                                });

                            } else {

                                swal({
                                    type: 'error',
                                    title: 'Oops...',
                                    text: 'No se logró modificar el expediente!'
                                });
                            }
                        }
                    });


                } else if (admitida == 2) {

                    if ($("#detalles").val().trim().length > 0) {

                        var datos = { numero_expediente: $("#num_expediente").val(), justificacion_anulacion: $("#detalles").val().trim() }

                        $.ajax({
                            type: "POST",
                            url: "Ajax/Conexion_Ajax.aspx/anular_proceso",
                            data: JSON.stringify(datos),
                            contentType: "application/json; chartset=utf-8",
                            processData: false,
                            dataType: "json",
                            success: function (respuesta) {

                                if (respuesta["d"] != false) {

                                    swal({
                                        type: 'success',
                                        title: 'Bien hecho!!',
                                        text: 'Este caso se ha rechazado'
                                    }).then((result) => {
                                        location.reload(true);
                                    });

                                } else {

                                    swal({
                                        type: 'error',
                                        title: 'Oops...',
                                        text: 'No se pudo rechazar el caso!'
                                    });
                                }
                            }

                        });


                    } else {

                        swal({
                            type: 'warning',
                            title: 'Oops...',
                            text: 'No has llenado la jusficación para rechazar este caso!'
                        });
                    }



                }  

            } else if (subetapa == 6) {
                debugger;
                data.append("proceso_judicial", $("#proceso_judicial").val());
                data.append("juzgado", $("#juzgado").val());
                data.append("numero_proceso", $("#numero_proceso").val());
                data.append("oficial", $("#oficial").val());

                data.append("monto_demanda", $("#monto_demanda").val());
                console.log($("#monto_demanda").val());


                $.ajax({
                    type: "POST",
                    url: "Ajax/Servicio_Conexion.asmx/modificar_caso_asignacion_juzgado",
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (respuesta) {

                        if (respuesta != "") {

                            swal({
                                type: 'success',
                                title: 'Bien hecho!!',
                                text: 'Se ha modificado el caso correctamente'
                            }).then((result) => {
                                location.reload(true);
                            });

                        } else {

                            swal({
                                type: 'error',
                                title: 'Oops...',
                                text: 'No se logró modificar el expediente!'
                            });
                        }
                    }
                });

            }
            break;
        case '4':

            if (subetapa == 11 || subetapa == 13 || subetapa == 14) {

                $.ajax({
                    type: "POST",
                    url: "Ajax/Servicio_Conexion.asmx/modificar_caso_sin_campos_extra",
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (respuesta) {

                        if (respuesta != "") {

                            swal({
                                type: 'success',
                                title: 'Bien hecho!!',
                                text: 'Se ha modificado el caso correctamente'
                            }).then((result) => {
                                location.reload(true);
                            });

                        } else {

                            swal({
                                type: 'error',
                                title: 'Oops...',
                                text: 'No se logró modificar el expediente!'
                            });
                        }
                    }
                });

            } else if (subetapa == 12 || subetapa == 15) {

                data.append("monto_recibido", $("#monto_recibido").val());
                data.append("fecha_pago", $("#fecha_pago").val());

                $.ajax({
                    type: "POST",
                    url: "Ajax/Servicio_Conexion.asmx/modificar_caso_pago_recibido",
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (respuesta) {

                        if (respuesta != "") {

                            swal({
                                type: 'success',
                                title: 'Bien hecho!!',
                                text: 'Se ha modificado el caso correctamente'
                            }).then((result) => {
                                location.reload(true);
                            });

                        } else {

                            swal({
                                type: 'error',
                                title: 'Oops...',
                                text: 'No se logró modificar el expediente!'
                            });
                        }
                    }
                });

            } else if (subetapa == 16) {

                debugger;
                /*Modificacion de la notificacion*/
                var banco = 0;
                var salario = 0;
                var otro;

                var monto = $("#monto_recibido_transaccion").val();
                var fecha = $("#fecha_fin_transaccion").val();

                data.append("monto_recibido", monto);
                data.append("fecha_pago", fecha );

                for (var i = 0; i < opciones_desestimiento.length; i++) {

                    if (opciones_desestimiento[i] == "Bancos") {
                        banco = 1;
                    }
                    if (opciones_desestimiento[i] == "Salario") {
                        salario = 1;
                    }
                    if (opciones_desestimiento[i] == "Otro") {
                        otro = $("#option_otro_admitida").val();
                    }
                }

                data.append("banco", banco);
                data.append("salario", salario);
                data.append("otro", otro);


                $.ajax({
                    type: "POST",
                    url: "Ajax/Servicio_Conexion.asmx/modificar_caso_desestimiento",
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (respuesta) {

                        if (respuesta != "") {

                            swal({
                                type: 'success',
                                title: 'Bien hecho!!',
                                text: 'Se ha modificado el caso correctamente'
                            }).then((result) => {
                                location.reload(true);
                            });

                        } else {

                            swal({
                                type: 'error',
                                title: 'Oops...',
                                text: 'No se logró modificar el expediente!'
                            });
                        }
                    }
                });



            }

            break;
        case '5':

            if (subetapa == 17 || subetapa == 18 || subetapa == 19 || subetapa == 20 || subetapa == 21 || subetapa == 22) {

                $.ajax({
                    type: "POST",
                    url: "Ajax/Servicio_Conexion.asmx/modificar_caso_sin_campos_extra",
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (respuesta) {

                        if (respuesta != "") {

                            swal({
                                type: 'success',
                                title: 'Bien hecho!!',
                                text: 'Se ha modificado el caso correctamente'
                            }).then((result) => {
                                location.reload(true);
                            });

                        } else {

                            swal({
                                type: 'error',
                                title: 'Oops...',
                                text: 'No se logró modificar el expediente!'
                            });
                        }
                    }
                });

            } else if (subetapa == 23) {
                /*Modificacion caso en sentencia*/
                console.log($("#opciones_sentencia").val());
                data.append("opciones_sentencia", $("#opciones_sentencia").val());

                $.ajax({
                    type: "POST",
                    url: "Ajax/Servicio_Conexion.asmx/modificar_caso_sentencia",
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (respuesta) {

                        if (respuesta != "") {

                            swal({
                                type: 'success',
                                title: 'Bien hecho!!',
                                text: 'Se ha modificado el caso correctamente'
                            }).then((result) => {
                                location.reload(true);
                            });

                        } else {

                            swal({
                                type: 'error',
                                title: 'Oops...',
                                text: 'No se logró modificar el expediente!'
                            });
                        }
                    }
                });

            }

            break;
    }

    //if ($("#etapa").val() == 2) {

    //    if ($("#tipo_pago").val() != 1 || $("#monto_cancelar").val() != "") {

    //        var datos = {
    //            numero_expediente: $("#num_expediente").val(), etapa: $("#etapa").val(), subetapa: $("#subetapa").val(), archivos: $("#archivos").val(),
    //            tipo_pago: $("#tipo_pago").val(), via_pago: "ARREGLO EXTRA JUDICIAL", monto_mensual: $("#monto_mensual").val(), monto_cancelar: $("#monto_cancelar").val(), tractos: $("#tractos").val(),
    //            fecha_inicio_pago: $("#fecha_inicio_pago").val(), fecha_fin_pago: $("#fecha_fin_pago").val(), observaciones: $("#detalles").val()
    //        };

    //        $.ajax({
    //            type: "POST",
    //            url: "Ajax/Conexion_Ajax.aspx/actualizar_expediente_convenio",
    //            data: JSON.stringify(datos),
    //            contentType: "application/json; chartset=utf-8",
    //            processData: false,
    //            dataType: "json",
    //            success: function (respuesta) {

    //                if (respuesta["d"] != -1) {

    //                    swal({
    //                        type: 'success',
    //                        title: 'Bien hecho!!',
    //                        text: 'Se ha confirmado la actualización del expediente!'
    //                    }).then((result) => {
    //                        location.reload(true);
    //                    });

    //                } else {

    //                    swal({
    //                        type: 'warning',
    //                        title: 'Oops...',
    //                        text: 'No se logro almacenar la información!'
    //                    });
    //                }

    //            }
    //        });

    //    } else {

    //        swal({
    //            type: 'warning',
    //            title: 'Oops...',
    //            text: 'Debes llenar la información solicitada!'
    //        });

    //    }

    //} else if ($("#etapa").val() == 4 && $("#subetapa").val() == 12 ) {

    //    if ($("#tipo_pago").val() != 1 || $("#monto_cancelar").val() != "") {

    //        var datos = {
    //            numero_expediente: $("#num_expediente").val(), etapa: $("#etapa").val(), subetapa: $("#subetapa").val(), archivos: $("#archivos").val(),
    //            tipo_pago: $("#tipo_pago").val(), via_pago: "ARREGLO EXTRA JUDICIAL", monto_mensual: $("#monto_mensual").val(), monto_cancelar: $("#monto_cancelar").val(), tractos: $("#tractos").val(),
    //            plazo: $("#plazo").val(), fecha_inicio_pago: $("#fecha_inicio_pago").val(), fecha_fin_pago: $("#fecha_fin_pago").val(), observaciones: $("#detalles").val()
    //        };

    //        $.ajax({
    //            type: "POST",
    //            url: "Ajax/Conexion_Ajax.aspx/actualizar_expediente_convenio",
    //            data: JSON.stringify(datos),
    //            contentType: "application/json; chartset=utf-8",
    //            processData: false,
    //            dataType: "json",
    //            success: function (respuesta) {

    //                if (respuesta["d"] != -1) {

    //                    swal({
    //                        type: 'success',
    //                        title: 'Bien hecho!!',
    //                        text: 'Se ha confirmado la actualización del expediente!'
    //                    }).then((result) => {
    //                        location.reload(true);
    //                    });

    //                } else {

    //                    swal({
    //                        type: 'warning',
    //                        title: 'Oops...',
    //                        text: 'No se logro almacenar la información!'
    //                    });
    //                }

    //            }
    //        });

    //    } else {

    //        swal({
    //            type: 'warning',
    //            title: 'Oops...',
    //            text: 'Debes llenar la información solicitada!'
    //        });

    //    }



    //} else if ($("#etapa").val() == 4 && $("#subetapa").val() == 13 || $("#subetapa").val() == 15 || $("#subetapa").val() == 16) {

    //    //$("#mont_recib").show();

    //    var datos = {
    //        numero_expediente: $("#num_expediente").val(), etapa: $("#etapa").val(), subetapa: $("#subetapa").val(), monto: $("#monto_recibido").val(),
    //        archivos: $("#archivos").val(), observaciones: $("#detalles").val()
    //    };

    //    $.ajax({
    //        type: "POST",
    //        url: "Ajax/Conexion_Ajax.aspx/actualizar_expediente_monto_recibido",
    //        data: JSON.stringify(datos),
    //        contentType: "application/json; chartset=utf-8",
    //        processData: false,
    //        dataType: "json",
    //        success: function (respuesta) {

    //            if (respuesta["d"] != -1) {

    //                swal({
    //                    type: 'success',
    //                    title: 'Bien hecho!!',
    //                    text: 'Se ha confirmado la actualización del expediente!'
    //                }).then((result) => {
    //                    location.reload(true);
    //                });

    //            } else {

    //                swal({
    //                    type: 'warning',
    //                    title: 'Oops...',
    //                    text: 'No se logro almacenar la información!'
    //                });
    //            }

    //        }
    //    });



    //}
    //else if ($("#etapa").val() == 3 && $("#subetapa").val() == 6) {

    //    if ($("#proceso_judicial").val() != 1 || $("#juzgado").val() != 1 || $("#oficial").val() != 1) {

    //        var datos = {
    //            numero_expediente: $("#num_expediente").val(), etapa: $("#etapa").val(), subetapa: $("#subetapa").val(), archivos: $("#archivos").val(),
    //            proceso: $("#proceso_judicial").val(), nombre_juzgado: $("#juzgado").val(), numero_proceso: $("#numero_proceso").val(), oficial: $("#oficial").val(),
    //            observaciones: $("#detalles").val()
    //        };

    //        $.ajax({
    //            type: "POST",
    //            url: "Ajax/Conexion_Ajax.aspx/actualizar_expediente_demanda",
    //            data: JSON.stringify(datos),
    //            contentType: "application/json; chartset=utf-8",
    //            processData: false,
    //            dataType: "json",
    //            success: function (respuesta) {

    //                if (respuesta["d"] != -1) {

    //                    swal({
    //                        type: 'success',
    //                        title: 'Bien hecho!!',
    //                        text: 'Se ha confirmado la actualización del expediente!'
    //                    }).then((result) => {
    //                        location.reload(true);
    //                    });

    //                } else {

    //                    swal({
    //                        type: 'warning',
    //                        title: 'Oops...',
    //                        text: 'No se logro almacenar la información!'
    //                    });
    //                }

    //            }
    //        });

    //    } else {

    //        swal({
    //            type: 'warning',
    //            title: 'Oops...',
    //            text: 'Debes llenar la información solicitada!'
    //        });

    //    }


    //} else {

    //    var datos = {
    //        numero_expediente: $("#num_expediente").val(), etapa: $("#etapa").val(), subetapa: $("#subetapa").val(),
    //        archivos: $("#archivos").val(), observaciones: $("#detalles").val()
    //    };

    //    $.ajax({
    //        type: "POST",
    //        url: "Ajax/Conexion_Ajax.aspx/actualizar_expediente_etapas",
    //        data: JSON.stringify(datos),
    //        contentType: "application/json; chartset=utf-8",
    //        processData: false,
    //        dataType: "json",
    //        success: function (respuesta) {

    //            if (respuesta["d"] != -1) {

    //                swal({
    //                    type: 'success',
    //                    title: 'Bien hecho!!',
    //                    text: 'Se ha confirmado la actualización del expediente!'
    //                }).then((result) => {
    //                    location.reload(true);
    //                });

    //            } else {

    //                swal({
    //                    type: 'warning',
    //                    title: 'Oops...',
    //                    text: 'No se logro almacenar la información!'
    //                });
    //            }

    //        }
    //    });


    //}
});//fin del metodo

function cerrar_caso(numero_expediente, numero_operacion) {

    $("#num_expedient").val(numero_expediente);

}

$("#btn_finalizar_caso").click(function (e) {
    e.preventDefault();

   // console.log($("#juztificacion").val());

    if ($("#juztificacion").val().trim().length > 0) {

        var datos = { numero_expediente: $("#num_expedient").val(), juztificacion: $("#juztificacion").val().trim() }

        $.ajax({
            type: "POST",
            url: "Ajax/Conexion_Ajax.aspx/cerrar_proceso",
            data: JSON.stringify(datos),
            contentType: "application/json; chartset=utf-8",
            processData: false,
            dataType: "json",
            success: function (respuesta) {

                if (respuesta["d"] != false) {

                    swal({
                        type: 'success',
                        title: 'Bien hecho!!',
                        text: 'Se ha finalizado el proceso para el expediente' + $("#num_expedient").val()
                    }).then((result) => {
                        location.reload(true);
                    });

                } else {

                    swal({
                        type: 'error',
                        title: 'Oops...',
                        text: 'No se pudo finalizar el caso!'
                    });
                }
            }

        });


    } else {

        swal({
            type: 'warning',
            title: 'Oops...',
            text: 'No has llenado la jusficación para finalizar este caso!'
        });
    }

});

/* Metoodos jquey para evitar ingresar letras en un campo de texto */
jQuery('#tractos').on('keypress', function (e) {

    if (e.keyCode == 101 || e.keyCode == 45 || e.keyCode == 46 || e.keyCode == 43 || e.keyCode == 44 || e.keyCode == 47) {
        return false;
    }
    soloNumeros(e.keyCode);
});

jQuery('#plazo').on('keypress', function (e) {
    if (e.keyCode == 101 || e.keyCode == 45 || e.keyCode == 46 || e.keyCode == 43 || e.keyCode == 44 || e.keyCode == 47) {
        return false;
    }
    soloNumeros(e.keyCode);
});

jQuery('#monto_mensual').on('keypress', function (e) {
    // -                          
    if (e.keyCode == 101 || e.keyCode == 45 || e.keyCode == 43 || e.keyCode == 44 || e.keyCode == 47) {
        return false;
    }
    soloNumeros(e.keyCode);
});

jQuery('#monto_recibido').on('keypress', function (e) {
    // -                          
    if (e.keyCode == 101 || e.keyCode == 45 || e.keyCode == 43 || e.keyCode == 44 || e.keyCode == 47) {
        return false;
    }
    soloNumeros(e.keyCode);
});

jQuery('#monto_cancelar').on('keypress', function (e) {
    if (e.keyCode == 101 || e.keyCode == 45 || e.keyCode == 43 || e.keyCode == 44 || e.keyCode == 47) {
        return false;
    }
    soloNumeros(e.keyCode);
});

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
}

$("#btn_busqueda_por_fechas").click(function (e) {
    e.preventDefault();

    $("#cargando").removeClass("hidden");

    var etapa = ($("#etapa_filtro").val() != "") ? $("#etapa_filtro").val() : 0;
    var subetapa = ($("#subetapa_filtro").val() != "") ? $("#subetapa_filtro").val() : 0;

    console.log({etapa,subetapa});

    cargar_informacion_filtro($("#nombre_deudor").val(), $("#codigo_proceso").val(), $("#numero_operacion").val(), etapa, subetapa, $("#estado").val());

})


function cargar_informacion_filtro(nombre, numero_proceso, numero_operacion, etapa, subetapa, estado) {
    debugger;
    $("#cargando").removeClass("hidden");
    var datos = { nombre: nombre, numero_proceso: numero_proceso, numero_operacion: numero_operacion, etapa: etapa, subetapa: subetapa, estado: estado };

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Ajax/Conexion_Ajax.aspx/mostrar_expedientes_legales_filtro",
        data: JSON.stringify(datos),
        contentType: "application/json; chartset=utf-8",
        processData: false,
        success: function (data) {

           // console.log(data["d"]);

            datatable = $('#lista_procesos').DataTable({
                destroy: true,
              //  dom: 'Bfrtip',
                //buttons: [
                //    'excel', 'pdf'
                //],
                data: data["d"],
                columns: [
                    { 'data': 'numero_expediente' },
                    { 'data': 'numero_operacion' },
                    { 'data': 'numero_proceso' },
                    { 'data': 'nombre_cliente' },
                    { 'data': 'etapa' },
                    { 'data': 'subetapa' },
                    { 'data': 'fecha_incluye' },
                    {
                        data: null, render: function (row) {

                            var opciones = '';

                            if (row.estado == 1) {

                                opciones += '<a id="btn_aplicar" class="btn btn-primary" onclick=tramitar("' + row.numero_expediente + '") data-toggle="modal" data-target="#tramitar_proceso"  ><i class="fa fa-sign-out"></i> Tramitar</a>\n';

                            }
                            debugger;
                            opciones += '<div class="btn-group">' +
                                '<button type = "button" class="btn btn-danger" > Opciones </button >' +
                                '<button type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-expanded="false">' +
                                '<span class="caret"></span>' +
                                '<span class="sr-only">Toggle Dropdown</span>' +
                                '</button>' +
                                '<ul class="dropdown-menu" role="menu">' +
                                '<li><a onclick=mostrar_historial("' + row.numero_expediente + '","' + row.numero_operacion + '") data-toggle="modal" data-target="#ver_historial">Historial</a></li>' +
                                '<li><a onclick=mostrar_informacion_demanda("' + row.numero_expediente + '","' + row.numero_operacion + '")  data-toggle="modal" data-target="#ver_detalles_demanda">Info. Demanda</a></li>';

                            if (row.estado == 1) {
                                opciones += '<li class="opciones-menu"><a onclick=anular_caso("' + row.numero_expediente + '","' + row.numero_operacion + '")  data-toggle="modal" data-target="#ventana_anular_caso">Rechazar Caso</a></li>' +
                                    '<li><a onclick=cerrar_caso("' + row.numero_expediente + '","' + row.numero_operacion + '")  data-toggle="modal" data-target="#ventana_cerrar_caso">Cerrar Caso</a></li>';
                            }


                            opciones += '</ul>' +
                                '</div >';


                            return opciones;

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

            $('label').addClass('form-inline');
            $('input[type="search"]').addClass('form-control redondeando  input-md');
            $('select').addClass('form-control input-md');

            $("#cargando").addClass("hidden");
        }
    });

}

function anular_caso(numero_expediente, numero_operacion) {

    $("#num_expedient2").val(numero_expediente);

    var datos = { numero_expediente: numero_expediente };

    //$.ajax({
    //    type: "POST",
    //    dataType: "json",
    //    url: "Ajax/Conexion_Ajax.aspx/obtener_nombre_demandado",
    //    data: JSON.stringify(datos),
    //    contentType: "application/json; chartset=utf-8",
    //    processData: false,
    //    success: function (data) {

    //        $("#titulo-anulacion").html("ANULACIÓN DEL CASO PERTENECIENTE A " + data["d"]);

    //    }
    //});
}

$("#btn_anular_caso").click(function (e) {
    e.preventDefault();

    //console.log($("#justificacion_anulacion").val());

    if ($("#justificacion_anulacion").val().trim().length > 0) {

        var datos = { numero_expediente: $("#num_expedient2").val(), justificacion_anulacion: $("#justificacion_anulacion").val().trim() }

        $.ajax({
            type: "POST",
            url: "Ajax/Conexion_Ajax.aspx/anular_proceso",
            data: JSON.stringify(datos),
            contentType: "application/json; chartset=utf-8",
            processData: false,
            dataType: "json",
            success: function (respuesta) {

                if (respuesta["d"] != false) {

                    swal({
                        type: 'success',
                        title: 'Bien hecho!!',
                        text: 'Se ha anulado el proceso para el expediente ' + $("#num_expedient2").val()
                    }).then((result) => {
                        location.reload(true);
                    });

                } else {

                    swal({
                        type: 'error',
                        title: 'Oops...',
                        text: 'No se pudo finalizar el caso!'
                    });
                }
            }

        });


    } else {

        swal({
            type: 'warning',
            title: 'Oops...',
            text: 'No has llenado la jusficación para rechazar este caso!'
        });
    }

});

function hoyFecha() {
    var hoy = new Date();
    var dd = hoy.getDate();
    var mm = hoy.getMonth() + 1;
    var yyyy = hoy.getFullYear();

    dd = addZero(dd);
    mm = addZero(mm);

    $("#fecha_inicial").val(yyyy + '-01-01');
    $("#fecha_final").val(yyyy + '-' + mm + '-' + dd);

}

function addZero(i) {
    if (i < 10) {
        i = '0' + i;
    }
    return i;
}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

$("input[data-type='currency']").on({
    keyup: function () {
        formatCurrency($(this));
    },
    blur: function () {
        formatCurrency($(this), "blur");
    }
});


function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
}


function formatCurrency(input, blur) {
    // appends $ to value, validates decimal side
    // and puts cursor back in right position.

    // get input value
    var input_val = input.val();

    // don't validate empty input
    if (input_val === "") { return; }

    // original length
    var original_len = input_val.length;

    // initial caret position 
    var caret_pos = input.prop("selectionStart");

    // check for decimal
    if (input_val.indexOf(".") >= 0) {

        // get position of first decimal
        // this prevents multiple decimals from
        // being entered
        var decimal_pos = input_val.indexOf(".");

        // split number by decimal point
        var left_side = input_val.substring(0, decimal_pos);
        var right_side = input_val.substring(decimal_pos);

        // add commas to left side of number
        left_side = formatNumber(left_side);

        // validate right side
        right_side = formatNumber(right_side);

        // On blur make sure 2 numbers after decimal
        if (blur === "blur") {
            right_side += "00";
        }

        // Limit decimal to only 2 digits
        right_side = right_side.substring(0, 2);

        // join number by .
        input_val = "Q" + left_side + "." + right_side;

    } else {
        // no decimal entered
        // add commas to number
        // remove all non-digits
        input_val = formatNumber(input_val);
        input_val = "Q" + input_val;

        // final formatting
        if (blur === "blur") {
            input_val += ".00";
        }
    }

    // send updated string to input
    input.val(input_val);

    // put caret back in the right position
    var updated_len = input_val.length;
    caret_pos = updated_len - original_len + caret_pos;
    input[0].setSelectionRange(caret_pos, caret_pos);
}

$("input[name='opcionVista']").click(function () {

    var opciones = new Array();
    var data = new FormData();
    var n = jQuery(".checkVista:checked").length;

    if (n > 0) {
        jQuery(".checkVista:checked").each(function () {

            opciones.push($(this).val());

            data.append("num_expediente", $("#num_expediente").val());
            data.append("visto", 1);


            $.ajax({
                type: "POST",
                url: "Ajax/Servicio_Conexion.asmx/caso_en_vista",
                data: data,
                contentType: false,
                processData: false,
                success: function (respuesta) {

                    if (respuesta["d"] != 1) {

                        swal({
                            position: 'top-end',
                            type: 'success',
                            title: 'Caso en vista',
                            showConfirmButton: false,
                            timer: 1500
                        })

                    } else {

                        swal({
                            position: 'top-end',
                            type: 'danger',
                            title: 'Error, no se agrego el caso en vista',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }



                }
            });

        });
    } else {

        data.append("num_expediente", $("#num_expediente").val());
        data.append("visto", 0);

        $.ajax({
            type: "POST",
            url: "Ajax/Servicio_Conexion.asmx/caso_en_vista",
            data: data,
            contentType: false,
            processData: false,
            success: function (respuesta) {

                if (respuesta["d"] != 1) {

                    swal({
                        position: 'top-end',
                        type: 'success',
                        title: 'Caso ya no esta en vista',
                        showConfirmButton: false,
                        timer: 1500
                    })

                } else {

                    swal({
                        position: 'top-end',
                        type: 'danger',
                        title: 'Error, no se desligo el caso en estado vista',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }

            }
        });

    }

    
});



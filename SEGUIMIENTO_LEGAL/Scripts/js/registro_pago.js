init();

function init() {

    $("#registro_pagos").addClass("active");
    $("#inicio").removeClass("active");

    cargar_fecha_hoy();

   // $("#informacion_pagos").hide();
}

$("#btn_buscar").click(function (e) {
    e.preventDefault();

    if ($("#num_ope").val().length > 0) {

        cargar_informacion();

        $("#informacion_pagos").show();

    } else {

        swal({
            type: 'warning',
            title: 'Atención!',
            text: 'Debes ingresar un número de operación para ejecutar la busqueda!'
        });

    }

});

function cargar_informacion() {

    var datos = { numero_operacion: $("#num_ope").val() };

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Ajax/Conexion_Ajax.aspx/mostrar_pagos",
        data: JSON.stringify(datos),
        contentType: "application/json; chartset=utf-8",
        processData: false,
        success: function (data) {

            //$("#numero_expediente").val(data["d"][""]);


            if (data["d"] != "") {


                datatable = $('#historial_pagos').DataTable({
                    destroy: true,
                    dom: 'Bfrtip',
                    buttons: [
                        'pdf'
                    ],
                    data: data["d"],
                    columns: [
                        //{ 'data': 'concepto_cuenta' },
                        { 'data': 'concepto_pago' },
                        { 'data': 'monto_inicial' },
                        { 'data': 'monto_cargos' },
                        { 'data': 'monto_recibido' },
                        { 'data': 'saldo' },
                        { 'data': 'fecha' }

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

            } else {
                swal({
                    position: 'top',
                    type: 'warning',
                    title: 'No existen registros de Pago',
                    showConfirmButton: false,
                    timer: 1500
                })

                //swal({
                //    type: 'warning',
                //    title: 'Atención!',
                //    text: 'Debes ingresar un número de operación para ejecutar la busqueda!'
                //});
            }
          
        }
    });

}

function abrir_modal() {

    $("#numero_operacion").val($("#num_ope").val());

    var datos = { numero_operacion: $("#numero_operacion").val()}

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Ajax/Conexion_Ajax.aspx/mostrar_numero_expediente",
        data: JSON.stringify(datos),
        contentType: "application/json; chartset=utf-8",
        processData: false,
        success: function (data) {

            $("#numero_expediente").val(data["d"]);
        }
    });



}

$("#registrar_movimiento_pago").click(function (e) {

    e.preventDefault();

    var datos = { numero_expediente: $("#numero_expediente").val(), numero_operacion: $("#numero_operacion").val(), concepto_pago: $("#tipo_pago").val() , monto: $("#monto_recibido").val(), fecha_pago: $("#fecha_pago").val(), observaciones: $("#observaciones").val() }

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Ajax/Conexion_Ajax.aspx/realizar_pago",
        data: JSON.stringify(datos),
        contentType: "application/json; chartset=utf-8",
        processData: false,
        success: function (data) {

            if (data["d"] == true) {

                swal({
                    position: 'top',
                    type: 'success',
                    title: 'Pago Registrado',
                    showConfirmButton: false,
                    timer: 1500
                })


            } else {

                swal({
                    position: 'top',
                    type: 'error',
                    title: 'No se registro el pago',
                    showConfirmButton: false,
                    timer: 1500
                })

                

            }

        }
    });

})

function cargar_fecha_hoy() {
    var fecha = new Date(); //Fecha actual
    var mes = fecha.getMonth() + 1; //obteniendo mes
    var dia = fecha.getDate(); //obteniendo dia
    var ano = fecha.getFullYear(); //obteniendo año
    if (dia < 10)
        dia = '0' + dia; //agrega cero si el menor de 10
    if (mes < 10)
        mes = '0' + mes //agrega cero si el menor de 10
    document.getElementById('fecha_pago').value = ano + "-" + mes + "-" + dia;
}

jQuery('#monto_recibido').on('keypress', function (e) {
    if (e.keyCode == 101 || e.keyCode == 45 || e.keyCode == 43 || e.keyCode == 44 || e.keyCode == 47) {
        return false;
    }
    soloNumeros(e.keyCode);
});

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
}

$("#tipo_pago").change(function () {

    var datos = {numero_operacion: $("#numero_operacion").val()};

    if ($("#tipo_pago").val() == "ABONO") {

        $.ajax({
            type: "POST",
            dataType: "json",
            url: "Ajax/Conexion_Ajax.aspx/mostrar_monto_mensual",
            data: JSON.stringify(datos),
            contentType: "application/json; chartset=utf-8",
            processData: false,
            success: function (data) {
                $("#monto_recibido").val(data["d"]);
            }
        });

        //monto_recibido

    } else if ($("#tipo_pago").val() == "ABONO EXTRAORDINARIO") {

        $("#monto_recibido").val(0);

    } else if ($("#tipo_pago").val() == "INTERESES") {

        $.ajax({
            type: "POST",
            dataType: "json",
            url: "Ajax/Conexion_Ajax.aspx/mostrar_monto_intereses",
            data: JSON.stringify(datos),
            contentType: "application/json; chartset=utf-8",
            processData: false,
            success: function (data) {
                $("#monto_recibido").val(data["d"]);
            }
        });

    } else if ($("#tipo_pago").val() == "CANCELACIÓN") {

        $.ajax({
            type: "POST",
            dataType: "json",
            url: "Ajax/Conexion_Ajax.aspx/mostrar_monto_total",
            data: JSON.stringify(datos),
            contentType: "application/json; chartset=utf-8",
            processData: false,
            success: function (data) {
                $("#monto_recibido").val(data["d"]);
            }
        });

    }

});
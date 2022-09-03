init();

function init() {
    $("#registros_cancelados").addClass("active");
    $("#inicio").removeClass("active");

    cargar_informacion();
}

function cargar_informacion() {

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Ajax/Conexion_Ajax.aspx/mostrar_expedientes_finalizados",
        contentType: "application/json; chartset=utf-8",
        processData: false,
        success: function (data) {

            datatable = $('#lista_finalizadas').DataTable({
                destroy: true,
                dom: 'Bfrtip',
                buttons: [
                    'pdf'
                ],
                data: data["d"],
                columns: [
                    { 'data': 'numero_expediente' },
                    { 'data': 'numero_operacion' },
                    { 'data': 'nombre_cliente' },
                    { 'data': 'identificacion' },
                    { 'data': 'detalles' },
                    { 'data': 'estado' }

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
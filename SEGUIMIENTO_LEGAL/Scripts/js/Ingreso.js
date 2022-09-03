init();

function init() {
    $("#abrir_expediente").addClass("active");
    $("#inicio").removeClass("active");

    cargar_subetapas_iniciales();

}

function cargar_subetapas_iniciales() {

    var etapa = 1;
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

$('input[id=num_ope]').on('keydown', function (e) {
    if (e.which == 13) {
        e.preventDefault();

        var datos = { numero_operacion: $("#num_ope").val() }

        $.ajax({
        type: "POST",
        url: "Ajax/Conexion_Ajax.aspx/verificar_duplicacion",
        data: JSON.stringify(datos),
        contentType: "application/json; chartset=utf-8",
        processData: false,
        dataType: "json",
        success: function (respuesta) {

            //console.log(respuesta["d"]);

                if (respuesta["d"] != true) {

                    $.ajax({
                    type: "POST",
                    url: "Ajax/Conexion_Ajax.aspx/obtener_informacion_cliente",
                    data: JSON.stringify(datos),
                    contentType: "application/json; chartset=utf-8",
                    processData: false,
                    dataType: "json",
                        success: function (respuesta2) {

                            console.log(respuesta2["d"][0].length)
                            if (respuesta2["d"].length > 0) {

                                if (respuesta2["d"][0].length <= 9) {

                                    $("#nit").val(respuesta2["d"][0]);
                                    $("#nom_cli").val(respuesta2["d"][1]);

                                } else {
                                    $("#dpi").val(respuesta2["d"][0]);
                                    $("#nom_cli").val(respuesta2["d"][1]);
                                }

                            } else {

                                swal({
                                    type: 'warning',
                                    title: 'Atención!',
                                    text: 'No se encuentran registros!'
                                });

                            }

                        }

                    })

                } else {

                    swal({
                        type: 'warning',
                        title: 'Cuidado!',
                        text: 'El proceso ha esta cuenta ya ha sido iniciado!'
                    });
                }
            }
        })

    }
});

function ingresar() {
    e.preventDefault();
    var datos = { numero_expediente: $("#correlativo").val()}

    $.ajax({
        type: "POST",
        url: "Ajax/Conexion_Ajax.aspx/ingresar_nuevo_caso",
        data: JSON.stringify(datos),
        contentType: "application/json; chartset=utf-8",
        processData: false,
        dataType: "json",
        success: function (respuesta) {

            if (respuesta["d"] == true) {


                $.ajax({
                    type: "POST",
                    url: "Ajax/Conexion_Ajax.aspx/verificar_duplicacion",
                    data: JSON.stringify(datos),
                    contentType: "application/json; chartset=utf-8",
                    processData: false,
                    dataType: "json",
                    success: function (respuesta2) {

                        if (respuesta2["d"] != true) {


                        } else {

                            swal({
                                type: 'warning',
                                title: 'Cuidado!',
                                text: 'El proceso ha esta cuenta ya ha sido iniciado!'
                            });

                        }




                    }



                });

                //var datos = { numero_operacion: $("#num_ope").val(), nombre_cliente: $("#nom_cli").val(), nit: $("#nit").val(), dpi: $("#dpi").val(), correlativo: $("#correlativo").val(), etapa: 1, subetapa: $("#subetapa").val(), ruta_archivos: $("#archivos").val() };

                //$.ajax({
                //    type: "POST",
                //    url: "Ajax/Conexion_Ajax.aspx/ingresar_nuevo_caso",
                //    data: JSON.stringify(datos),
                //    contentType: "application/json; chartset=utf-8",
                //    processData: false,
                //    dataType: "json",
                //    success: function (respuesta2) {
                //        if (respuesta2["d"] > 0) {

                //            swal({
                //                title: 'Buen Trabajo!',
                //                text: 'Se ha abierto un nuevo expediente!',
                //                type: 'success'
                //            }).then((result) => {
                //                location.reload(true);
                //            });

                //        } else {
                //            swal({
                //                title: 'Disculpas!',
                //                text: 'No se ha creó un nuevo expediente!',
                //                type: 'error'
                //            }).then((result) => {
                                
                                
                //                location.reload(true);
                //            });
                //        }
                //    }
                //});

            } else {
                swal({
                    title: 'Atención!',
                    text: 'Ya exite un número de expediente abierto con el correlativo ingresado!',
                    type: 'warning'
                }).then((result) => {
                    e.preventDefault();
                    location.reload(true);
                });
            }


        }

    });

}

$("#btn_ingresar").click(function (e) {

    e.preventDefault();

    console.log("hola");

    var datos = { numero_expediente: $("#correlativo").val() }

    $.ajax({
        type: "POST",
        url: "Ajax/Conexion_Ajax.aspx/consultar_duplicion_expediente",
        data: JSON.stringify(datos),
        contentType: "application/json; chartset=utf-8",
        processData: false,
        dataType: "json",
        success: function (respuesta) {


            if (respuesta["d"] == true) {


                swal({
                    title: 'Atención!',
                    text: 'El correlativo ingresado ya tiene proceso!',
                    type: 'warning'
                });
               


            } else {

                datos = { numero_operacion: $("#num_ope").val() };

                $.ajax({
                    type: "POST",
                    url: "Ajax/Conexion_Ajax.aspx/verificar_duplicacion",
                    data: JSON.stringify(datos),
                    contentType: "application/json; chartset=utf-8",
                    processData: false,
                    dataType: "json",
                    success: function (respuesta2) {

                        console.log("operacion",respuesta2["d"]);

                        if (respuesta2["d"] == true) {

                            swal({
                                type: 'warning',
                                title: 'Cuidado!',
                                text: 'La operación ingresada ya tiene proceso!'
                            });


                        } else {                        

                            var data = new FormData();
                            data.append("numero_operacion", $("#num_ope").val());
                            data.append("num_expediente", $("#correlativo").val());
                            data.append("nit", $("#nit").val());
                            data.append("dpi", $("#dpi").val());
                            data.append("nombre_deudor", $("#nom_cli").val());
                            data.append("subetapa", $("#subetapa").val());
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

                            // Crear el ajax con ---> contentType = false, and procesDate = false
                            $.ajax({
                                type: "POST",
                                url: "Ajax/Servicio_Conexion.asmx/registrar_nuevo_caso",
                                contentType: false,
                                processData: false,
                                data: data,
                                success: function (respuesta) {


                                    if (respuesta != "") {

                                        swal({
                                            type: 'success',
                                            title: 'Bien hecho!!',
                                            text: 'Se ha registrado el nuevo expediente '
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
                            


                            //var datos = { numero_operacion: $("#num_ope").val(), nombre_cliente: $("#nom_cli").val(), nit: $("#nit").val(), dpi: $("#dpi").val(), correlativo: $("#correlativo").val(), etapa: 1, subetapa: $("#subetapa").val(), ruta_archivos: $("#archivos").val()};

                            //$.ajax({
                            //    type: "POST",
                            //    url: "Ajax/Conexion_Ajax.aspx/ingresar_nuevo_caso",
                            //    data: JSON.stringify(datos),
                            //    contentType: "application/json; chartset=utf-8",
                            //    processData: false,
                            //    dataType: "json",
                            //    success: function (respuesta3) {
                            //        if (respuesta3["d"] > 0) {

                            //            swal({
                            //                title:'Buen Trabajo!',
                            //                text:'Se ha abierto un nuevo expediente!',
                            //                type:'success'
                            //            }).then((result) => {

                            //                location.reload(true);
                            //            });

                            //        } else {
                            //            swal({
                            //                title:'Disculpas!',
                            //                text:'No se ha creó un nuevo expediente!',
                            //                type:'danger'
                            //            }).then((result) => {

                            //                location.reload(true);
                            //            });
                            //        }
                            //    }
                            //})


                           

                        }




                    }

                });
         
             
            }


        }

    });

    


});

$("#archivos").change(function () {
    var input = document.getElementById('archivos');
    var path = $("#archivos").val();
    //path = path.replace('\\','/');
    //path = path.substr(0, path.lastIndexOf("/"));

    for (var x = 0; x < input.files.length; x++) {

        console.log(input.files[x].name);
        console.log(path);
    }

})




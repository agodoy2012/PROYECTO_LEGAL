<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CambiaEstadoLegal.aspx.cs" Inherits="SEGUIMIENTO_LEGAL.CambiaEstadoLegal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <h1>Actualizar Estado de Casos Legal
        </h1>
        <ol class="breadcrumb">
            <li><a href="Inicio"><i class="fa fa-dashboard"></i>Inicio</a></li>
            <li>Abrir Expediente Legal</li>
            <li class="active">Ingreso</li>
        </ol>
    </section>

    <section class="content">

        <div class="box">

            <div class="box-header with-border">
                <i class="fa fa-folder-open"></i>
                <h3 class="box-title">Actualizar Estado Caso</h3>
            </div>

            <div class="box-body">
                <div class="row">

                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Número de Operación:</label>
                            <asp:TextBox ID="num_ope" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label>ID Expediente Legal:</label>
                            <asp:TextBox ID="id_exp_L" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Número Proceso:</label>
                             <asp:TextBox ID="nProceso" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Estado:</label>
                             <asp:DropDownList ID="ddlEstado" Width="100%" CssClass="form-control select2" runat="server" ClientIDMode="Static">
                                    <asp:ListItem Value="1" Text="ABIERTO"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="CERRADO"></asp:ListItem>
                                </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-4">
                          <input class="btn btn-sm btn-warning" id="btnActualiza" type="button" value="Actualizar" />
                    </div>
                </div>
            </div>
        </div>
    </section>
    <script src="Scripts/sweetalert2/sweetalert2.all.js"></script>

    <script>
         // actulizar datos de la empresa
        $('#btnActualiza').click(function () {
            debugger;
            var estado = $('#ddlEstado').val();
            var operacion = $('#num_ope').val();
            var id_exp = $('#id_exp_L').val();
            var numProcess = $('#nProceso').val();

            if (operacion != "" && id_exp != "" && numProcess != "") {
                var variables = { p_estado: estado, p_operacion: operacion, p_id_exp: id_exp, p_num_proces: numProcess };
                $.ajax({
                type: "POST",
                dataType: "json",
                url: "<%= ResolveUrl("CambiaEstadoLegal.aspx/actualizarEstadoCaso") %>",
                data: JSON.stringify(variables),
                contentType: "application/json; charset=utf-8",
                processData: false,
                dataType: "json",
                success: function (respuesta) {

                    var result = respuesta["d"];
                    if (result) {
                        swal({
                            type: 'success',
                            title: '',
                            text: '¡Se actualizó correctamente!'
                        });
                    }
                },
                error: function (data, success, error) {
                    console.log("Error: " + Error);
                }
            });
            }
            else {
                swal({
                    type: 'error',
                    title: '',
                    text: '¡Debe completar todos los campos!'
                });
            }
            

        });
    </script>
</asp:Content>

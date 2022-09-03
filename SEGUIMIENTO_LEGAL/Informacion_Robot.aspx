<%@ Page Title="Informacion_Robot" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Informacion_Robot.aspx.cs" Inherits="SEGUIMIENTO_LEGAL.Informacion_Robot" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
      <h1>
        Información Extraída del Robot
      </h1>
      <ol class="breadcrumb">
        <li ><a href="Inicio"><i class="fa fa-dashboard"></i> Inicio</a></li>
        <li>Lista de Clientes</li>
        <li class="active">Información Robot</li>
      </ol>
    </section>

    <section class="content">

        <div class="box">

            <div class="box-header with-border">

                    <i class="fa fa-binoculars"></i><h3 class="box-title">Información Robot</h3>
             </div>

            <div class="box-body">

                <table id="lista_robot" class="table table-striped table-hover clientes" style='width:100%' ">
                <thead>
                    <tr>
                        <th>Número de Operacion</th>
                        <th>Cliente</th>
                        <th>Identificación</th>
                        <th>Lugar Trabajo</th>
                        <th>Mes</th>
                        <th>Año</th>
                        <th>Opciones</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
                <tfoot>
                    <tr>
                        <th>Número de Operacion</th>
                        <th>Cliente</th>
                        <th>Identificación</th>
                        <th>Lugar Trabajo</th>
                        <th>Mes</th>
                        <th>Año</th>
                        <th>Opciones</th>
                    </tr>
                </tfoot>
            </table>

            </div>

            <div class="box-footer">
              Área Legal Guatemala
            </div>

        </div>

    </section>

    <div id="tramitar_proceso" class="modal fade" role="dialog">
  
    <div class="modal-dialog">

        <div class="modal-content" style="width:600px !important">

        <!--=====================================
        CABEZA DEL MODAL
        ======================================-->

            <div class="modal-header" style="background:#27333a; color:white">

            <button type="button" class="close" data-dismiss="modal">&times;</button>

            <h4 class="modal-title">Gestionar Expediente Legal
            </h4>

            </div>

        <!--=====================================
        CUERPO DEL MODAL
        ======================================-->

            <div class="modal-body">

                <div class="box-body">

                    <div class="col-md-6 col-xs-12">
                        <div class="form-group">
                          <label>Seleccione Etapa:</label>
                            <input type="hidden" name="num_expediente" id="num_expediente"/>
                            <input type="hidden" name="documentos" id="documentos"/>
                          <select class="form-control" name="etapa" id="etapa" required="required"></select>
                        </div>
                    </div> 

                   <div class="col-md-6 col-xs-12">
                        <div class="form-group">
                          <label>Seleccione Subetapa:</label>
                          <select class="form-control" name="subetapa" id="subetapa" required="required"></select>
                        </div>
                    </div> 

                    <div class="col-xs-12">
                        <div class="form-group">
                          <label>Seleccione Documentos Relacionados con el proceso:</label>

                            <input type="file" name="archivos" id="archivos" multiple="multiple"/>
                        </div>
                    </div> 

                    <div id="datos_arreglo_extra_judicial">
                    <!-- Campos para el arreglo extra judicial -->
                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Tipo de pago:</label>
                          <select class="form-control" name="tipo_pago" id="tipo_pago"></select>
                        </div>
                    </div> 

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Vía de pago:</label>
                          <select class="form-control" name="via_pago" id="via_pago">
                              <option value="Arreglo Extra Judicial">Arreglo Extra Judicial</option>
                              <option value="Arreglo Judicial">Arreglo Judicial</option>
                          </select>
                        </div>
                    </div> 

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Monto a Cancelar:</label>
                          <input type="number" step="any" class="form-control" name="monto_cancelar" id="monto_cancelar">
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Tractos:</label>
                          <input type="number" step="any" class="form-control" name="tractos" id="tractos">
                        </div>
                    </div>  

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Plazo:</label>
                          <input type="number" step="any" class="form-control" name="plazo" id="plazo">
                        </div>
                    </div> 

                   <%--<div class="col-md-4">
                        <div class="form-group">
                          <label>Día de pago:</label>
                          <input type="hidden"  class="form-control" name="dia_pago" id="dia_pago" value="0">
                        </div>
                    </div> --%>

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Fecha Fin de Pago:</label>
                          <input type="date" step="any" class="form-control" name="fecha_fin_pago" id="fecha_fin_pago">
                        </div>
                    </div> 

                 </div>
                    <!-- Fin campos para el arreglo extra judicial -->

                    <!-- Campos para el presentación de la demanda -->

                    <div id="datos_presentacion_demanda" >

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Proceso Judicial:</label>
                          <select class="form-control" name="proceso_judicial" id="proceso_judicial"></select>
                        </div>
                    </div> 

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Juzgado:</label>
                          <select class="form-control" name="juzgado" id="juzgado"></select>
                        </div>
                    </div> 


                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Numero de Proceso:</label>
                             <input type="text" class="form-control" name="numero_proceso" id="numero_proceso" data-inputmask="&quot;mask&quot;: &quot;(999) 999-9999&quot;" data-mask="">
                            <%--<input type="text"  class="form-control" name="numero_proceso" id="numero_proceso">--%>
                        </div>
                    </div> 

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Oficial a cargo:</label>
                            <select class="form-control" name="oficial" id="oficial"></select>
                        </div>
                    </div> 

                  </div>
                    <!-- Fin campos para el presentación de la demanda -->

                    <div class="col-md-12">
                        <div class="form-group">
                          <label>Detalles:</label>
                            <textarea class="form-control" name="detalles" id="detalles"></textarea>
                           <%-- <input type="text" class="form-control" name="detalles" id="detalles">--%>
                        </div>
                    </div> 

                </div>

            </div>

        <!--=====================================
        PIE DEL MODAL
        ======================================-->

        <div class="modal-footer">

        <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Salir</button>

        <asp:Button class="btn btn-danger" Text="Registar" ID="btn_registrar" ClientIDMode="Static" runat="server"/>

        </div>

        </div>

    </div>

</div>

   
    <script src="Scripts/js/Lista_Robot.js"></script>
    <script src="Scripts/sweetalert2/sweetalert2.all.js"></script>
</asp:Content>
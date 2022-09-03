<%@ Page Title="Registro_Pagos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro_Pagos.aspx.cs" Inherits="SEGUIMIENTO_LEGAL.Registro_Pagos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
      <h1>
        Registrar Pago
      </h1>
      <ol class="breadcrumb">
        <li ><a href="Inicio"><i class="fa fa-dashboard"></i> Inicio</a></li>
        <li>Registro de Pagos</li>
        <li class="active">Registrar Pago</li>
      </ol>
    </section>

    <section class="content">

        <div class="box">

            <div class="box-header with-border">

                    <i class="fa fa-money"></i><h3 class="box-title">Registrar Pago</h3>
             </div>

            <div class="box-body">

                <div class="row">

                    <div class="col-md-6 col-xs-12">
                        <div class="form-group">
                          <label>Número de Operación:</label>
                          <input type="text" class="form-control" name="num_ope" id="num_ope" placeholder="" required="required">
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-group">
                            <button class="btn btn-danger" id="btn_buscar">Buscar</button>
                        </div>
                    </div>

                    <div id="informacion_pagos">

                        <div class="col-md-12">

                            <button type="button" class="btn btn-danger pull-right" onclick=abrir_modal() data-toggle="modal" data-target="#registro_pago">Registrar Pago</button>
                            

                        </div>

                        <div class="col-md-12">
                            <br>

                             <table id="historial_pagos" class="table table-striped table-hover" style='width:100%' ">
                        <thead>
                            <tr>
                                <th>Concepto Pago</th>
                                <th>Monto Inicial</th>
                                <th>Intereses</th>
                                <th>Monto Recibido</th>
                                <th>Saldo</th>
                                <th>Fecha Recibido</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                        <tfoot>
                            <tr>
                                <th>Concepto Pago</th>
                                <th>Monto Inicial</th>
                                <th>Intereses</th>
                                <th>Monto Recibido</th>
                                <th>Saldo</th>
                                <th>Fecha Recibido</th>
                            </tr>
                        </tfoot>
                    </table>

                            
                        </div>
                        
 
                      

                        </div>

                    
                </div>

            </div>

            <div class="box-footer">
             Gestionadora de Créditos
            </div>


            </div>
       
        </section>

    <div id="registro_pago" class="modal fade" role="dialog">
  
    <div class="modal-dialog">

        <div class="modal-content" style="width:600px !important">

        <!--=====================================
        CABEZA DEL MODAL
        ======================================-->

            <div class="modal-header" style="background:#27333a; color:white">

            <button type="button" class="close" data-dismiss="modal">&times;</button>

            <h4 class="modal-title">Registro de pago
            </h4>

            </div>

        <!--=====================================
        CUERPO DEL MODAL
        ======================================-->

            <div class="modal-body">

                <div class="box-body">

                    <div class="col-md-8 col-xs-12">
                        <div class="form-group">                          
                          <label>Tipo Pago:</label>
                            <select class="form-control" id="tipo_pago" required="required">
                                <option value="">Seleccione...</option>
                                <option value="ABONO">Abono Mensual</option>
                                <option value="ABONO EXTRAORDINARIO">Abono Extraordinario</option>
                                <option value="INTERESES">Pago de Intereses</option>
                                <option value="CANCELACIÓN">Cancelación de la Cuenta</option>
                            </select>
                        </div>
                    </div> 

                    <div class="col-md-8 col-xs-12">
                        <div class="form-group">                          
                          <label>Monto Recibido:</label>
                            <input type="hidden" name="numero_operacion" id="numero_operacion"/>
                            <input type="hidden" name="numero_expediente" id="numero_expediente"/>
                            <input type="number"  class="form-control" step="any" name="monto_recibido"  id="monto_recibido" min="0" placeholder="0.00" />
                        </div>
                    </div> 
                    <div class="col-md-8 col-xs-12">
                        <div class="form-group">                          
                          <label>Fecha de Pago:</label>
                            <input type="date"  class="form-control" step="any" name="fecha_pago"  id="fecha_pago"/>
                        </div>
                    </div> 

                    <div class="col-md-12">
                        <div class="form-group">
                          <label>Observaciones del Pago:</label>
                            <textarea class="form-control" id="observaciones"></textarea>
                        </div>
                    </div> 


        <!--=====================================
        PIE DEL MODAL
        ======================================-->       

            </div>

        </div>

            <div class="modal-footer">

                <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Salir</button>

                  <asp:Button class="btn btn-danger" Text="Registar" ID="registrar_movimiento_pago" ClientIDMode="Static" runat="server"/>
                
         </div>

    </div>

  </div>

</div>
   
    <script src="Scripts/js/registro_pago.js"></script>
    <script src="Scripts/sweetalert2/sweetalert2.all.js"></script>

</asp:Content>

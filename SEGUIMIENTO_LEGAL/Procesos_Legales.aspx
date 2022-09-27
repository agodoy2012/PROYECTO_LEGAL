<%@ Page Title="Procesos_Legales" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Procesos_Legales.aspx.cs" Inherits="SEGUIMIENTO_LEGAL.Procesos_Legales" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <%-- xslx export to excel --%>

<%--    <link href="Content/DataTables/css/buttons.dataTables.css" rel="stylesheet" />
    <link href="Content/DataTables/css/buttons.bootstrap.min.css" rel="stylesheet" />--%>

    <script src="Scripts/DataTables/dataTables.buttons.js"></script>
    <script src="Scripts/DataTables/buttons.flash.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="Scripts/pdfmake.js"></script>
    <script src="Scripts/vfs_fonts.js"></script>
   <%-- <script src="Scripts/DataTables/buttons.html5.js"></script>
    <script src="Scripts/DataTables/buttons.print.js"></script>--%>
    <script src="Scripts/moment.min.js"></script>
    
    <script src="Scripts/bootstrap-table.min.js"></script>


  <div class="lds-css ng-scope loading hidden" id="cargando">
  <div style="width:100%;height:100%" class="lds-double-ring">
  <div>
  </div>
  <div>
  </div>
  </div>  
  <style type="text/css">

        .redondeando {
            border-radius: 15px;
        }

      @keyframes lds-double-ring {
          0% {
              -webkit-transform: rotate(0);
              transform: rotate(0);
          }

          100% {
              -webkit-transform: rotate(360deg);
              transform: rotate(360deg);
          }
      }

      @-webkit-keyframes lds-double-ring {
          0% {
              -webkit-transform: rotate(0);
              transform: rotate(0);
          }

          100% {
              -webkit-transform: rotate(360deg);
              transform: rotate(360deg);
          }
      }

      @keyframes lds-double-ring_reverse {
          0% {
              -webkit-transform: rotate(0);
              transform: rotate(0);
          }

          100% {
              -webkit-transform: rotate(-360deg);
              transform: rotate(-360deg);
          }
      }

      @-webkit-keyframes lds-double-ring_reverse {
          0% {
              -webkit-transform: rotate(0);
              transform: rotate(0);
          }

          100% {
              -webkit-transform: rotate(-360deg);
              transform: rotate(-360deg);
          }
      }

      .lds-double-ring {
          position: relative;
      }

          .lds-double-ring div {
              position: absolute;
              width: 160px;
              height: 160px;
              top: 20px;
              left: 20px;
              border-radius: 50%;
              border: 8px solid #000;
              border-color: #912623 transparent #912623 transparent;
              -webkit-animation: lds-double-ring 2.7s linear infinite;
              animation: lds-double-ring 2.7s linear infinite;
          }

              .lds-double-ring div:nth-child(2) {
                  width: 140px;
                  height: 140px;
                  top: 30px;
                  left: 30px;
                  border-color: transparent #959797 transparent #959797;
                  -webkit-animation: lds-double-ring_reverse 2.7s linear infinite;
                  animation: lds-double-ring_reverse 2.7s linear infinite;
              }

      .lds-double-ring {
          width: 200px !important;
          height: 200px !important;
          -webkit-transform: translate(-100px, -100px) scale(1) translate(100px, 100px);
          transform: translate(-100px, -100px) scale(1) translate(100px, 100px);
      }
  </style></div>

    <section class="content-header">
      <h1>
        Casos en Proceso Legal
      </h1>
      <ol class="breadcrumb">
        <li ><a href="Inicio"><i class="fa fa-dashboard"></i> Inicio</a></li>
        <li>Lista de Casos</li>
        <li class="active">Casos en Proceso Legal</li>
      </ol>
    </section>

    <section class="content">

        <div class="box">
             <div class="box-header with-border">

                 <i class="fa fa-gavel"></i><h3 class="box-title">Casos en Proceso Legal</h3>

                 <asp:Button ID="Button1" runat="server" Text="PROCESO LEGAL" OnClick="bExel_Click" CssClass="btn btn-success pull-right" />

                 <%--<button class="btn btn-primary pull-right" id="btn_Reporte_Excel" style="margin-top: 25px;">Búscar</button>--%> 
                 <%--<dividir los dos botones>--%>
             </div>

            <div class="box-header with-border">

                 <i class="fa fa-gavel"></i><h3 class="box-title">Casos sin Movimiento Legal</h3>

                 <asp:Button ID="bExel" runat="server" Text="SIN MOVIMIENTO" OnClick="bExel_Click_sin_act" CssClass="btn btn-success pull-right" BackColor="Red" />

                 <%--<button class="btn btn-primary pull-right" id="btn_Reporte_Excel" style="margin-top: 25px;">Búscar</button>--%> 
             </div>

            <div class="box-body">

                <div class="form-group col-md-2">
                    <label>Nombre:</label>
                    <input type="text" class="form-control" name="nombre_deudor" id="nombre_deudor" runat="server" clientIdmode="static"/>
                </div>

                <div class="form-group col-md-2">
                    <label>Número de proceso:</label>
                    <input type="text" class="form-control" name="codigo_proceso" id="codigo_proceso" runat="server" clientIdmode="static"/>
                </div>

                <div class="form-group col-md-2">
                    <label>Número de Operación:</label>
                    <input type="text" class="form-control" name="numero_operacion" ID="numero_operacion" runat="server" clientIdmode="static"/>
                </div>

               <%-- <div class="form-group col-md-2">
                    <label>Fecha Inicial:</label>
                    <input type="date" class="form-control" name="fecha_inicial" id="fecha_inicial"  />
                </div>

                <div class="form-group col-md-2">
                    <label>Fecha Final:</label>
                    <input type="date" class="form-control" name="fecha_final" id="fecha_final"  />
                </div>--%>

                <div class="col-md-2">
                        <div class="form-group">
                          <label>Etapa:</label>
                          <select class="form-control" name="etapa_filtro" id="etapa_filtro"  runat="server" clientIdmode="static"></select>
                        </div>
                    </div> 

                   <div class="col-md-2">
                        <div class="form-group">
                          <label>Subetapa:</label>
                          <select class="form-control pull-right" name="subetapa_filtro" id="subetapa_filtro" runat="server" clientIdmode="static" ></select>
                        </div>
                    </div> 

                <div class="col-md-2">
                        <div class="form-group">
                          <label>Estado:</label>
                          <select class="form-control" name="estado" id="estado" required="required" runat="server" clientIdmode="static">
                              <option value="1">ABIERTOS</option>    
                              <option value="2">RECHAZADOS</option>   
                              <option value="0">CERRADOS</option>   
                          </select>
                        </div>
                    </div> 

                <div class="form-group col-md-12">
                    
                    <button class="btn btn-primary pull-right" id="btn_busqueda_por_fechas" style="margin-top: 25px;">Buscar</button>
                </div>

                <table id="lista_procesos" class="table table-striped table-hover" style='width:100%' ">
                <thead>
                    <tr>
                        <th>No. Expediente</th>
                        <th>No. de Operacion</th>
                        <th style="width: 85px;">No. de Proceso</th>
                        <th>Cliente</th>
                        <th>Etapa</th>
                        <th>Subetapa</th>
                        <th>Fecha Ingreso</th>
                        <th>Opciones</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
                <tfoot>
                    <tr>
                        <th>No. Expediente</th>
                        <th>No. de Operacion</th>
                        <th>No. de Proceso</th>
                        <th>Cliente</th>
                        <th>Etapa</th>
                        <th>Subetapa</th>
                        <th>Fecha Ingreso</th>
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

        <div class="modal-content" style="width:800px !important">

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

                    <div class="col-xs-12" style="display: none">
                        <div class="form-group">
                          <label>Seleccione Documentos Relacionados con el proceso:</label>

                            <input type="file" name="archivos" id="archivos" ClientIDMode="Static" runat="server" multiple="multiple"/>
                        </div>
                    </div> 
                    <!-- INICIO campos para PAGOS-->
                    <div id="pagos">
                    <!-- Campos para PAGOS -->
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
                              <option value="ARREGLO EXTRA JUDICIA">ARREGLO EXTRA JUDICIAL</option>
                              <option value="ARREGLO JUDICIAL">ARREGLO JUDICIAL</option>
                          </select>
                        </div>
                    </div> 

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Monto Mensual:</label>
                          <!--<input type="number" step="any" class="form-control" name="monto_mensual" min="0" id="monto_mensual" pattern="^[0-9]+" onpaste="return false;" onDrop="return false;" autocomplete=off>-->
                            <input type="text" class="form-control" name="monto_mensual" min="0" id="monto_mensual" pattern="^\$\d{1,3}(,\d{3})*(\.\d+)?$" data-type="currency" placeholder="Q1,000.00" onpaste="return false;" onDrop="return false;" autocomplete=off>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Monto a Cancelar:</label>
                          <input type="text" class="form-control" name="monto_cancelar" min="0" id="monto_cancelar" pattern="^\$\d{1,3}(,\d{3})*(\.\d+)?$" data-type="currency" placeholder="Q1,000.00" onpaste="return false;" onDrop="return false;" autocomplete=off>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Tractos:</label>
                          <input type="number" class="form-control" min="0" name="tractos" id="tractos" pattern="^\$\d{1,3}(,\d{3})*(\.\d+)?$" onpaste="return false;" onDrop="return false;" autocomplete=off>
                        </div>
                    </div>  
                        
                        

                   <div class="col-md-4">
                        <div class="form-group">
                          <label>Fecha Inicio de Pago:</label>
                          <input type="date" step="any" class="form-control" name="fecha_inicio_pago" id="fecha_inicio_pago">
                        </div>
                    </div> 

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Fecha Fin de Pago:</label>
                          <input type="date" step="any" class="form-control" name="fecha_fin_pago" id="fecha_fin_pago">
                        </div>
                    </div> 

                 </div>
                    <!-- Fin campos para PAGOS-->

                    <div id="mont_recib">

                              <div class="col-md-4" >
                                <div class="form-group">
                                  <label>Monto Recibido:</label>
                                  <input type="text" class="form-control" min="0"  name="monto_recibido" id="monto_recibido" pattern="^\$\d{1,3}(,\d{3})*(\.\d+)?$" data-type="currency" placeholder="Q1,000.00" pattern="^[0-9]+" onpaste="return false;" onDrop="return false;" autocomplete=off>
                                </div>
                            </div>

                        <div class="col-md-4">
                            <div class="form-group">
                              <label>Fecha de Pago:</label>
                              <input type="date" step="any" class="form-control" name="fecha_pago" id="fecha_pago">
                            </div>
                        </div> 
                            
                        </div>

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
                         <div class="col-md-4" >
                                <div class="form-group">
                                  <label>Monto Demanda:</label>
                                  <input type="text" class="form-control" min="0"  name="monto_recibido" id="monto_demanda" pattern="^\$\d{1,3}(,\d{3})*(\.\d+)?$" data-type="currency" placeholder="Q1,000.00" pattern="^[0-9]+" onpaste="return false;" onDrop="return false;" autocomplete=off>
                                </div>
                            </div>
                  </div>
                    <!-- Fin campos para el presentación de la demanda -->


                    <!-- Campos para el notificacion de la demanda -->
                    <div id="campos_notificacion">
                    <div class="col-md-6">
                        <div class="form-group">
                          <label>Admitida o rechazada:</label>
                          <select class="form-control" name="admitida" id="admitida">
                              <option >Seleccione</option>
                              <option value="1">Admitida</option>
                              <option value="2">Rechaza</option>
                          </select>
                        </div>
                    </div> 

                    <div class="col-md-6 admitida_detalles hidden">

                          <div class="form-group">                            

                            <input class="checkboxClassAd" type="checkbox" name="optionsAdmitida" value="Bancos" > Bancos <br>
                            <input class="checkboxClassAd" type="checkbox" name="optionsAdmitida" value="Salario"> Salario<br>
                            <input class="checkboxClassAd" type="checkbox" name="optionsAdmitida" value="Otro"> Otros<br><br>
                            <input type="text" name="option_otro_admitida" id="option_otro_admitida" class="form-control" disabled="Disabled"/>
                                
                            </div>

                    </div>     
                        
                    </div> 
                    <!-- Fin campos para el notificacion de la demanda -->


                    <!-- Campos para la transacción de la demanda -->

                    <div id="mont_transaccion">

                        <div class="col-md-4" >
                            <div class="form-group">
                                <label>Pago Recibido:</label>
                                <input type="text" class="form-control" min="0"  name="monto_recibido_transaccion" id="monto_recibido_transaccion" pattern="^\$\d{1,3}(,\d{3})*(\.\d+)?$" data-type="currency" placeholder="Q1,000.00" pattern="^[0-9]+" onpaste="return false;" onDrop="return false;" autocomplete=off>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="form-group">
                              <label>Fecha Fin de Pago:</label>
                              <input type="date" step="any" class="form-control" name="fecha_fin_transaccion" id="fecha_fin_transaccion">
                            </div>
                        </div> 

                        <div class="col-md-6">
                            <div class="form-group">                            

                            <input class="checkboxClass" type="checkbox" name="optionsDesestimiento" value="Bancos" > Bancos <br>
                            <input class="checkboxClass" type="checkbox" name="optionsDesestimiento" value="Salario"> Salario<br>
                            <input class="checkboxClass" type="checkbox" name="optionsDesestimiento" value="Otro"> Otros<br><br>
                            <input type="text" name="option_otro_desestimiento" id="option_otro_desestimiento" class="form-control" disabled="Disabled"/>
                                
                            </div>

                    </div>  

                  </div>

                    <div id="campos_sentencia">

                        <div class="col-md-6">
                        <div class="form-group">
                          <label>Resultado:</label>
                          <select class="form-control" name="opciones_sentencia" id="opciones_sentencia">
                              <option value="A_FAVOR">A favor</option>
                              <option value="EN_CONTRA">En contra</option>
                          </select>
                        </div>
                    </div> 

                    </div>

                    <!-- Fin campos para la transacción de la demanda -->

                    <div class="col-md-12">
                        <div class="form-group">
                          <label>Detalles:</label>
                            <textarea class="form-control" name="detalles" id="detalles" style="height: 200px;"></textarea>
                           <%-- <input type="text" class="form-control" name="detalles" id="detalles">--%>
                        </div>
                    </div> 

                    <div class="col-md-12">

                        <div class="form-group">

                            <input class="checkVista" type="checkbox" name="opcionVista" value="1"> Proceso en Vista<br>

                        </div>

                    </div>
         

                </div>

            </div>

        <!--=====================================
        PIE DEL MODAL
        ======================================-->

        <div class="modal-footer">

        <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Salir</button>

        <asp:Button class="btn btn-danger" Text="Registar" ID="btn_registrar" OnClick="btn_registrar_Click" ClientIDMode="Static" runat="server"/>

        </div>

        </div>

    </div>

</div>






 







<div id="ver_historial" class="modal fade" role="dialog">
  
    <div class="modal-dialog">

        <div class="modal-content" style="width:800px !important">

        <!--=====================================
        CABEZA DEL MODAL
        ======================================-->

            <div class="modal-header" style="background:#27333a; color:white">

            <button type="button" class="close" data-dismiss="modal">&times;</button>

            <h4 class="modal-title">Historial del Caso 
            </h4>

            </div>

        <!--=====================================
        CUERPO DEL MODAL
        ======================================-->

            <div class="modal-body">

                <div class="box-body">

                    <h4>Datos del Cliente</h4>

                        <div class="col-xs-12">
                            <div class="form-group">
                              <label>No. Expediente:</label>
                                <b id="num_exp"></b>
                            </div>
                        </div> 

                        <div class="col-xs-12"> 
                            <div class="form-group">
                              <label>No. Operacion:</label>
                                <b id="num_ope"></b>
                            </div>
                            <hr />
                        </div>                                         


                                

           <table id="historial" class="table table-striped table-hover" style='width:100%' ">
                <thead>
                    <tr>
                        <th>Etapa</th>
                       
                        <th>Subetapa</th>
                        <th>Observaciones</th>
                        <th>Fecha</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
                <tfoot>
                    <tr>
                        <th>Etapa</th>
                        <th>Subetapa</th>
                        <th>Observaciones</th>
                        <th>Fecha</th>
                    </tr>
                </tfoot>
            </table>

            </div>

        </div>

            <!--=====================================
        PIE DEL MODAL
        ======================================-->

        <div class="modal-footer">

            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Salir</button>

          <%--  <asp:Button class="btn btn-danger" Text="Registar" ID="registrar_movimiento_caso" ClientIDMode="Static" runat="server"/>--%>

        </div>

    </div>

  </div>

</div>






       <!--==============================================================================================================
        MODAL PARA VER PDF
    ===============================================================================================================-->
    <div id="ver_pdf" class="modal fade" role="dialog">
  
    <div class="modal-dialog">

        <div class="modal-content" style="width:800px !important">

        <!--=====================================
        CABEZA DEL MODAL
        ======================================-->

            <div class="modal-header" style="background:#27333a; color:white">

            <button type="button" class="close" data-dismiss="modal">&times;</button>

            <h4 class="modal-title">Historial PDF'S 
            </h4>

            </div>

        <!--=====================================
        CUERPO DEL MODAL
        ======================================-->

            <div class="modal-body">

                <div class="box-body">

                                                           

                      <div class="col-xs-12">
                            <div class="form-group">
                              <label>No. Expediente:</label>
                                <b id="num_exp_pdf_mostrar"></b>
                            </div>
                        </div> 

                        <div class="col-xs-12"> 
                            <div class="form-group">
                              <label>No. Operacion:</label>
                                <b id="num_ope_pdf_mostrar"></b>
                            </div>
                            <hr />
                        </div>                                         



                    <table id="historial_pdf" class="table table-striped table-hover" style='width:100%' ">
                <thead>
                    <tr>
                        <th>Etapa</th>
                       
                        <th>Subetapa</th>
                        <th>Observaciones</th>
                        <th>PDF</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
                <tfoot>
                    <tr>
                        <th>Etapa</th>
                        <th>Subetapa</th>
                        <th>Observaciones</th>
                        <th>PDF</th>
                    </tr>
                </tfoot>
            </table>

            </div>

        </div>

            <!--=====================================
        PIE DEL MODAL
        ======================================-->

        <div class="modal-footer">

            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Salir</button>

          <%--  <asp:Button class="btn btn-danger" Text="Registar" ID="registrar_movimiento_caso" ClientIDMode="Static" runat="server"/>--%>

        </div>

    </div>

  </div>

</div>




    <!--===============================================================================================================
    =================================================================================================================-->





<div id="ver_detalles_demanda" class="modal fade" role="dialog">
  
    <div class="modal-dialog">

        <div class="modal-content" style="width:600px !important">

        <!--=====================================
        CABEZA DEL MODAL
        ======================================-->

            <div class="modal-header" style="background:#27333a; color:white">

            <button type="button" class="close" data-dismiss="modal">&times;</button>

            <h4 class="modal-title">Información del Caso 
            </h4>

            </div>

        <!--=====================================
        CUERPO DEL MODAL
        ======================================-->

            <div class="modal-body">

                <div class="box-body">

                    <div class="col-xs-12">
                       <center><h4 style="font-weight:bold">Datos del Deudor</h4></center> 
                    </div>

                <div class="col-xs-6">
                    <div class="form-group">
                        <label>No. Expediente:</label>
                        <input class="form-control" type="text" name="num_exp_" id="num_exp_" readonly/>
                    </div>
                </div>   

                <div class="col-xs-6">
                    <div class="form-group">
                        <label>No. Operación:</label>
                        <input class="form-control" type="text" name="num_ope_" id="num_ope_" readonly/>
                    </div>
                </div>  

                    <div class="col-xs-12">
                    <div class="form-group">
                        <label>Nombre:</label>
                        <input class="form-control" type="text" name="nom_cli_" id="nom_cli_" readonly/>
                    </div>
                </div>  

                    <div class="col-xs-6">
                    <div class="form-group">
                        <label>Etapa:</label>
                        <input class="form-control" type="text" name="etapa_" id="etapa_" readonly/>
                    </div>
                </div>  

                    <div class="col-xs-6">
                    <div class="form-group">
                        <label>Suetapa:</label>
                        <input class="form-control" type="text" name="subetapa_" id="subetapa_" readonly/>
                    </div>
                </div>  

                    <div class="col-xs-12">
                       <center><h4 style="font-weight:bold">Datos Arreglo de Pago</h4></center> 
                    </div>

                    <div class="col-xs-6">
                    <div class="form-group">
                        <label>Tipo de Pago:</label>
                        <input class="form-control" type="text" name="tipo_pago_" id="tipo_pago_" readonly/>
                    </div>
                </div>  

                    <div class="col-xs-6">
                    <div class="form-group">
                        <label>Monto Cancelar:</label>
                        <input class="form-control" type="text" name="monto_canc_" id="monto_canc_" readonly/>
                    </div>
                </div>  

                    <div class="col-xs-6">
                    <div class="form-group">
                        <label>Monto Mensual:</label>
                        <input class="form-control" type="text" name="monto_mens_" id="monto_mens_" readonly/>
                    </div>
                </div> 

                    <div class="col-xs-6">
                    <div class="form-group">
                        <label>Cantidad de tractos:</label>
                        <input class="form-control" type="text" name="tract_" id="tract_" readonly/>
                    </div>
                </div> 

                    <div class="col-xs-6">
                    <div class="form-group">
                        <label>Fecha Inicio Pago:</label>
                        <input class="form-control" type="text" name="fin_ini_" id="fin_ini_" readonly/>
                    </div>
                </div> 
                    <div class="col-xs-6">
                    <div class="form-group">
                        <label>Fecha Fin Pago:</label>
                        <input class="form-control" type="text" name="fin_pag_" id="fin_pag_" readonly/>
                    </div>
                </div> 

                    <div class="col-xs-12">
                       <center><h4 style="font-weight:bold">Datos Judiciales</h4></center> 
                    </div>

                    <div class="col-xs-12">
                    <div class="form-group">
                        <label>No. Proceso:</label>
                        <input class="form-control" type="text" name="num_proc_" id="num_proc_" readonly/>
                    </div>
                </div> 

                    <div class="col-xs-6">
                    <div class="form-group">
                        <label>Nombre Juzgado:</label>
                        <input class="form-control" type="text" name="nomb_juzg_" id="nomb_juzg_" readonly/>
                    </div>
                </div> 

                    <div class="col-xs-6">
                    <div class="form-group">
                        <label>Tipo Proceso:</label>
                        <input class="form-control" type="text" name="tipo_proc_" id="tipo_proc_" readonly/>
                    </div>
                </div> 

                    <div class="col-xs-6">
                    <div class="form-group">
                        <label>Oficial a Cargo:</label>
                        <input class="form-control" type="text" name="ofic_acarg_" id="ofic_acarg_" readonly/>
                    </div>
                </div> 


                    <div class="col-xs-12">
                        <div class="form-group">
                        <label>Observaciones:</label>
                        <textarea class="form-control" name="observaciones_" id="observaciones_" readonly></textarea>
                    </div>
                    </div>

        <!--=====================================
        PIE DEL MODAL
        ======================================-->       

            </div>

        </div>

            <div class="modal-footer">

                <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Salir</button>
                

         </div>

    </div>

  </div>

</div>

    <!--============================================ modal para cargar pdf================================================ -->
    <div id="cargar_pdf" class="modal fade" role="dialog">
  
    <div class="modal-dialog">

        <div class="modal-content" style="width:600px !important">

        <!--=====================================
        CABEZA DEL MODAL
        ======================================-->

            <div class="modal-header" style="background:#27333a; color:white">

            <button type="button" class="close" data-dismiss="modal">&times;</button>

            <h4 class="modal-title">CARGAR PDF'S
            </h4>

            </div>

        <!--=====================================
        CUERPO DEL MODAL
        ======================================-->

            <div class="modal-body">

                <div class="box-body">
                
                    
                       
                    
                  

                  <div class="col-md-12 col-xs-12">
                        <div class="form-group" novalidate>
                          <label>Seleccione Etapa:</label>
                            <input type="hidden"  name="num_exp_pdf" id="num_exp_pdf"/>
                            
                         

                          <select class="form-control"  required  name="etapa_pdf" id="etapa_pdf"    aria-required="true">
                               <option  selected value="0" >SELECCIONE UNA OPCION</option>




                          </select>
                        </div>
                    </div> 
               

                        <div class="col-xs-12">
                        <div class="form-group">
                          <label>Seleccione Documentos Relacionados con el proceso:</label>

                            <input type="file" name="archivos" id="File1" ClientIDMode="Static" runat="server" multiple="multiple"/>
                        </div>
                    </div> 

                     <div class="col-md-12">
                        <div class="form-group">
                          <label>Detalles:</label>
                            <textarea class="form-control" name="detalles_pdf" id="detalles_pdf" style="height: 200px;"></textarea>
                          
                           <%-- <input type="text" class="form-control" name="detalles" id="detalles">--%>
                        </div>
                    </div> 



                   
               

                 

                    

        <!--=====================================
        PIE DEL MODAL
        ======================================-->       

            </div>

        </div>

            <div class="modal-footer">

                <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Salir</button>
                <asp:Button class="btn btn-danger" Text="Registar" ID="Button5" OnClick="btn_registrar_pdf_Click" ClientIDMode="Static" runat="server"/>

         </div>

    </div>

  </div>

</div>


    <!--============================================ termina modal carga pdf ============================================ -->

    <% if (Session["administrador"].ToString() == "1")
        { %>
    <div id="ventana_cerrar_caso" class="modal fade" role="dialog">
  
    <div class="modal-dialog">

        <div class="modal-content" style="width:600px !important">

        <!--=====================================
        CABEZA DEL MODAL
        ======================================-->

            <div class="modal-header" style="background:#27333a; color:white">

            <button type="button" class="close" data-dismiss="modal">&times;</button>

            <h4 class="modal-title">Cerrar Caso
            </h4>

            </div>

        <!--=====================================
        CUERPO DEL MODAL
        ======================================-->

            <div class="modal-body">

                <div class="box-body">
                    <input type="hidden" name="num_expedient" id="num_expedient"/>

                    <textarea style="height: 300px; width:550px;" id="juztificacion" name="juztificacion"></textarea>

        <!--=====================================
        PIE DEL MODAL
        ======================================-->       

            </div>

        </div>

            <div class="modal-footer">

                <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Salir</button>
                <asp:Button class="btn btn-danger" Text="Finalizar Caso" ID="btn_finalizar_caso" ClientIDMode="Static" runat="server"/>

         </div>

    </div>

  </div>

</div>
    <% } %>

        <div id="ventana_anular_caso" class="modal fade" role="dialog">
  
    <div class="modal-dialog">

        <div class="modal-content" style="width:600px !important">

        <!--=====================================
        CABEZA DEL MODAL
        ======================================-->

            <div class="modal-header" style="background:#27333a; color:white">

            <button type="button" class="close" data-dismiss="modal">&times;</button>

            <h4 class="modal-title" id="titulo-anulacion">Rechazar Caso</h4>

            </div>

        <!--=====================================
        CUERPO DEL MODAL
        ======================================-->

            <div class="modal-body">

                <div class="box-body">

                       <input type="hidden" name="num_expedient2" id="num_expedient2"/>

                    <textarea style="height: 300px; width:550px;" id="justificacion_anulacion" name="justificacion_anulacion"></textarea>

            </div>

        </div>
        <!--=====================================
        PIE DEL MODAL
        ======================================-->    


            <div class="modal-footer">

                <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Salir</button>
                <asp:Button class="btn btn-danger" Text="Rechazar" ID="btn_anular_caso" ClientIDMode="Static" runat="server"/>
                

         </div>

    </div>

  </div>

</div>

    
    <script src="Scripts/js/Listas_Proceso.js"></script>
    <script src="Scripts/sweetalert2/sweetalert2.all.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.maskedinput/1.4.1/jquery.maskedinput.js"></script>
    <script>
        $(document).ready(function () {
            $("#numero_proceso").mask("99999-9999-9999", { placeholder: "XXXXX-XXXX-XXXX" });
        })

    </script>

    
</asp:Content>
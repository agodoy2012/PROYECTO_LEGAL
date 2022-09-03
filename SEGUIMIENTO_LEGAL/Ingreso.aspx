<%@ Page Title="Ingreso" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ingreso.aspx.cs" Inherits="SEGUIMIENTO_LEGAL.Ingreso" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
      <h1>
        Ingreso de Nuevos Casos
      </h1>
      <ol class="breadcrumb">
        <li ><a href="Inicio"><i class="fa fa-dashboard"></i> Inicio</a></li>
        <li>Abrir Expediente Legal</li>
        <li class="active">Ingreso</li>
      </ol>
    </section>

    <section class="content">

    <div class="box">

            <div class="box-header with-border">
                    <i class="fa fa-folder-open"></i><h3 class="box-title">Nuevo Caso</h3>
             </div>

            <div class="box-body">
                <div class="row">

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Número de Operación:</label>
                          <input type="text" class="form-control" name="num_ope" id="num_ope" placeholder="" required="required">
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Nombre Cliente:</label>
                          <input type="text" class="form-control" name="nom_cli" id="nom_cli" placeholder="" required="required">
                        </div>
                    </div>   

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>NIT:</label>
                          <input type="text" class="form-control" name="nit" id="nit" placeholder="">
                        </div>
                    </div>  

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>DPI:</label>
                          <input type="text" class="form-control" name="dpi" id="dpi" placeholder="">
                        </div>
                    </div>  

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Correlativo:</label>
                          <input type="number" class="form-control" name="correlativo" id="correlativo" required="required">
                        </div>
                    </div>  

                    <div class="col-md-4">
                        <div class="form-group">
                          <label>Seleccione Subetapa:</label>
                          <select class="form-control" name="subetapa" id="subetapa" required="required"></select>
                        </div>
                    </div> 

                    <div class="col-xs-12">
                        <div class="form-group">
                          <label>Seleccione Documentos Relacionados con el proceso:</label>

                            <input type="file" ClientIDMode="Static" name="archivos" id="archivos" runat="server" multiple="multiple"/>
                        </div>
                    </div> 

                    <div class="col-md-12">
                        <div class="form-group">
                          <label>Detalles:</label>
                            <textarea class="form-control" name="detalles" id="detalles"></textarea>
                           <%-- <input type="text" class="form-control" name="detalles" id="detalles">--%>
                        </div>
                    </div> 

                    <div class="col-md-4">

                       <%-- <asp:Button ID="btn_ingresar" class="btn btn-danger" ClientIDMode="Static" runat="server" Text="Guardar" OnClick="btn_ingresar_Click"/>--%>
                       <%-- <asp:Button ID="btn_ingresar" class="btn btn-danger" ClientIDMode="Static" runat="server" Text="Guardar"/>--%>
                        <button id="btn_ingresar" class="btn btn-sm btn-success">Guardar</button>

                    </div>
                    
                </div>
                
              
            </div>

            <div class="box-footer">
              Área Legal Guatemala
            </div>

        </div>
   </section>
    <script src="Scripts/sweetalert2/sweetalert2.all.js"></script>
   <script src="Scripts/js/Ingreso.js"></script>

</asp:Content>
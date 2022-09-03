<%@ Page Title="Creacion_Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Creacion_Usuarios.aspx.cs" Inherits="SEGUIMIENTO_LEGAL.Creacion_Usuarios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
      <h1>
        Ingreso de Usuarios
      </h1>
      <ol class="breadcrumb">
        <li ><a href="Inicio"><i class="fa fa-dashboard"></i> Inicio</a></li>
        <li>Control de Usuarios</li>
        <li class="active">Ingreso de Usuarios</li>
      </ol>
    </section>

     <section class="content">

        <div class="box">

            <div class="box-header with-border">

                    <i class="fa fa-users"></i><h3 class="box-title">Creación de Usuarios</h3>
             </div>

            <div class="box-body">

                <div class="col-md-6">
                    <div class="form-group">
                        <label>Nombre Completo:</label>
                        <input type="text" class="form-control" name="nombre_completo" id="nombre_completo" placeholder="" required="required">
                    </div>
                 </div>

                <div class="col-md-3">
                    <div class="form-group">
                        <label>Usuario:</label>
                        <input type="text" class="form-control" name="usuario" id="usuario" placeholder="" required="required">
                    </div>
                 </div>

                <div class="col-md-3">
                    <div class="form-group">
                        <label>Contraseña:</label>
                        <input type="password" class="form-control" name="clave" id="clave" placeholder="" required="required">
                    </div>
                 </div>

                <div class="col-md-4">
                    <div class="form-group">
                        <label>Perfil:</label>
                        <select class="form-control" name="perfil_usuario" id="perfil_usuario" required="required">
                            <option value="1">Administrador</option>
                            <option value="2">Registro y Consulta</option>

                        </select>
                    </div>
                 </div>

            </div>

            <div class="box-footer">
              <asp:Button ID="btn_ingresar" class="btn btn-sm btn-success" ClientIDMode="Static" runat="server" Text="Guardar"/>
            </div>

        </div>

    </section>
    <script src="Scripts/js/Usuarios.js"></script>
    <script src="Scripts/sweetalert2/sweetalert2.all.js"></script>
</asp:Content>
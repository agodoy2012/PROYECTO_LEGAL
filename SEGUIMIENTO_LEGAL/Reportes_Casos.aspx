<%@ Page Title="Reportes_Casos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes_Casos.aspx.cs" Inherits="SEGUIMIENTO_LEGAL.Reportes_Casos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
      <h1>
        Reportes de Casos
      </h1>
      <ol class="breadcrumb">
        <li ><a href="Inicio"><i class="fa fa-dashboard"></i> Inicio</a></li>
        <li>Lista de Casos</li>
        <li class="active">Reportes de Casos</li>
      </ol>
    </section>


    <section class="content">

        <div class="box">

            <div class="box-header with-border">

                    <i class="fa fa-file-text"></i><h3 class="box-title">Generación de Reportes</h3>
             </div>

            <div class="box-body">

                <div class="form-group col-md-2">
                    <label>Estado del Caso:</label>
                    <select class="form-control" id="estado_caso" name="estado_caso">
                        <option>Seleccionar</option>
                        <option value="1">Admitida</option>
                        <option value="2">Rechazada</option>
                    </select>
                </div>
                <div class="form-group col-md-2">
                    <label>Oficios del Caso:</label>
                    <select class="form-control" id="oficios_caso" name="oficios_caso">
                        <option>Seleccionar</option>
                        <option value="1">Tramitados</option>
                        <option value="2">Pendientes</option>
                    </select>
                </div>
                <div class="form-group col-md-2">
                    <label>Documentación de Caso:</label>
                    <select class="form-control" id="documentacion_devuelta_caso" name="documentacion_devuelta_caso">
                        <option>Seleccionar</option>
                        <option value="0">En espera</option>
                        <option value="1">Devueltos</option>
                        <option value="2">Sin devolución</option>
                    </select>
                </div>
                <div class="form-group col-md-2">
                    <label>Sentencia de Casos:</label>
                    <select class="form-control" id="sentencia_casos" name="sentencia_casos">
                        <option>Seleccionar</option>
                        <option value="1">A favor</option>
                        <option value="2">En contra</option>
                    </select>
                </div>
                <div class="form-group col-md-12">
                    <button class="btn btn-danger">Buscar</button>
                </div>
                
            </div>

        </div>

    </section>


</asp:Content>




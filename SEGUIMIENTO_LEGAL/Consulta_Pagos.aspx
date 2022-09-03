<%@ Page Title="Consulta_Pagos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Consulta_Pagos.aspx.cs" Inherits="SEGUIMIENTO_LEGAL.Consulta_Pagos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
      <h1>
        Consulta de Pagos
      </h1>
      <ol class="breadcrumb">
        <li ><a href="Inicio"><i class="fa fa-dashboard"></i> Inicio</a></li>
        <li>Registro de Pagos</li>
        <li class="active">Consulta de Pagos</li>
      </ol>
    </section>

    <section class="content">

        <div class="box">

            <div class="box-header with-border">

                    <i class="fa fa-search"></i><h3 class="box-title">Consulta de Pagos</h3>
             </div>

            <div class="box-body">

                <table id="lista_pagos" class="table table-striped table-hover" style='width:100%' ">
                <thead>
                    <tr>
                        <th>No. Expediente</th>
                        <th>No. de Operacion</th>
                        <th>Cliente</th>
                        <th>Identificación</th>
                        <th>Monto Inicial</th>
                        <th>Monto Cargos</th>
                        <th>Monto Recibido</th>
                        <th>Saldo</th>
                        <th>Estado</th>
                        

                    </tr>
                </thead>
                <tbody>
                </tbody>
                <tfoot>
                    <tr>
                        <th>No. Expediente</th>
                        <th>No. de Operacion</th>
                        <th>Cliente</th>
                        <th>Identificación</th>
                        <th>Monto Inicial</th>
                        <th>Monto Cargos</th>
                        <th>Monto Recibido</th>
                        <th>Saldo</th>
                        <th>Estado</th>

                    </tr>
                </tfoot>
            </table>



            </div>

            <div class="box-footer">
             Gestionadora de Créditos
            </div>


            </div>

            
        </section>
    <script src="Scripts/js/Lista_Pagos.js"></script>

</asp:Content>

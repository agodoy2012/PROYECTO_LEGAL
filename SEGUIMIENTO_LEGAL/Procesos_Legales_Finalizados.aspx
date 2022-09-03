<%@ Page Title="Procesos_Legales_Finalizados" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Procesos_Legales_Finalizados.aspx.cs" Inherits="SEGUIMIENTO_LEGAL.Procesos_Legales_Finalizados" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   <section class="content-header">
      <h1>
        Procesos Finalizados
      </h1>
      <ol class="breadcrumb">
        <li ><a href="Inicio"><i class="fa fa-dashboard"></i> Inicio</a></li>
        <li>Procesos Finalizados</li>
        <li class="active">Procesos Finalizados</li>
      </ol>
    </section>

    <section class="content">

        <div class="box">

            <div class="box-header with-border">

                    <i class="fa fa-flag"></i><h3 class="box-title">Procesos Finalizados</h3>
             </div>

            <div class="box-body">

                <table id="lista_finalizadas" class="table table-striped table-hover" style='width:100%' ">
                <thead>
                    <tr>
                        <th>No. Expediente</th>
                        <th>No. de Operacion</th>
                        <th>Cliente</th>
                        <th>Identificación</th>
                        <th>Juztificación</th>
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
                        <th>Juztificación</th>
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

    <script src="Scripts/js/Listas_Finalizadas.js"></script>

</asp:Content>

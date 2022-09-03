<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="SEGUIMIENTO_LEGAL.Inicio" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Inicio
      </h1>
      <ol class="breadcrumb">
        <li class="active"><a href="Inicio"><i class="fa fa-dashboard"></i> Inicio</a></li>
          <!--<li><a href="#">Examples</a></li>
          <li>Blank page</li>-->
      </ol>
    </section>

    <section class="content">

        <!--  Guatemala   -->
        <div class="row">

            <%
                SEGUIMIENTO_LEGAL.Data.Conexion cxn = new SEGUIMIENTO_LEGAL.Data.Conexion();

                decimal porcentaje = cxn.obtener_total_etapa_gt(1) / cxn.obtener_total_etapas_gt() * 100;
                string pornc = porcentaje.ToString("##.00");
                %>

        <div class="col-lg-2 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-aqua">
            <div class="inner">
              <h3><%=pornc%><sup style="font-size: 20px">%</sup></h3>

              <p>Preparación</p>
            </div>
            <div class="icon">
              <i class="ion ion-bag"></i>
            </div>
            <a href="Procesos_Legales?etapa=1" class="small-box-footer">Detalles <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>



        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-green">
            <div class="inner">
                <%porcentaje = cxn.obtener_total_etapa_gt(2) / cxn.obtener_total_etapas_gt() * 100;
                     pornc = porcentaje.ToString("##.00");
                    %>
              <h3><%=pornc %><sup style="font-size: 20px">%</sup></h3>

              <p>Arreglo Extra Judicial</p>
            </div>
            <div class="icon">
              <i class="ion ion-stats-bars"></i>
            </div>
            <a href="Procesos_Legales?etapa=2" class="small-box-footer">Detalles <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-yellow">
            <div class="inner">
              <%porcentaje = cxn.obtener_total_etapa_gt(3) / cxn.obtener_total_etapas_gt() * 100; 
                  pornc = porcentaje.ToString("##.00");%>
              <h3><%=pornc %><sup style="font-size: 20px">%</sup></h3>

              <p>Presentación Demanda</p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="Procesos_Legales?etapa=3" class="small-box-footer">Detalles <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-2 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-red">
            <div class="inner">
              <%porcentaje = cxn.obtener_total_etapa_gt(4) / cxn.obtener_total_etapas_gt() * 100; 
                  pornc = porcentaje.ToString("##.00");%>
              <h3><%=pornc %><sup style="font-size: 20px">%</sup></h3>

              <p>Arreglo Judicial</p>
            </div>
            <div class="icon">
              <i class="ion ion-pie-graph"></i>
            </div>
            <a href="Procesos_Legales?etapa=4" class="small-box-footer">Detalles <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>

         <div class="col-lg-2 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-gray">
            <div class="inner">
              <%porcentaje = cxn.obtener_total_etapa_gt(5) / cxn.obtener_total_etapas_gt() * 100; 
                  pornc = porcentaje.ToString("##.00");%>
              <h3><%=pornc %><sup style="font-size: 20px">%</sup></h3>

              <p>Diligenciamiento</p>
            </div>
            <div class="icon">
              <i class="ion ion-pie-graph"></i>
            </div>
            <a href="Procesos_Legales?etapa=5" class="small-box-footer">Detalles <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <!-- ./col -->
      </div>

        <!--  Fin Guatemala   -->

        <div class="box">

            <div class="box-header with-border">
                    <i class="fa fa-archive"></i><h3 class="box-title">Total de Casos en Proceso Legal</h3>
             </div>

            <div class="box-body">
              <canvas id="myChart" class="chartjs" width="770" height="385" style="display: block; width: 770px; height: 385px !important;"></canvas>
            </div>

            <div class="box-footer">
              Área Legal Guatemala
            </div>

        </div>

    </section>
    




    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/Chart.js"></script>
    <script src="Scripts/flot/jquery.flot.js"></script>
    <script src="Scripts/flot/jquery.flot.pie.js"></script>
    <script src="Scripts/flot/jquery.flot.resize.js"></script>
    <script src="Scripts/flot/jquery.flot.categories.js"></script>
    
    <script>

        <%

        //SEGUIMIENTO_LEGAL.Data.Conexion cxn = new SEGUIMIENTO_LEGAL.Data.Conexion();
        List<string> nombres_etapas = cxn.obtener_nombres_etapas();
        List<int> cantidades = cxn.obtener_cantidad_etapas_gt();


        string nombres = "[";

        for(int i = 0; i < (nombres_etapas.Count - 1); i++)
        {
            nombres += "'"+nombres_etapas[i]+"',";
        }

        nombres += "'"+nombres_etapas[nombres_etapas.Count - 1]+"']";

        string cantid = "[";

        for(int j = 0; j < (cantidades.Count - 1); j++)
        {
            cantid += "'" + cantidades[j] + "',";
        }

        cantid += "'" + cantidades[cantidades.Count - 1] + "']";

        %>

        

         var ctx = document.getElementById("myChart");
        var bar;

        var myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: <%=nombres%>,
                datasets: [{
                    label: 'Cantidad de Registros',
                    data: <%=cantid%>,
                    //data: [68500, 3252,4255,8421,2040 ],
                    backgroundColor: [
                        'rgb(223, 104, 104)',
                        '#00c0ef',
                        '#00a65a',
                        '#f39c12',
                        '#f56954',
                        '#d2d6de'
                    ],
                    borderColor: [
                        'rgb(223, 104, 104)',
                        '#00c0ef',
                        '#00a65a',
                        '#f39c12',
                        '#f56954',
                        '#d2d6de'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },

                legend: {
                    labels: {
                        // This more specific font property overrides the global property
                        fontColor: 'black'
                    }
                }

            }
        });

        var donutData = [
        { label: 'Preparación', data: 20, color: '#3c8dbc' },
        { label: 'Arreglo Extra Judiacial',data: 20, color: '#6C7EE3' },
        { label: 'Presentación Demanda', data: 20, color: '#00c0ef' },
        { label: 'Arreglo Judicial', data: 20, color: '#76D7C4' },
        { label: 'Diligenciamiento', data: 20, color: '#7DCEA0' }
        ]
        $.plot('#donut-chart', donutData, {
            series: {
                pie: {
                    show: true,
                    radius: 1,
                    innerRadius: 0.5,
                    label: {
                        show: true,
                        radius: 2 / 3,
                        formatter: labelFormatter,
                        threshold: 0.1
                    }

                }
            },
            legend: {
                show: false
            }
        })
        /*
         * END DONUT CHART
         */

        function labelFormatter(label, series) {
            return '<div style="font-size:13px; text-align:center; padding:2px; color: #343342; font-weight: 600;">'
                + label
                + '<br>'
                + Math.round(series.percent) + '%</div>'
        }


    </script>
</asp:Content>

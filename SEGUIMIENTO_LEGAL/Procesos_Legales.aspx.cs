using SEGUIMIENTO_LEGAL.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML;

namespace SEGUIMIENTO_LEGAL
{
    public partial class Procesos_Legales : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["usuario"] == null)
                {

                    Response.Redirect("login");

                }
            }
        }

        protected void btn_registrar_Click(object sender, EventArgs e)
        {
            Conexion cxn = new Conexion();
            Escritor error = new Escritor();
            string rutas = "";

            try
            {
     
                if (Request.Files.Count > 0)
                {

                    string vRutaLocal = Server.MapPath("~/");
                    string rutaCarpeta = "";
            

                    if (Request.Files.Count > 0)
                    {
                        HttpFileCollection files = Request.Files;

                        rutaCarpeta = Path.Combine(vRutaLocal, "Archivos/" + Request.Form["num_expediente"] + "/");

                        if (!System.IO.Directory.Exists(rutaCarpeta))
                        {
                            System.IO.Directory.CreateDirectory(rutaCarpeta);
                        }

                        for (int i = 0; i < files.Count; i++)
                        {
                            HttpPostedFile file = files[i];

                            string fname = Path.Combine(vRutaLocal, "Archivos/" + Request.Form["num_expediente"] + "/" + file.FileName);

                            if (file.FileName != "")
                            {
                                rutas += "Archivos/" + Request.Form["num_expediente"] + "/" + file.FileName  ;

                                file.SaveAs(fname);
                            }
                            else
                            {
                                rutas = "";
                            }
                        }
                         
                    }

                    //    cxn.registrar_rutas(rutas, int.Parse(Request.Form["num_expediente"]));

                }

                }
            catch (Exception ex)
            {

                error.logError(ex);
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////// GUARDAR PDF'S EN LA BASE DE DATOS ////////////////////////////////////////////////////

        protected void btn_registrar_pdf_Click(object sender, EventArgs e)
        {
            Conexion cxn = new Conexion();
            Escritor error = new Escritor();
            string rutas = "";

            try
            {

                if (Request.Files.Count > 0)
                {

                    string vRutaLocal = Server.MapPath("~/");
                    string rutaCarpeta = "";


                    if (Request.Files.Count > 0)
                    {
                        HttpFileCollection files = Request.Files;

                        rutaCarpeta = Path.Combine(vRutaLocal, "Archivos/" + Request.Form["num_exp_pdf"] + "/");

                        if (!System.IO.Directory.Exists(rutaCarpeta))
                        {
                            System.IO.Directory.CreateDirectory(rutaCarpeta);
                        }

                        for (int i = 0; i < files.Count; i++)
                        {
                            HttpPostedFile file = files[i];

                            string fname = Path.Combine(vRutaLocal, "Archivos/" + Request.Form["num_exp_pdf"] + "/" + file.FileName);

                            if (file.FileName != "")
                            {
                                rutas += "Archivos/" + Request.Form["num_exp_pdf"] + "/" + file.FileName ;

                                file.SaveAs(fname);
                            }
                            else
                            {
                                rutas = "";
                            }
                        }

                    }

                    var etapa = Request.Form["etapa_pdf"];

                    var numero = Request.Form["etapa_pdf"];

                    var detalles = Request.Form["detalles_pdf"];
                   cxn.registrar_rutas_pdf(rutas, int.Parse(Request.Form["etapa_pdf"]), Request.Form["detalles_pdf"]);
                  

                }

            }
            catch (Exception ex)
            {

                error.logError(ex);
            }
        }





        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        protected void bExel_Click(object sender, EventArgs e)
        {

            string p_nombre = nombre_deudor.Value.ToString();
            string p_codigo_proceso = codigo_proceso.Value.ToString();
            string p_numero_operacion = numero_operacion.Value.ToString();
         

            int p_etapa = int.Parse(etapa_filtro.Value.ToString().Equals("") ? "0" : etapa_filtro.Value.ToString());

            int p_subetapa = int.Parse(subetapa_filtro.Value.ToString().Equals("") ? "0" : subetapa_filtro.Value.ToString());

            int p_estado = int.Parse(estado.Value.ToString());

            string fecha = "12/2/2022";

            Conexion cxn = new Conexion();

            DataTable data = cxn.obtener_expedientes_Reporte(p_nombre, p_codigo_proceso, p_numero_operacion, p_etapa,
                p_subetapa, p_estado, fecha);   
            generarExcel(data);

        }







        protected void bExel_Click_sin_act(object sender, EventArgs e)
        {

            string p_nombre = nombre_deudor.Value.ToString();
            string p_codigo_proceso = codigo_proceso.Value.ToString();
            string p_numero_operacion = numero_operacion.Value.ToString();


            int p_etapa = int.Parse(etapa_filtro.Value.ToString().Equals("") ? "0" : etapa_filtro.Value.ToString());

            int p_subetapa = int.Parse(subetapa_filtro.Value.ToString().Equals("") ? "0" : subetapa_filtro.Value.ToString());

            int p_estado = int.Parse(estado.Value.ToString());

            string fecha = "12/2/2022";

            Conexion cxn = new Conexion();

            DataTable data = cxn.obtener_expedientes_Reporte_sin_mov(p_nombre, p_codigo_proceso, p_numero_operacion, p_etapa,
                p_subetapa, p_estado, fecha);
            generarExcel(data);

        }
















        public void generarExcel(DataTable tabla)
        {

            ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
            wbook.Worksheets.Add(tabla, "Reporte");
            // Prepare the response
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Provide you file name here
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"REPORTE.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }
    }
}
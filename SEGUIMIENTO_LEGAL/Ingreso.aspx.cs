using SEGUIMIENTO_LEGAL.Data;
using SEGUIMIENTO_LEGAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEGUIMIENTO_LEGAL
{
    public partial class Ingreso : System.Web.UI.Page
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

        protected void btn_ingresar_Click(object sender, EventArgs e)
        {
            Conexion cxn = new Conexion();
            string rutas = "";
            Escritor error = new Escritor();

            try
            {            

            if (Request.Files.Count > 0)
            {
                string vRutaLocal = Server.MapPath("/");
                string rutaCarpeta = "";


                if (Request.Files.Count > 0)
                {
                    HttpFileCollection files = Request.Files;

                    rutaCarpeta = Path.Combine(vRutaLocal, "Archivos/" + Request.Form["correlativo"] + "/");

                    if (!System.IO.Directory.Exists(rutaCarpeta))
                    {
                        System.IO.Directory.CreateDirectory(rutaCarpeta);
                    }

                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];

                        string fname = Path.Combine(vRutaLocal, "Archivos/" + Request.Form["correlativo"] + "/" + file.FileName);

                        if (file.FileName != "")
                        {
                            rutas += "Archivos/" + Request.Form["correlativo"] + "/" + file.FileName + ";";

                            file.SaveAs(fname);
                        }
                        else
                        {
                            rutas = "";
                        }
                    }

                }

                   // cxn.registrar_rutas(rutas, int.Parse(Request.Form["num_expediente"]));

            }

            //Expediente_Judicial nuevo = new Expediente_Judicial();
            //Historial_Expediente historial = new Historial_Expediente();
            //Clientes cliente = new Clientes();
            //string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
            //int rspta = -1;

            //DateTime fecha_pago = DateTime.ParseExact("1900-01-01 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);

            //cliente.nombre_completo = Request.Form["nom_cli"].ToUpper();
            //cliente.dpi = Request.Form["dpi"];
            //cliente.nit = Request.Form["nit"];
            //cliente.usuario = usuario;//this user

            //int id_cliente = cxn.registrar_cliente(Request.Form["nom_cli"].ToUpper(), Request.Form["nit"], Request.Form["dpi"], usuario);

            //if (id_cliente > 0)
            //{

            //    nuevo.id = int.Parse(Request.Form["correlativo"]);
            //    nuevo.id_cliente = id_cliente;
            //    nuevo.numero_operacion = Request.Form["correlativo"];
            //    nuevo.etapa = 1;
            //    nuevo.subetapa = int.Parse(Request.Form["subetapa"]); ;
            //    nuevo.tipo_pago = 1;
            //    nuevo.via_pago = "NO ESTABLECIDO";
            //    nuevo.monto_cancelar = 0;
            //    nuevo.tractos = 0;
            //    nuevo.plazos = 0;
            //    nuevo.fecha_pago = 0;
            //    nuevo.fecha_fin_pago = fecha_pago;
            //    nuevo.id_proceso_judicial = 1;
            //    nuevo.nombre_juzgado = "NO ESTABLECIDO";
            //    nuevo.numero_proceso = "0";
            //    nuevo.oficial = "";
            //    nuevo.observaciones = "NO SE ESTABLECIERON DETALLES";
            //    nuevo.nombre_usuario = usuario;

            //    int numero_expediente = cxn.registrar_nuevo_expediente(nuevo);

            //    historial.id = numero_expediente;
            //    historial.etapa = 1;
            //    historial.subetapa = int.Parse(Request.Form["subetapa"]); ;
            //    historial.ruta_archivos = rutas;
            //    historial.detalles = "Caso Inicial";
            //    historial.usuario = usuario;

            //    rspta = cxn.registrar_historial_expediente(historial);

            //    if (rspta > 0)
            //    {

            //        Response.Write(@"<script>swal({
            //        title:'Buen Trabajo!',
            //        text:'Se ha abierto un nuevo expediente!',
            //        type:'success'
            //        })</script>");

            //    }
            //    else
            //    {
            //        Response.Write(@"<script>swal({
            //        title:'Disculpas!',
            //        text:'No se ha creó un nuevo expediente!',
            //        type:'danger'
            //        })</script>");
            //    }

            //}
      

            }
            catch (Exception ex)
            {

                error.logError(ex);
            }
        }

    }
}
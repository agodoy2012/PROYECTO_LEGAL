using SEGUIMIENTO_LEGAL.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEGUIMIENTO_LEGAL
{
    public partial class Arreglo_Extra_Judicial : System.Web.UI.Page
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

                    string vRutaLocal = Server.MapPath("/");
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
                                rutas += "Archivos/" + Request.Form["num_expediente"] + "/" + file.FileName + ";";

                                file.SaveAs(fname);
                            }
                            else
                            {
                                rutas = "";
                            }
                        }

                    }

                    cxn.registrar_rutas(rutas, int.Parse(Request.Form["num_expediente"]));

                }

            }
            catch (Exception ex)
            {

                error.logError(ex);
            }
        }
    }
}
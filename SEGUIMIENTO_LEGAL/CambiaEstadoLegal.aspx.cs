using SEGUIMIENTO_LEGAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEGUIMIENTO_LEGAL
{
    public partial class CambiaEstadoLegal : System.Web.UI.Page
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

        [System.Web.Services.WebMethod()]
        public static bool actualizarEstadoCaso(int p_estado, string p_operacion, int p_id_exp,
           string p_num_proces)
        {
            Conexion cxn = new Conexion();
            bool estado = cxn.actualizar_estado_caso(p_estado, p_operacion, p_id_exp, p_num_proces);
            
            return estado;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEGUIMIENTO_LEGAL
{
    public partial class Administrar_Seguimiento : System.Web.UI.Page
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
    }
}
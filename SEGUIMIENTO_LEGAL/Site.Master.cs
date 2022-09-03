using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEGUIMIENTO_LEGAL
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            annoFinal.Value = "Copyright  © 2018 - " +  DateTime.Now.ToString("yyyy") + ' ';
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEGUIMIENTO_LEGAL.Models
{
    public class Registros_Robot
    {
        public int id { set; get; }
        public string numero_operacion { set; get; }
        public string nombre_cliente { set; get; }
        public string identificacion { set; get; }
        public string lugar_trabajo { set; get; }
        public string mes { set; get; }
        public string anio { set; get; }
        public int procesado { set; get; }
    }
}
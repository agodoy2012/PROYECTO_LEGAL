using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEGUIMIENTO_LEGAL.Models
{
    public class Resumen_Expedientes_Legales
    {
        public int numero_expediente { set; get; }
        public string nombre_cliente { set; get; }
        public string numero_operacion { set; get; }
        public string etapa { set; get; }
        public string subetapa { set; get; }
        public string numero_proceso { set; get; }
        public string juzgado { set; get; }
        public string fecha_incluye { set; get; }
        public int estado { set; get; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEGUIMIENTO_LEGAL.Models
{
    public class Expedientes_Finalizados
    {
        public int numero_expediente { set; get; }
        public string numero_operacion { set; get; }
        public string nombre_cliente { set; get; }
        public string identificacion { set; get; }
        public string detalles { set; get; }
        public string estado { set; get; }
    }
}
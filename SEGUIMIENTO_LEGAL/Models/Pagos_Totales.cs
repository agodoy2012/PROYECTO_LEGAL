using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEGUIMIENTO_LEGAL.Models
{
    public class Pagos_Totales
    {
        public int numero_expediente { set; get; }
        public string numero_operacion { set; get; }
        public string nombre_cliente { set; get; }
        public string identificacion { set; get; }
        public decimal monto_inicial { set; get; }
        public decimal monto_cargos { set; get; }
        public decimal monto_recibido { set; get; }
        public decimal saldo { set; get; }
        public int estado { set; get; }
    }
}
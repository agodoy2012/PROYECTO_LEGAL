using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEGUIMIENTO_LEGAL.Models
{
    public class Pagos_Cuenta_Unica
    {
        public string concepto_cuenta { set; get; }
        public string concepto_pago { set; get; }
        public decimal monto_inicial { set; get; }
        public decimal monto_cargos { set; get; }
        public decimal monto_recibido { set; get; }
        public decimal saldo { set; get; }
        public string fecha { set; get; }
        public string observaciones { set; get; }
    }
}
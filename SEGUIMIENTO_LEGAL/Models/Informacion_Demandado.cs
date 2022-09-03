using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEGUIMIENTO_LEGAL.Models
{
    public class Informacion_Demandado
    {
        public string nombre_cliente { set; get; }
        public string nombre_tipo_pago { set; get; }
        public string via_pago { set; get; }
        public decimal monto_cancelar { set; get; }
        public decimal monto_cargos { set; get; }
        public decimal monto_total { set; get; }
        public decimal monto_mensual { set; get; }
        public int tratos { set; get; }
        public int plazo { set; get; }
        public string fecha_ini_pago { set; get; }
        public string fecha_fin_pago { set; get; }
        public string nombre_proceso_judicial { set; get; }
        public string numero_proceso { set; get; }
        public string nombre_juzgado { set; get; }
        public string oficial { set; get; }
        public string detalles { set; get; }
        public string etapa { set; get; }
        public string subetapa { set; get; }
    }
}
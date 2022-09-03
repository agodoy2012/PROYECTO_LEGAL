using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEGUIMIENTO_LEGAL.Models
{
    public class Expediente_Judicial
    {
        public int id { set; get; }
        public string nit { set; get; }
        public string dpi { set; get; }
        public int id_cliente { set; get; }
        public string nombre_cliente { set; get; }
        public string numero_operacion { set; get; }
        public int etapa { set; get; }
        public int subetapa { set; get; }
        public int tipo_pago { set; get; }
        public string via_pago { set; get; }
        public decimal monto_mensual { set; get; }
        public decimal monto_cancelar { set; get; }
        public decimal monto_recibido { set; get; }
        public decimal monto_cargos { set; get; }
        public decimal monto_total { set; get; }
        public int tractos { set; get; }
        public int plazos { set; get; }
        public DateTime fecha_pago  { set; get; }
        public DateTime fecha_inicio_pago { set; get; }
        public DateTime fecha_fin_pago { set; get; }
        public int id_proceso_judicial { set; get; }
        public string nombre_juzgado { set; get; }
        public string numero_proceso { set; get; }
        public string oficial { set; get; }
        public string observaciones { set; get; }
        public string nombre_usuario { set; get; }
        public DateTime fecha_incluye { set; get; }
        public string archivos { set; get; }
        public int estado { set; get; }
        public int embargo_bancos { set; get; }
        public int embargo_salario { set; get; }
        public string embargo_otro { set; get; }
        public string sentencia { set; get; }
        public decimal monto_Demanda { set; get; }
    }
}
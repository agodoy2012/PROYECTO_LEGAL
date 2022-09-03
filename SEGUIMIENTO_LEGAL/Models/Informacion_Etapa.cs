using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEGUIMIENTO_LEGAL.Models
{
    public class Informacion_Etapa
    {

        public int id { set; get; }
        public int id_cliente { set; get; }
        public string nombre_cliente { set; get; }
        public string numero_operacion { set; get; }
        public string etapa { set; get; }
        public string subetapa { set; get; }
        public string tipo_pago { set; get; }
        public string via_pago { set; get; }
        public string monto_cancelar { set; get; }
        public string monto_cargos { set; get; }
        public string monto_pagare { set; get; }
        public int tractos { set; get; }
        public int plazos { set; get; }
        public int fecha_pago { set; get; }
        public DateTime fecha_fin_pago { set; get; }
        public string id_proceso_judicial { set; get; }
        public string nombre_juzgado { set; get; }
        public string numero_proceso { set; get; }
        public string oficial { set; get; }
        public string observaciones { set; get; }
        public string nombre_usuario { set; get; }
        public DateTime fecha_incluye { set; get; }
        public int estado { set; get; }
    }
}
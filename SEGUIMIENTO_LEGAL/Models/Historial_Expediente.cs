using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEGUIMIENTO_LEGAL.Models
{
    public class Historial_Expediente
    {

        public int id { set; get; }
        public int etapa { set; get; }
        public string nombre_etapa { set; get; }
        public int subetapa { set; get; }
        public string nombre_subetapa { set; get; }
        public string ruta_archivos { set; get; }
        public string detalles { set; get; }
        public string usuario { set; get; }
        public string fecha_registro { set; get; }

    }
}
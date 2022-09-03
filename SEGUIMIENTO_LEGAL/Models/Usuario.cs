using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEGUIMIENTO_LEGAL.Models
{
    public class Usuario
    {
        public int id { set; get; }
        public string nombre_completo { set; get; }
        public string usuario { set; get; }
        public int perfil { set; get; }
        public int admin { set; get; }
        public int pais { set; get; }
        public string usuario_incluye { set; get; }
    }
}
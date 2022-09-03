using Newtonsoft.Json;
using SEGUIMIENTO_LEGAL.Data;
using SEGUIMIENTO_LEGAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEGUIMIENTO_LEGAL.Ajax
{
    public partial class Conexion_Ajax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static List<Etapas> obtener_etapas()
        {
            Conexion cxn = new Conexion();
            int etapa = 0;
            List<Etapas> subetapas = cxn.mostrar_etapas(etapa);

            return subetapas;
        }

        [WebMethod(EnableSession = true)]
        public static List<Subetapas> obtener_reaciones_iniciales(int etapa)
        {
            Conexion cxn = new Conexion();

            List<Subetapas> subetapas = cxn.mostrar_subetapas_por_etapa(etapa);

            return subetapas;
        }

        [WebMethod(EnableSession = true)]
        public static bool verificar_duplicacion(string numero_operacion)
        {
            Conexion cxn = new Conexion();

            return cxn.verificar_duplicidad(numero_operacion);
        }

        [WebMethod(EnableSession = true)]
        public static List<string> obtener_informacion_cliente(string numero_operacion)
        {
            Conexion cxn = new Conexion();

            return cxn.obtener_informacion_cliente_summa(numero_operacion);
        }

        [WebMethod(EnableSession = true)]
        public static int ingresar_nuevo_caso(string numero_operacion, string nombre_cliente, string nit, string dpi, int correlativo, int etapa, int subetapa, string ruta_archivos)
        {
            Conexion cxn = new Conexion();

            Expediente_Judicial nuevo = new Expediente_Judicial();
            Historial_Expediente historial = new Historial_Expediente();
            Clientes cliente = new Clientes();
            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
            int rspta = -1;

            DateTime fecha_pago = DateTime.ParseExact("1900-01-01 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff",System.Globalization.CultureInfo.InvariantCulture);

            cliente.nombre_completo = nombre_cliente.ToUpper();
            cliente.dpi = dpi;
            cliente.nit = nit;
            cliente.usuario = usuario;//this user

            int id_cliente = cxn.registrar_cliente(nombre_cliente, nit, dpi, usuario);

            if (id_cliente > 0) {

                nuevo.id = correlativo;
                nuevo.id_cliente = id_cliente;
                nuevo.numero_operacion = numero_operacion;
                nuevo.etapa = etapa;
                nuevo.subetapa = subetapa;
                nuevo.tipo_pago = 1;
                nuevo.via_pago = "NO ESTABLECIDO";
                nuevo.monto_cancelar = 0;
                nuevo.tractos = 0;
                nuevo.plazos = 0;
                nuevo.fecha_pago = fecha_pago;
                nuevo.fecha_fin_pago = fecha_pago;
                nuevo.id_proceso_judicial = 1;
                nuevo.nombre_juzgado = "NO ESTABLECIDO";
                nuevo.numero_proceso = "0";
                nuevo.oficial = "NO ESTABLECIDO";
                nuevo.observaciones = "NO SE AGREGARON DETALLES";
                nuevo.nombre_usuario = usuario;

                int numero_expediente = cxn.registrar_nuevo_expediente(nuevo);

                historial.id = numero_expediente;
                historial.etapa = etapa;
                historial.subetapa = subetapa;
                historial.ruta_archivos = ruta_archivos;
                historial.detalles = "CASO INICIAL";
                historial.usuario = usuario;

                rspta = cxn.registrar_historial_expediente(historial);

            } 

            return rspta;
        }

        [WebMethod(EnableSession = true)]
        public static int ingresar_nuevo_caso_completo(int numero_expediente, string numero_operacion, string nombre_cliente, string nit, string dpi, int correlativo, int etapa, int subetapa,
            string archivos, int tipo_pago, string via_pago, decimal monto_mensual, decimal monto_cancelar, int tractos, int plazo, DateTime fecha_inicio_pago, DateTime fecha_fin_pago, int proceso,
            string nombre_juzgado, string numero_proceso, string oficial, string observaciones)
        {
            Conexion cxn = new Conexion();

            Expediente_Judicial nuevo = new Expediente_Judicial();
            Historial_Expediente historial = new Historial_Expediente();
            Clientes cliente = new Clientes();
            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
            int rspta = -1;
            int fecha_pago = 0;

            DateTime fecha_ini_pago = DateTime.ParseExact("1900-01-01 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
           // DateTime fecha_finali_pago = DateTime.ParseExact("1900-01-01 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);

            cliente.nombre_completo = nombre_cliente.ToUpper();
            cliente.dpi = dpi;
            cliente.nit = nit;
            cliente.usuario = usuario;//this user

            int id_cliente = cxn.registrar_cliente(nombre_cliente, nit, dpi, usuario);

            if (id_cliente > 0)
            {

                nuevo.id = correlativo;
                nuevo.id_cliente = id_cliente;
                nuevo.numero_operacion = numero_operacion;
                nuevo.etapa = etapa;
                nuevo.subetapa = subetapa;
                nuevo.tipo_pago = (tipo_pago != 0 ? tipo_pago : 1);
                nuevo.via_pago = (via_pago != "" ? via_pago : "NO ESTEBLECIDO");
                nuevo.monto_cancelar = 0;
                nuevo.tractos = 0;
                nuevo.plazos = 0;
                nuevo.fecha_pago = fecha_ini_pago;
                nuevo.fecha_fin_pago = fecha_inicio_pago;
                nuevo.id_proceso_judicial = 1;
                nuevo.nombre_juzgado = "NO ESTABLECIDO";
                nuevo.numero_proceso = "0";
                nuevo.oficial = "NO ESTABLECIDO";
                nuevo.observaciones = "NO SE AGREGARON DETALLES";
                nuevo.nombre_usuario = usuario;

                int numero_expediente1 = cxn.registrar_nuevo_expediente(nuevo);

                historial.id = numero_expediente1;
                historial.etapa = etapa;
                historial.subetapa = subetapa;
                historial.ruta_archivos = archivos;
                historial.detalles = "CASO INICIAL";
                historial.usuario = usuario;

                rspta = cxn.registrar_historial_expediente(historial);

            }

            return rspta;
        }

        [WebMethod(EnableSession = true)]
        public static int modificar_caso_arreglo_judicial(string numero_operacion, string nombre_cliente, string nit, string dpi, int correlativo, int etapa, int subetapa)
        {
            Conexion cxn = new Conexion();

            Expediente_Judicial nuevo = new Expediente_Judicial();
            Historial_Expediente historial = new Historial_Expediente();
            Clientes cliente = new Clientes();
            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
            int rspta = -1;

            DateTime fecha_pago = DateTime.ParseExact("1900-01-01 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);

            cliente.nombre_completo = nombre_cliente.ToUpper();
            cliente.dpi = dpi;
            cliente.nit = nit;
            cliente.usuario = usuario;//this user

            int id_cliente = cxn.registrar_cliente(nombre_cliente, nit, dpi, usuario);

            if (id_cliente > 0)
            {

                nuevo.id = correlativo;
                nuevo.id_cliente = id_cliente;
                nuevo.numero_operacion = numero_operacion;
                nuevo.etapa = etapa;
                nuevo.subetapa = subetapa;
                nuevo.tipo_pago = 1;
                nuevo.via_pago = "NO ESTABLECIDO";
                nuevo.monto_cancelar = 0;
                nuevo.tractos = 0;
                nuevo.plazos = 0;
                nuevo.fecha_pago = fecha_pago;
                nuevo.fecha_fin_pago = fecha_pago;
                nuevo.id_proceso_judicial = 1;
                nuevo.nombre_juzgado = "NO ESTABLECIDO";
                nuevo.numero_proceso = "0";
                nuevo.oficial = "NO ESTABLECIDO";
                nuevo.observaciones = "NO SE AGREGARON DETALLES";
                nuevo.nombre_usuario = usuario;

                int numero_expediente = cxn.registrar_nuevo_expediente(nuevo);

                historial.id = numero_expediente;
                historial.etapa = etapa;
                historial.subetapa = subetapa;
                historial.ruta_archivos = "";
                historial.detalles = "Caso Inicial";
                historial.usuario = usuario;

                rspta = cxn.registrar_historial_expediente(historial);

            }

            return rspta;
        }

        [WebMethod(EnableSession = true)]
        public static int actualizar_expediente(int numero_expediente, string numero_operacion, string nombre_cliente, string nit, string dpi, int correlativo, int etapa, int subetapa,
            string archivos, int tipo_pago, string via_pago, decimal monto_cancelar, int tractos, DateTime fecha_fin_pago, int proceso,
            string nombre_juzgado, string numero_proceso, string oficial, string observaciones)
        {
            Expediente_Judicial nuevo = new Expediente_Judicial();
            Historial_Expediente historial = new Historial_Expediente();
            Clientes cliente = new Clientes();
            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
            int respuesta = -1;

            return respuesta;
        }

        [WebMethod(EnableSession = true)]
        public static int actualizar_expediente_convenio(int numero_expediente, int etapa, int subetapa,
        string archivos, int tipo_pago, string via_pago, decimal monto_mensual, decimal monto_cancelar, int tractos, DateTime fecha_inicio_pago, DateTime fecha_fin_pago, string observaciones)

        {
            Expediente_Judicial nuevo = new Expediente_Judicial();
            Historial_Expediente historial = new Historial_Expediente();
            Conexion cxn = new Conexion();
            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
            int respuesta = -1;

            if (numero_expediente > 0)
            {

                nuevo.id = numero_expediente;
                nuevo.etapa = etapa;
                nuevo.subetapa = subetapa;
                nuevo.tipo_pago = tipo_pago;
                nuevo.via_pago = via_pago;
                nuevo.monto_mensual = monto_mensual;
                nuevo.monto_cancelar = monto_cancelar;
                nuevo.tractos = tractos;
                nuevo.fecha_inicio_pago = fecha_inicio_pago;
                nuevo.fecha_fin_pago = fecha_fin_pago;
                nuevo.nombre_usuario = usuario;

                cxn.actualizar_expediente_convenio(nuevo);

                historial.id = numero_expediente;
                historial.etapa = etapa;
                historial.subetapa = subetapa;
                historial.ruta_archivos = archivos;
                historial.detalles = observaciones;
                historial.usuario = usuario;

                respuesta = cxn.registrar_historial_expediente(historial);

            }

            return respuesta;
        }

        [WebMethod(EnableSession = true)]
        public static int actualizar_expediente_demanda(int numero_expediente, int etapa, int subetapa,string archivos, 
            int proceso, string nombre_juzgado, string numero_proceso, string oficial, string observaciones)
        {
            Expediente_Judicial nuevo = new Expediente_Judicial();
            Historial_Expediente historial = new Historial_Expediente();
            Conexion cxn = new Conexion();
            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
            int respuesta = -1;

            if (numero_expediente > 0)
            {
                nuevo.id = numero_expediente;
                nuevo.etapa = etapa;
                nuevo.subetapa = subetapa;
                nuevo.id_proceso_judicial = proceso;
                nuevo.nombre_juzgado = nombre_juzgado;
                nuevo.numero_proceso = numero_proceso;
                nuevo.oficial = oficial;
                nuevo.nombre_usuario = usuario;

                cxn.actualizar_expediente_demanda(nuevo);

                historial.id = numero_expediente;
                historial.etapa = etapa;
                historial.subetapa = subetapa;
                historial.ruta_archivos = archivos;
                historial.detalles = observaciones;
                historial.usuario = usuario;

                respuesta = cxn.registrar_historial_expediente(historial);
            }

            return respuesta;
        }

        [WebMethod(EnableSession = true)]
        public static int actualizar_expediente_etapas(int numero_expediente, int etapa, int subetapa, string archivos, string observaciones)
        {
            Expediente_Judicial nuevo = new Expediente_Judicial();
            Historial_Expediente historial = new Historial_Expediente();
            Conexion cxn = new Conexion();
            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
            int respuesta = -1;

            if (numero_expediente > 0)
            {
                nuevo.id = numero_expediente;
                nuevo.etapa = etapa;
                nuevo.subetapa = subetapa;
                nuevo.nombre_usuario = usuario;
                nuevo.observaciones = observaciones;

                cxn.actualizar_expediente_etapas(nuevo);

                historial.id = numero_expediente;
                historial.etapa = etapa;
                historial.subetapa = subetapa;
                historial.ruta_archivos = archivos;
                historial.detalles = observaciones;
                historial.usuario = usuario;

                respuesta = cxn.registrar_historial_expediente(historial);
            }

            return respuesta;
        }

        [WebMethod(EnableSession = true)]
        public static int actualizar_expediente_monto_recibido(int numero_expediente, int etapa, int subetapa, decimal monto, string archivos, string observaciones)
        {
            Expediente_Judicial nuevo = new Expediente_Judicial();
            Historial_Expediente historial = new Historial_Expediente();
            Conexion cxn = new Conexion();
            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
            int respuesta = -1;

            if (numero_expediente > 0)
            {
                nuevo.id = numero_expediente;
                nuevo.etapa = etapa;
                nuevo.subetapa = subetapa;
                nuevo.nombre_usuario = usuario;
                nuevo.observaciones = observaciones;
                nuevo.monto_cancelar = monto;

                cxn.actualizar_expediente_monto_recibido(nuevo);

                historial.id = numero_expediente;
                historial.etapa = etapa;
                historial.subetapa = subetapa;
                historial.ruta_archivos = archivos;
                historial.detalles = observaciones;
                historial.usuario = usuario;

                respuesta = cxn.registrar_historial_expediente(historial);
            }

            return respuesta;
        }

        [WebMethod(EnableSession = true)]
        public static bool validar_ingreso(string login, string pass)
        {
            Conexion cxn = new Conexion();

            bool acceso = false;

            Usuario usuario = cxn.validacion_acceso(login, pass);

            if (usuario.usuario != null && usuario.nombre_completo!=null)
            {
                HttpContext.Current.Session.Add("nombre_completo", usuario.nombre_completo);
                HttpContext.Current.Session.Add("usuario", usuario.usuario);
                HttpContext.Current.Session.Add("perfil", usuario.perfil);
                HttpContext.Current.Session.Add("administrador", usuario.admin);

                acceso = true;

            }

            return acceso;
        }

        [WebMethod(EnableSession = true)]
        public static bool registrar_usuario(string nombre_completo, string usuario, string clave, int perfil)
        {
            Conexion cxn = new Conexion();
            string user = HttpContext.Current.Session["nombre_completo"].ToString();

            return cxn.registar_usuarios(nombre_completo, usuario,clave, perfil, user);
        }//mostrar_registros_robot

        [WebMethod(EnableSession = true)]
        public static List<Registros_Robot> mostrar_registros_robot()
        {
            Conexion cxn = new Conexion();

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;

            return cxn.obtener_registros_robot();
        }

        [WebMethod(EnableSession = true)]
        public static List<Resumen_Expedientes_Legales> mostrar_expedientes_legales()
        {
            Conexion cxn = new Conexion();

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;

            return cxn.obtener_expedientes_legales();
        }

        [WebMethod(EnableSession = true)]
        public static List<Resumen_Expedientes_Legales> mostrar_expedientes_legales_filtro(string nombre, string numero_proceso, string numero_operacion, int etapa, int subetapa, int estado)
        {
            Conexion cxn = new Conexion();

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;

            return cxn.obtener_expedientes_legales_filtro(nombre, numero_proceso, numero_operacion, etapa, subetapa, estado);
        }

        [WebMethod(EnableSession = true)]
        public static List<Informacion_Etapa> mostrar_preparacion_demanda()
        {
            Conexion cxn = new Conexion();

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;

            return cxn.obtener_preparacion_demanda();
        }

        [WebMethod(EnableSession = true)]
        public static List<Informacion_Etapa> mostrar_areglo_extra_judicial()
        {
            Conexion cxn = new Conexion();

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;

            return cxn.obtener_arreglo_extra_judicial();
        }

        [WebMethod(EnableSession = true)]
        public static List<Informacion_Etapa> mostrar_presentacion_demanda()
        {
            Conexion cxn = new Conexion();

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;

            return cxn.obtener_arreglo_presentacion_demanda();
        }

        [WebMethod(EnableSession = true)]
        public static List<Informacion_Etapa> mostrar_arreglo_judicial()
        {
            Conexion cxn = new Conexion();

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;

            return cxn.obtener_arreglo_judicial();
        }

        [WebMethod(EnableSession = true)]
        public static List<Informacion_Etapa> mostrar_diligenciamiento()
        {
            Conexion cxn = new Conexion();

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;

            return cxn.obtener_diligenciamiento();
        }

        [WebMethod(EnableSession = true)]
        public static List<Tipo_Pago> mostrar_tipo_pago()
        {
            Conexion cxn = new Conexion();

            return cxn.obtener_tipo_pago();
        }

        [WebMethod(EnableSession = true)]
        public static List<Proceso_Judicial> mostrar_procesos_jodicial()
        {
            Conexion cxn = new Conexion();

            return cxn.obtener_proceso_judicial();
        }

        [WebMethod(EnableSession = true)]
        public static List<string> mostrar_juzgados()
        {
            Conexion cxn = new Conexion();

            return cxn.obtener_juzgados();
        }

        [WebMethod(EnableSession = true)]
        public static List<string> mostrar_oficiales()
        {
            Conexion cxn = new Conexion();

            return cxn.obtener_oficiales();
        }

        [WebMethod(EnableSession = true)]
        public static List<Historial_Expediente> mostrar_historial(int numero_expediente)
        {
            Conexion cxn = new Conexion();

            return cxn.obtener_historial(numero_expediente);
        }

        [WebMethod(EnableSession = true)]
        public static List<Historial_Expediente> mostrar_historial_etapa(int numero_expediente, int etapa)
        {
            Conexion cxn = new Conexion();

            return cxn.obtener_historial_etapa(numero_expediente, etapa);
        }

        [WebMethod(EnableSession = true)]
        public static int mostrar_numero_expediente(string numero_operacion)
        {
            Conexion cxn = new Conexion();

            return cxn.obtener_numero_expediente(numero_operacion);
        }

        //[WebMethod(EnableSession = true)]
        //public static int mostrar_cantidad_etapas(int etapa)
        //{
        //    Conexion cxn = new Conexion();

        //    return cxn.obtener_cantidad_etapas(etapa);
        //}

        [WebMethod(EnableSession = true)]
        public static Informacion_Demandado mostrar_informacion_demanda(string numero_operacion)
        {
            Conexion cxn = new Conexion();

            return cxn.obtener_informacion_demanda(numero_operacion);
        }

        [WebMethod(EnableSession = true)]
        public static bool cerrar_proceso(int numero_expediente, string juztificacion)
        {
            Conexion cxn = new Conexion();

            return cxn.cerrar_proceso(numero_expediente, juztificacion);
        }

        [WebMethod(EnableSession = true)]
        public static bool consultar_duplicion_expediente(int numero_expediente)
        {
            Conexion cxn = new Conexion();

            return cxn.consultar_duplicion_expediente(numero_expediente);
        }

        [WebMethod(EnableSession = true)]
        public static bool KeepActiveSession()
        {

            if (HttpContext.Current.Session["usuario"] != null && HttpContext.Current.Session["nombre_completo"] != null && HttpContext.Current.Session["perfil"] != null &&
                HttpContext.Current.Session["administrador"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [WebMethod(EnableSession = true)]
        public static List<Expedientes_Finalizados> mostrar_expedientes_finalizados()
        {
            Conexion cxn = new Conexion();

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;

            return cxn.obtener_expedientes_finalizados();
        }

        [WebMethod(EnableSession = true)]
        public static List<Pagos_Totales> mostrar_pagos_totales()
        {
            Conexion cxn = new Conexion();

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;

            return cxn.obtener_pagos_totales();
        }

        [WebMethod(EnableSession = true)]
        public static List<Pagos_Cuenta_Unica> mostrar_pagos(string numero_operacion)
        {
            Conexion cxn = new Conexion();

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;

            return cxn.obtener_pagos(numero_operacion);
        }

        [WebMethod(EnableSession = true)]
        public static bool realizar_pago(string numero_expediente, string numero_operacion, string concepto_pago, decimal monto, DateTime fecha_pago, string observaciones)
        {
            Conexion cxn = new Conexion();
            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();

            return cxn.registrar_pago(numero_expediente, numero_operacion, concepto_pago, monto, fecha_pago, observaciones, usuario);
        }

        [WebMethod(EnableSession = true)]
        public static decimal mostrar_monto_mensual(string numero_operacion)
        {
            Conexion cxn = new Conexion();
            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();

            return cxn.obtener_monto_pago_mensual(numero_operacion);
        }

        [WebMethod(EnableSession = true)]
        public static decimal mostrar_monto_intereses(string numero_operacion)
        {
            Conexion cxn = new Conexion();
            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();

            return cxn.obtener_monto_cargos(numero_operacion);
        }

        [WebMethod(EnableSession = true)]
        public static decimal mostrar_monto_total(string numero_operacion)
        {
            Conexion cxn = new Conexion();
            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();

            return cxn.obtener_monto_total_pagar(numero_operacion);
        }

        /*Anular*/
        [WebMethod(EnableSession = true)]
        public static bool anular_proceso(int numero_expediente, string justificacion_anulacion)
        {
            Conexion cxn = new Conexion();

            return cxn.anular_proceso(numero_expediente, justificacion_anulacion);
        }

    }
}
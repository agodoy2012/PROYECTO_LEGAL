using SEGUIMIENTO_LEGAL.Data;
using SEGUIMIENTO_LEGAL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;


namespace SEGUIMIENTO_LEGAL.Ajax
{
    /// <summary>
    /// Descripción breve de Servicio_Conexion
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Servicio_Conexion : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void registrar_nuevo_caso()
        {
            bool resultado = false;
            Escritor error = new Escritor();

            HttpContext contexto = HttpContext.Current;
            HttpFileCollection coleccionArchivos = contexto.Request.Files;
            Expediente_Judicial exp = new Expediente_Judicial();
            Conexion cxn = new Conexion();

            try
            {
                string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
                exp.numero_operacion = HttpContext.Current.Request.Form["numero_operacion"];
                exp.id = int.Parse(HttpContext.Current.Request.Form["num_expediente"]);
                exp.nit = HttpContext.Current.Request.Form["nit"];
                exp.dpi = HttpContext.Current.Request.Form["dpi"];
                exp.nombre_cliente = HttpContext.Current.Request.Form["nombre_deudor"];
                exp.etapa = 1;
                exp.subetapa = int.Parse(HttpContext.Current.Request.Form["subetapa"]);
                exp.observaciones = HttpContext.Current.Request.Form["detalles"];


                exp.archivos = almacenar_archivo(exp.id);

                int id_cliente = cxn.registrar_cliente(exp.nombre_cliente, exp.nit, exp.dpi, usuario);

                if (id_cliente > 0)
                {  /*Registrar caso*/
                    int numero_expediente = crear_expediente(exp, id_cliente, usuario);

                    if (numero_expediente > 0)
                    {   /*Registrar historial*/
                        int historial = registrar_historial(numero_expediente, exp, usuario);

                        if (historial > 0)
                        {
                            resultado = true;
                        }

                    }
                }

                contexto.Response.Write(resultado);
            }
            catch (Exception e)
            {

                error.logError(e);
            }

        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void modificar_caso_sin_campos_extra()
        {
            Escritor error = new Escritor();
            bool resultado = false;

            HttpContext contexto = HttpContext.Current;
            HttpFileCollection coleccionArchivos = contexto.Request.Files;
            Expediente_Judicial exp = new Expediente_Judicial();
            Conexion cxn = new Conexion();

            int numero_expediente = 0;

            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
            exp.etapa = int.Parse(HttpContext.Current.Request.Form["etapa"]);
            exp.subetapa = int.Parse(HttpContext.Current.Request.Form["subetapa"]);
            exp.id = Convert.ToInt32(HttpContext.Current.Request.Form["num_expediente"]);
            exp.observaciones = HttpContext.Current.Request.Form["detalles"];
            exp.nombre_usuario = usuario;
            //modificar_expediente(exp, usuario);

            numero_expediente = cxn.actualizar_expediente(exp);

            if (numero_expediente >= 0)
            {   /*Registrar historial*/
                exp.archivos = almacenar_archivo(exp.id);

                int historial = registrar_historial(numero_expediente, exp, usuario);

                resultado = true;
            }

            contexto.Response.Write(resultado);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void modificar_caso_convenio()
        {
            Escritor error = new Escritor();

            HttpContext contexto = HttpContext.Current;
            HttpFileCollection coleccionArchivos = contexto.Request.Files;
            Expediente_Judicial exp = new Expediente_Judicial();
            Conexion cxn = new Conexion();
            bool resultado = false;

            string monto_mensual = HttpContext.Current.Request.Form["monto_mensual"] != "" ? HttpContext.Current.Request.Form["monto_mensual"] : "0";
            string monto_cancelar = HttpContext.Current.Request.Form["monto_cancelar"] != "" ? HttpContext.Current.Request.Form["monto_cancelar"] : "0";

            monto_mensual = monto_mensual.Replace("Q", "");
            monto_mensual = monto_mensual.Replace(",", "");

            monto_cancelar = monto_cancelar.Replace("Q", "");
            monto_cancelar = monto_cancelar.Replace(",", "");

            int numero_expediente = 0;

            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
            exp.etapa = int.Parse(HttpContext.Current.Request.Form["etapa"]);
            exp.subetapa = int.Parse(HttpContext.Current.Request.Form["subetapa"]);
            exp.id = Convert.ToInt32(HttpContext.Current.Request.Form["num_expediente"]);
            exp.tipo_pago = Convert.ToInt32(HttpContext.Current.Request.Form["tipo_pago"]);
            exp.observaciones = HttpContext.Current.Request.Form["detalles"];
            exp.via_pago = "Arreglo ExtraJudicial";
            exp.monto_mensual = Convert.ToDecimal(monto_mensual);
            exp.monto_cancelar = Convert.ToDecimal(monto_cancelar);
            exp.tractos = Convert.ToInt32(HttpContext.Current.Request.Form["tractos"]);
            exp.fecha_inicio_pago = Convert.ToDateTime(HttpContext.Current.Request.Form["fecha_inicio_pago"]);
            exp.fecha_fin_pago = Convert.ToDateTime(HttpContext.Current.Request.Form["fecha_fin_pago"]);
            exp.nombre_usuario = usuario;

            //modificar_expediente(exp, usuario);

            numero_expediente = cxn.actualizar_expediente_convenio(exp);

            if (numero_expediente > 0)
            {   /*Registrar historial*/
                exp.archivos = almacenar_archivo(exp.id);
                int historial = registrar_historial(numero_expediente, exp, usuario);

                resultado = true;
            }

            contexto.Response.Write(resultado);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void modificar_caso_asignacion_juzgado()
        {
            Escritor error = new Escritor();

            HttpContext contexto = HttpContext.Current;
            HttpFileCollection coleccionArchivos = contexto.Request.Files;
            Expediente_Judicial exp = new Expediente_Judicial();
            Conexion cxn = new Conexion();
            bool resultado = false;

            int numero_expediente = 0;

            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
            exp.etapa = int.Parse(HttpContext.Current.Request.Form["etapa"]);
            exp.subetapa = int.Parse(HttpContext.Current.Request.Form["subetapa"]);
            exp.id = Convert.ToInt32(HttpContext.Current.Request.Form["num_expediente"]);
            exp.tipo_pago = Convert.ToInt32(HttpContext.Current.Request.Form["tipo_pago"]);
            exp.observaciones = HttpContext.Current.Request.Form["detalles"];
            exp.id_proceso_judicial = Convert.ToInt32(HttpContext.Current.Request.Form["proceso_judicial"]);
            exp.nombre_juzgado = HttpContext.Current.Request.Form["juzgado"];
            exp.numero_proceso = HttpContext.Current.Request.Form["numero_proceso"];
            exp.oficial = HttpContext.Current.Request.Form["oficial"];
            string monto_Demanda = HttpContext.Current.Request.Form["monto_demanda"] != "" ? HttpContext.Current.Request.Form["monto_demanda"] : "0";
            try
            {
                monto_Demanda = monto_Demanda.Replace("Q", "");
                monto_Demanda = monto_Demanda.Replace(",", "");
                decimal value = Decimal.Parse(monto_Demanda, NumberStyles.Currency, CultureInfo.InvariantCulture);
                exp.monto_Demanda = value;
            }
            catch (Exception ex)
            {

            }
            exp.nombre_usuario = usuario;
            numero_expediente = cxn.actualizar_expediente_demanda(exp);

            if (numero_expediente > 0)
            {   /*Registrar historial*/
                exp.archivos = almacenar_archivo(exp.id);
                int historial = registrar_historial(numero_expediente, exp, usuario);

                resultado = true;
            }

            contexto.Response.Write(resultado);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void modificar_caso_pago_recibido()
        {
            Escritor error = new Escritor();

            HttpContext contexto = HttpContext.Current;
            HttpFileCollection coleccionArchivos = contexto.Request.Files;
            Expediente_Judicial exp = new Expediente_Judicial();
            Conexion cxn = new Conexion();
            bool resultado = false;

            string monto_recibido = HttpContext.Current.Request.Form["monto_recibido"] != "" ? HttpContext.Current.Request.Form["monto_recibido"] : "0";
            monto_recibido = monto_recibido.Replace("Q", "");
            monto_recibido = monto_recibido.Replace(",", "");
            int numero_expediente = 0;


            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
            exp.etapa = int.Parse(HttpContext.Current.Request.Form["etapa"]);
            exp.subetapa = int.Parse(HttpContext.Current.Request.Form["subetapa"]);
            exp.id = Convert.ToInt32(HttpContext.Current.Request.Form["num_expediente"]);
            exp.observaciones = HttpContext.Current.Request.Form["detalles"];

            exp.monto_recibido = Convert.ToDecimal(monto_recibido);

            exp.fecha_pago = Convert.ToDateTime(HttpContext.Current.Request.Form["fecha_pago"]);
            exp.nombre_usuario = usuario;

            numero_expediente = cxn.actualizar_expediente_monto_recibido(exp);

            if (numero_expediente > 0)
            {   /*Registrar historial*/
                exp.archivos = almacenar_archivo(exp.id);
                int historial = registrar_historial(numero_expediente, exp, usuario);

                resultado = true;
            }

            contexto.Response.Write(resultado);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void modificar_caso_notificacion()
        {
            Escritor error = new Escritor();

            HttpContext contexto = HttpContext.Current;
            HttpFileCollection coleccionArchivos = contexto.Request.Files;
            Expediente_Judicial exp = new Expediente_Judicial();
            Conexion cxn = new Conexion();
            bool resultado = false;
            int numero_expediente = 0;


            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
            exp.etapa = int.Parse(HttpContext.Current.Request.Form["etapa"]);
            exp.subetapa = int.Parse(HttpContext.Current.Request.Form["subetapa"]);
            exp.id = Convert.ToInt32(HttpContext.Current.Request.Form["num_expediente"]);
            exp.observaciones = HttpContext.Current.Request.Form["detalles"];

            exp.embargo_bancos = Convert.ToInt32(HttpContext.Current.Request.Form["banco"]);
            exp.embargo_salario = Convert.ToInt32(HttpContext.Current.Request.Form["salario"]);
            exp.embargo_otro = HttpContext.Current.Request.Form["otro"];

            exp.nombre_usuario = usuario;
             
            numero_expediente = cxn.actualizar_expediente_notificacion(exp);

            if (numero_expediente > 0)
            {   /*Registrar historial*/
                exp.archivos = almacenar_archivo(exp.id);
                int historial = registrar_historial(numero_expediente, exp, usuario);

                resultado = true;
            }

            contexto.Response.Write(resultado);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void modificar_caso_desestimiento()
        {
            Escritor error = new Escritor();

            HttpContext contexto = HttpContext.Current;
            HttpFileCollection coleccionArchivos = contexto.Request.Files;
            Expediente_Judicial exp = new Expediente_Judicial();
            Conexion cxn = new Conexion();
            bool resultado = false;
            int numero_expediente = 0;
            string monto_recibido = HttpContext.Current.Request.Form["monto_recibido"] != "" ? HttpContext.Current.Request.Form["monto_recibido"] : "0";
            monto_recibido = monto_recibido.Replace("Q", "");
            monto_recibido = monto_recibido.Replace(",", "");

            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
            exp.etapa = int.Parse(HttpContext.Current.Request.Form["etapa"]);
            exp.subetapa = int.Parse(HttpContext.Current.Request.Form["subetapa"]);
            exp.id = Convert.ToInt32(HttpContext.Current.Request.Form["num_expediente"]);
            exp.observaciones = HttpContext.Current.Request.Form["detalles"];

            CultureInfo cultures = new CultureInfo("en-US");

            exp.monto_recibido = Convert.ToDecimal(monto_recibido, cultures);
            exp.fecha_pago = Convert.ToDateTime(HttpContext.Current.Request.Form["fecha_pago"]);
            exp.embargo_bancos = Convert.ToInt32(HttpContext.Current.Request.Form["banco"]);
            exp.embargo_salario = Convert.ToInt32(HttpContext.Current.Request.Form["salario"]);
            exp.embargo_otro = HttpContext.Current.Request.Form["otro"];

            exp.nombre_usuario = usuario;

            numero_expediente = cxn.actualizar_expediente_desestimiento(exp);

            if (numero_expediente > 0)
            {   /*Registrar historial*/
                exp.archivos = almacenar_archivo(exp.id);
                int historial = registrar_historial(numero_expediente, exp, usuario);

                resultado = true;
            }

            contexto.Response.Write(resultado);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void modificar_caso_sentencia()
        {
            Escritor error = new Escritor();

            HttpContext contexto = HttpContext.Current;
            HttpFileCollection coleccionArchivos = contexto.Request.Files;
            Expediente_Judicial exp = new Expediente_Judicial();
            Conexion cxn = new Conexion();
            bool resultado = false;
            int numero_expediente = 0;

            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
            exp.etapa = int.Parse(HttpContext.Current.Request.Form["etapa"]);
            exp.subetapa = int.Parse(HttpContext.Current.Request.Form["subetapa"]);
            exp.id = Convert.ToInt32(HttpContext.Current.Request.Form["num_expediente"]);
            exp.observaciones = HttpContext.Current.Request.Form["detalles"];

            string a = HttpContext.Current.Request.Form["opciones_sentencia"];

            exp.sentencia = HttpContext.Current.Request.Form["opciones_sentencia"];

            exp.nombre_usuario = usuario;

            numero_expediente = cxn.actualizar_expediente_sentencia(exp);

            if (numero_expediente > 0)
            {   /*Registrar historial*/
                exp.archivos = almacenar_archivo(exp.id);
                int historial = registrar_historial(numero_expediente, exp, usuario);

                resultado = true;
            }

            contexto.Response.Write(resultado);
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void modificar_caso(int etapa, int subetapa)
        {
            Escritor error = new Escritor();

            HttpContext contexto = HttpContext.Current;
            HttpFileCollection coleccionArchivos = contexto.Request.Files;
            Expediente_Judicial exp = new Expediente_Judicial();
            Conexion cxn = new Conexion();

            string rutaCarpeta = "";
            string rutas = "";

            string usuario = HttpContext.Current.Session["nombre_completo"].ToString();
            exp.etapa = int.Parse(HttpContext.Current.Request.Form["etapa"]);
            exp.subetapa = int.Parse(HttpContext.Current.Request.Form["subetapa"]);
            exp.numero_operacion = HttpContext.Current.Request.Form["numero_operacion"];
            exp.id = int.Parse(HttpContext.Current.Request.Form["num_expediente"]);
            exp.nit = HttpContext.Current.Request.Form["nit"];
            exp.dpi = HttpContext.Current.Request.Form["dpi"];
            exp.nombre_cliente = HttpContext.Current.Request.Form["nombre_deudor"];


            int id_cliente = cxn.registrar_cliente(exp.nombre_cliente, exp.nit, exp.dpi, usuario);

            if (id_cliente > 0)
            {

                exp.archivos = almacenar_archivo(exp.id);

                switch (etapa)
                {
                    case 1:
                        /*En la etapa 1 no se definen campos extra*/
                        if (subetapa == 1 || subetapa == 2 || subetapa == 3)
                        {
                            exp.observaciones = HttpContext.Current.Request.Form["detalles"];
                        }

                        break;
                    case 2:
                        if (subetapa == 4)
                        {
                            exp.observaciones = HttpContext.Current.Request.Form["detalles"];
                            /*Campos extra de subetapa 4*/
                            exp.tipo_pago = Convert.ToInt32(HttpContext.Current.Request.Form["tipo_pago"]);
                            exp.monto_mensual = Convert.ToDecimal(HttpContext.Current.Request.Form["monto_mensual"]);
                            exp.monto_cancelar = Convert.ToDecimal(HttpContext.Current.Request.Form["monto_cancelar"]);
                            exp.tractos = Convert.ToInt32(HttpContext.Current.Request.Form["tractos"]);
                            exp.fecha_inicio_pago = Convert.ToDateTime(HttpContext.Current.Request.Form["fecha_inicio_pago"]);
                            exp.fecha_fin_pago = Convert.ToDateTime(HttpContext.Current.Request.Form["fecha_fin_pago"]);
                        }
                        break;
                    case 3:
                        /*En la subetapa no se definen campos extra*/
                        if (subetapa == 5 || subetapa == 6 || subetapa == 7 || subetapa == 8 || subetapa == 9 || subetapa == 10)
                        {
                            exp.observaciones = HttpContext.Current.Request.Form["detalles"];
                        }
                        else if (subetapa == 6)
                        {
                            exp.observaciones = HttpContext.Current.Request.Form["detalles"];
                            /*Campos extra de subetapa 6*/
                            exp.id_proceso_judicial = Convert.ToInt32(HttpContext.Current.Request.Form["proceso_judicial"]);
                            exp.nombre_juzgado = HttpContext.Current.Request.Form["juzgado"];
                            exp.numero_proceso = HttpContext.Current.Request.Form["numero_proceso"];
                            exp.oficial = HttpContext.Current.Request.Form["oficial"];
                        }

                        break;
                    case 4:

                        if (subetapa == 13 || subetapa == 16)
                        {
                            exp.observaciones = HttpContext.Current.Request.Form["detalles"];

                        }
                        else if (subetapa == 15 || subetapa == 16)
                        {
                            exp.observaciones = HttpContext.Current.Request.Form["detalles"];
                            /*Campos extra de subetapa 15 y 16*/
                            exp.monto_recibido = Convert.ToDecimal(HttpContext.Current.Request.Form["monto_recibido"]);
                            exp.fecha_pago = Convert.ToDateTime(HttpContext.Current.Request.Form["fecha_pago"]);
                        }
                        break;
                    case 5:
                        /*En la subetapa no se definen campos extra*/
                        if (subetapa == 17 || subetapa == 18 || subetapa == 19 || subetapa == 20 || subetapa == 21 || subetapa == 22 || subetapa == 23)
                        {
                            exp.observaciones = HttpContext.Current.Request.Form["detalles"];
                        }

                        break;
                }//fin del switch

            }
            else
            {
                /*No se registro el cliente*/
            }


        }

        public int crear_expediente(Expediente_Judicial expediente, int id_cliente, string usuario)
        {
            int numero_expediente = 0;
            Conexion cxn = new Conexion();

            Expediente_Judicial nuevo = new Expediente_Judicial();
            DateTime fecha_pago = DateTime.ParseExact("1900-01-01 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);

            nuevo.id = expediente.id;
            nuevo.id_cliente = id_cliente;
            nuevo.numero_operacion = expediente.numero_operacion;
            nuevo.etapa = 1;
            nuevo.subetapa = expediente.subetapa;
            nuevo.tipo_pago = 1;
            nuevo.via_pago = "NO ESTABLECIDO";
            nuevo.monto_cancelar = 0;
            nuevo.tractos = 0;
            nuevo.fecha_pago = fecha_pago;
            nuevo.fecha_fin_pago = fecha_pago;
            nuevo.id_proceso_judicial = 1;
            nuevo.nombre_juzgado = "NO ESTABLECIDO";
            nuevo.numero_proceso = "";
            nuevo.oficial = "NO ESTABLECIDO";
            nuevo.observaciones = expediente.observaciones;
            nuevo.nombre_usuario = usuario;

            numero_expediente = cxn.registrar_nuevo_expediente(nuevo);

            return numero_expediente;
        }

        public int modificar_expediente(Expediente_Judicial expediente, string usuario)
        {
            int numero_expediente = 0;
            Conexion cxn = new Conexion();

            Expediente_Judicial nuevo = new Expediente_Judicial();
            DateTime fecha_pago = DateTime.ParseExact("1900-01-01 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);

            nuevo.id = expediente.id;
            nuevo.numero_operacion = expediente.numero_operacion;
            nuevo.etapa = expediente.etapa;
            nuevo.subetapa = expediente.subetapa;
            nuevo.tipo_pago = expediente.tipo_pago;
            nuevo.via_pago = (expediente.via_pago != null ? expediente.via_pago : "NO ESTABLECIDO");
            nuevo.monto_cancelar = expediente.monto_cancelar;
            nuevo.tractos = expediente.tractos;
            nuevo.fecha_pago = (expediente.fecha_pago != null ? expediente.fecha_pago : fecha_pago);
            nuevo.fecha_fin_pago = (expediente.fecha_fin_pago != null ? expediente.fecha_fin_pago : fecha_pago);
            nuevo.id_proceso_judicial = expediente.id_proceso_judicial;
            nuevo.nombre_juzgado = (expediente.nombre_juzgado != null ? expediente.nombre_juzgado : "NO ESTABLECIDO");
            nuevo.numero_proceso = (expediente.numero_proceso != null ? expediente.numero_proceso : "");
            nuevo.oficial = (expediente.oficial != null ? expediente.oficial : "NO ESTABLECIDO");
            nuevo.observaciones = expediente.observaciones;
            nuevo.nombre_usuario = usuario;
            nuevo.embargo_bancos = expediente.embargo_bancos;
            nuevo.embargo_salario = expediente.embargo_salario;
            nuevo.embargo_otro = (expediente.embargo_otro != null ? expediente.embargo_otro : "");
            nuevo.sentencia = (expediente.sentencia != null ? expediente.sentencia : "SIN SENTENCIA");

            numero_expediente = cxn.registrar_nuevo_expediente(nuevo);

            return numero_expediente;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string almacenar_archivo(int id)
        {
            HttpContext contexto = HttpContext.Current;
            HttpFileCollection coleccionArchivos = contexto.Request.Files;
            var httpPostedFile = HttpContext.Current.Request.Files["Uploaded_Document"];
            string rutaCarpeta = "";

            string ruta = string.Empty;

            if (httpPostedFile != null)
            {
                rutaCarpeta = HttpContext.Current.Server.MapPath("~/Archivos/") + id + "/";

                if (!System.IO.Directory.Exists(rutaCarpeta))
                {
                    System.IO.Directory.CreateDirectory(rutaCarpeta);
                }

                for (int i = 0; i < coleccionArchivos.Count; i++)
                {
                    HttpPostedFile file = coleccionArchivos[i];

                    string fname = Path.Combine(rutaCarpeta, file.FileName); ;

                    if (file.FileName != "")
                    {
                        ruta += "Archivos/" + id + "/" + file.FileName + ";";

                        file.SaveAs(fname);
                    }
                    else
                    {
                        ruta = "";
                    }
                }
            }

            return ruta;
        }

        public int registrar_historial(int numero_expediente, Expediente_Judicial expediente, string usuario)
        {
            Conexion cxn = new Conexion();
            int resultado = 0;

            if (numero_expediente > 0)
            {
                Historial_Expediente historial = new Historial_Expediente();
                historial.id = expediente.id;
                historial.etapa = expediente.etapa;
                historial.subetapa = expediente.subetapa;
                historial.ruta_archivos = expediente.archivos;
                historial.detalles = expediente.observaciones;
                historial.usuario = usuario;
                resultado = cxn.registrar_historial_expediente(historial);
            }
            return resultado;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void caso_en_vista()
        {
            Conexion cxn = new Conexion();
            HttpContext contexto = HttpContext.Current;
            int num_expediente = Convert.ToInt32(HttpContext.Current.Request.Form["num_expediente"]);
            int visto = Convert.ToInt32(HttpContext.Current.Request.Form["visto"]);
            int resultado;

            resultado = cxn.caso_en_vista(num_expediente, visto);


            contexto.Response.Write(resultado);


        }




    }
}

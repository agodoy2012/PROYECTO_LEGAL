using SEGUIMIENTO_LEGAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SEGUIMIENTO_LEGAL.Data
{
    public class Conexion
    {
        private SqlConnection cxn;
        private SqlConnection cxn_summa;
        private Escritor error; 

        public Conexion()
        {
            cxn = new SqlConnection(ConfigurationManager.AppSettings["string_connection"].ToString());

            error = new Escritor();
        }

        public void conectarSumma()
        {
            cxn_summa = new SqlConnection(@"Data Source=192.168.2.30;Initial Catalog=MONEYBACK;User ID=sa;Password=Sqlserver2008");
        }

        public List<Etapas> mostrar_etapas(int id_etapa)
        {
            List<Etapas> etapas = new List<Etapas>();
            Etapas etapa = null;

            try
            {
                if (id_etapa == 0)
                {
                    //SqlCommand cmd = new SqlCommand("SELECT ID_ETAPA, NOMBRE_ETAPA, DETALLES FROM SL_ETAPAS WITH(NOLOCK) WHERE ESTADO = 1 ORDER BY ID_ETAPA ASC;", cxn);
                    SqlCommand cmd = new SqlCommand(@"SP_SL_MOSTRAR_ETAPAS", cxn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cxn.Open();

                    SqlDataReader lector = cmd.ExecuteReader();
                    
                    while (lector.Read())
                    {
                        etapa = new Etapas();
                        etapa.id_etapa = lector.GetInt32(0);
                        etapa.nombre = lector.GetString(1);
                        etapa.detalles = (lector.GetValue(2) != DBNull.Value ? lector.GetString(2) : "");
                        etapas.Add(etapa);
                    }
                }
                else
                {
                    SqlCommand cmd = new SqlCommand(@"SP_SL_MOSTRAR_ETAPAS_POR_ID", cxn);
                    cmd.Parameters.AddWithValue("@ID_ETAPA", id_etapa);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cxn.Open();

                    SqlDataReader lector = cmd.ExecuteReader();

                    while (lector.Read())
                    {
                        etapa = new Etapas();
                        etapa.id_etapa = lector.GetInt32(0);
                        etapa.nombre = lector.GetString(1);
                        etapa.detalles = (lector.GetValue(2) != DBNull.Value ? lector.GetString(2) : "");
                        etapas.Add(etapa);
                    }
                }
            }
            catch (Exception e)
            {
                error.logError(e);
            }
            finally
            {
                cxn.Close();
            }

            return etapas;
        }

        public List<Subetapas> mostrar_subetapas(int id_subetapa)
        {
            List<Subetapas> subetapas = new List<Subetapas>();
            Subetapas subetapa = null;

            try
            {
                if (id_subetapa != 0)
                {
                    //SqlCommand cmd = new SqlCommand("SELECT ID_SUBETAPA, NOMBRE_SUBETAPA, DETALLES FROM SL_SUBETAPAS WITH(NOLOCK) WHERE ESTADO = 1;", cxn);
                    SqlCommand cmd = new SqlCommand(@"SP_SL_MOSTRAR_SUB_ETAPAS", cxn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cxn.Open();

                    SqlDataReader lector = cmd.ExecuteReader();

                    while (lector.Read())
                    {
                        subetapa = new Subetapas();
                        subetapa.id_subetapa = lector.GetInt32(0);
                        subetapa.nombre = lector.GetString(1);
                        subetapa.detalles = (lector.GetValue(2) != DBNull.Value ? lector.GetString(2) : "");
                        subetapas.Add(subetapa);
                    }
                }
                else
                {
                    //SqlCommand cmd = new SqlCommand("SELECT ID_SUBETAPA, NOMBRE_SUBETAPA, DETALLES FROM SL_SUBETAPAS WITH(NOLOCK) WHERE ESTADO = 1 AND ID_SUBETAPA = @ID_SUBETAPA;", cxn);
                    SqlCommand cmd = new SqlCommand(@"SP_SL_MOSTRAR_SUB_ETAPAS_POR_ID", cxn);
                    cmd.Parameters.AddWithValue("@ID_SUBETAPA", id_subetapa);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cxn.Open();

                    SqlDataReader lector = cmd.ExecuteReader();

                    while (lector.Read())
                    {
                        subetapa = new Subetapas();
                        subetapa.id_subetapa = lector.GetInt32(0);
                        subetapa.nombre = lector.GetString(1);
                        subetapa.detalles = lector.GetString(2);
                        subetapas.Add(subetapa);
                    }
                }
            }
            catch (Exception e)
            {
                error.logError(e);
            }
            finally
            {
                cxn.Close();
            }

            return subetapas;
        }

        public bool consultar_duplicion_expediente(int numero_expediente)
        {
            bool resultado = false;

            try
            {
                //SqlCommand cmd = new SqlCommand(@"SELECT ID_EXPEDIENTE_LEGAL FROM SL_EXPEDIENTE_LEGAL WHERE ID_EXPEDIENTE_LEGAL = @ID;", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_CONSULTAR_DUPLICACION_EXP", cxn);
                cmd.Parameters.AddWithValue("@ID", numero_expediente);
                cmd.CommandType = CommandType.StoredProcedure;

                cxn.Open();
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    resultado = true;
                }

            }
            catch (Exception ex)
            {
                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }
            return resultado;
        }


        public int registrar_nuevo_expediente(Expediente_Judicial expediente)
        {
            int rstp = 0;

            try
            {
                //SqlCommand cmd = new SqlCommand(@"INSERT INTO SL_EXPEDIENTE_LEGAL(ID_EXPEDIENTE_LEGAL,ID_CLIENTE,NUMERO_OPERACION,ID_ETAPA,ID_SUBETAPA
                //, ID_TIPO_PAGO, VIA_DE_PAGO, MONTO_CANCELAR,TRACTOS, FECHA_PAGO,ID_PROCESO_JUDICIAL, NOMBRE_JUZGADO
                //, NUMERO_PROCESO, OFICIAL_A_CARGO, DETALLES, USUARIO_INCLUYE, FECHA_INCLUYE, ESTADO) VALUES
                // (@ID,@ID_CLIENTE,@NUMERO_OPERACION,@ETAPA,@SUBETAPA,@ID_TIPO_PAGO,@VIA_PAGO,@MONTO_CANCELAR,@TRACTOS, @FECHA_PAGO,
                //@ID_PROCESO_JUDICIAL,@NOMBRE_JUZGADO,@NUMERO_PROCESO,@OFICIAL_A_CARGO,@DETALLES,@USUARIO_INCLUYE,
                //GETDATE(), 1);", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_REGISTRAR_NUEVO_EXPEDIENTE", cxn);
                cmd.Parameters.AddWithValue("@ID", expediente.id);
                cmd.Parameters.AddWithValue("@ID_CLIENTE", expediente.id_cliente);
                cmd.Parameters.AddWithValue("@NUMERO_OPERACION", expediente.numero_operacion);
                cmd.Parameters.AddWithValue("@ETAPA", expediente.etapa);
                cmd.Parameters.AddWithValue("@SUBETAPA", expediente.subetapa);
                cmd.Parameters.AddWithValue("@ID_TIPO_PAGO", expediente.tipo_pago);
                cmd.Parameters.AddWithValue("@VIA_PAGO", expediente.via_pago);
                cmd.Parameters.AddWithValue("@MONTO_CANCELAR", expediente.monto_cancelar);
                cmd.Parameters.AddWithValue("@TRACTOS", expediente.tractos);
                cmd.Parameters.AddWithValue("@FECHA_PAGO", expediente.fecha_pago);
                cmd.Parameters.AddWithValue("@ID_PROCESO_JUDICIAL", expediente.id_proceso_judicial);
                cmd.Parameters.AddWithValue("@NOMBRE_JUZGADO", expediente.nombre_juzgado);
                cmd.Parameters.AddWithValue("@NUMERO_PROCESO", expediente.numero_proceso);
                cmd.Parameters.AddWithValue("@OFICIAL_A_CARGO", expediente.oficial);
                cmd.Parameters.AddWithValue("@DETALLES", expediente.observaciones);
                cmd.Parameters.AddWithValue("@USUARIO_INCLUYE", expediente.nombre_usuario);
                cmd.Parameters.AddWithValue("@FECHA", "12/02/2022");
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                cmd.ExecuteNonQuery();

                rstp = expediente.id;

            }
            catch (Exception ex)
            {

                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }

            return rstp;
        }

        public int actualizar_expediente(Expediente_Judicial expediente)
        {
            int rstp = 0;

            try
            {
                //    SqlCommand cmd = new SqlCommand(@"UPDATE SL_EXPEDIENTE_LEGAL SET ID_ETAPA = @ETAPA,ID_SUBETAPA = @SUBETAPA
                //,USUARIO_MODIFICA = @USUARIO_MODIFICA, FECHA_MODIFICA = GETDATE() WHERE ID_EXPEDIENTE_LEGAL = @ID_EXPEDIENTE_LEGAL; ", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_ACTUALIZAR_EXPEDIENTE", cxn);
                cmd.Parameters.AddWithValue("@ID_EXPEDIENTE_LEGAL", expediente.id);
                cmd.Parameters.AddWithValue("@ETAPA", expediente.etapa);
                cmd.Parameters.AddWithValue("@SUBETAPA", expediente.subetapa);
                cmd.Parameters.AddWithValue("@USUARIO_MODIFICA", expediente.nombre_usuario);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                cmd.ExecuteNonQuery();
                rstp = expediente.id;

            }
            catch (Exception ex)
            {
                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }
            return rstp;
        }

        public int actualizar_expediente_convenio(Expediente_Judicial expediente)
        {
            int rstp = 0;

            try
            {
                //    SqlCommand cmd = new SqlCommand(@"UPDATE SL_EXPEDIENTE_LEGAL SET ID_ETAPA = @ETAPA,ID_SUBETAPA = @SUBETAPA,ID_TIPO_PAGO = @TIPO_PAGO,VIA_DE_PAGO = @VIA_PAGO, MONTO_MENSUAL = @MONTO_MENSUAL
                //, MONTO_CANCELAR = @MONTO_CANCELAR, TRACTOS = @TRACTOS, FECHA_INICIO_PAGO = @FECHA_INICIO_PAGO,FECHA_FINALIZACION_PAGO = @FECHA_FINALIZACION_PAGO
                //, USUARIO_MODIFICA = @USUARIO_MODIFICA, FECHA_MODIFICA = GETDATE() WHERE ID_EXPEDIENTE_LEGAL = @ID_EXPEDIENTE_LEGAL; ", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_ACTUALIZAR_EXPEDIENTE_CONVENIO", cxn);
                cmd.Parameters.AddWithValue("@ID_EXPEDIENTE_LEGAL", expediente.id);
                cmd.Parameters.AddWithValue("@ETAPA", expediente.etapa);
                cmd.Parameters.AddWithValue("@SUBETAPA", expediente.subetapa);
                cmd.Parameters.AddWithValue("@TIPO_PAGO", expediente.tipo_pago);
                cmd.Parameters.AddWithValue("@VIA_PAGO", expediente.via_pago);
                cmd.Parameters.AddWithValue("@MONTO_MENSUAL", expediente.monto_mensual);
                cmd.Parameters.AddWithValue("@MONTO_CANCELAR", expediente.monto_cancelar);
                cmd.Parameters.AddWithValue("@TRACTOS", expediente.tractos);
                cmd.Parameters.AddWithValue("@FECHA_INICIO_PAGO", expediente.fecha_inicio_pago);
                cmd.Parameters.AddWithValue("@FECHA_FINALIZACION_PAGO", expediente.fecha_fin_pago);
                cmd.Parameters.AddWithValue("@USUARIO_MODIFICA", expediente.nombre_usuario);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                cmd.ExecuteNonQuery();

                rstp = expediente.id;

            }
            catch (Exception ex)
            {

                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }

            return rstp;
        }

        public int actualizar_expediente_demanda(Expediente_Judicial expediente)
        {
            int rstp = 0;
            try
            {
                String Consulta = "";
                if (expediente.monto_Demanda > 0)
                {
                    Consulta= ",MONTO_DEMANDA=@MONTO_DEMANDA ";
                }
                SqlCommand cmd = new SqlCommand(@"UPDATE SL_EXPEDIENTE_LEGAL SET ID_ETAPA = @ETAPA,ID_SUBETAPA = @SUBETAPA, 
                ID_PROCESO_JUDICIAL = @ID_PROCESO_JUDICIAL, NOMBRE_JUZGADO = @NOMBRE_JUZGADO, NUMERO_PROCESO = @NUMERO_PROCESO, 
                OFICIAL_A_CARGO = @OFICIAL_A_CARGO, USUARIO_MODIFICA = @USUARIO_MODIFICA, FECHA_MODIFICA = GETDATE() " + Consulta + " WHERE ID_EXPEDIENTE_LEGAL = @ID_EXPEDIENTE_LEGAL; ", cxn);
                cxn.Open();
                cmd.Parameters.AddWithValue("@ID_EXPEDIENTE_LEGAL", expediente.id);
                cmd.Parameters.AddWithValue("@ETAPA", expediente.etapa);
                cmd.Parameters.AddWithValue("@SUBETAPA", expediente.subetapa);
                cmd.Parameters.AddWithValue("@ID_PROCESO_JUDICIAL", expediente.id_proceso_judicial);
                cmd.Parameters.AddWithValue("@NOMBRE_JUZGADO", expediente.nombre_juzgado);
                cmd.Parameters.AddWithValue("@NUMERO_PROCESO", expediente.numero_proceso);
                cmd.Parameters.AddWithValue("@OFICIAL_A_CARGO", expediente.oficial);
                cmd.Parameters.AddWithValue("@USUARIO_MODIFICA", expediente.nombre_usuario);
                cmd.Parameters.AddWithValue("@MONTO_DEMANDA", expediente.monto_Demanda);
                cmd.ExecuteNonQuery();

            rstp = expediente.id;

        }
            catch (Exception ex)
            {

                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }

            return rstp;
        }

        public string actualizar_expediente_etapas(Expediente_Judicial expediente)
        {
            string rstp = "";
            try
            {
                //SqlCommand cmd = new SqlCommand(@"UPDATE SL_EXPEDIENTE_LEGAL SET ID_ETAPA = @ETAPA,
                //        ID_SUBETAPA = @SUBETAPA,USUARIO_MODIFICA = @USUARIO_MODIFICA, FECHA_MODIFICA = GETDATE() 
                //        WHERE ID_EXPEDIENTE_LEGAL = @ID_EXPEDIENTE_LEGAL; ", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_ACTUALIZAR_EXPEDIENTE_ETAPAS", cxn);
                cmd.Parameters.AddWithValue("@ID_EXPEDIENTE_LEGAL", expediente.id);
                cmd.Parameters.AddWithValue("@ETAPA", expediente.etapa);
                cmd.Parameters.AddWithValue("@SUBETAPA", expediente.subetapa);
                cmd.Parameters.AddWithValue("@USUARIO_MODIFICA", expediente.nombre_usuario);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();
                cmd.ExecuteNonQuery();
                rstp = "" + expediente.id;
            }
            catch (Exception ex)
            {
                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }
            return rstp;
        }

        public int actualizar_expediente_monto_recibido(Expediente_Judicial expediente)
        {
            int rstp = 0;
            try
            {
                //SqlCommand cmd = new SqlCommand(@"UPDATE SL_EXPEDIENTE_LEGAL SET ID_ETAPA = @ETAPA,
                //        ID_SUBETAPA = @SUBETAPA, MONTO_CANCELAR = @MONTO_CANCELAR, FECHA_PAGO = @FECHA_PAGO, USUARIO_MODIFICA = @USUARIO_MODIFICA, FECHA_MODIFICA = GETDATE() 
                //        WHERE ID_EXPEDIENTE_LEGAL = @ID_EXPEDIENTE_LEGAL; ", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_ACTUALIZAR_EXPEDIENTE_MONTO_RECIBIDO", cxn);
                cmd.Parameters.AddWithValue("@ID_EXPEDIENTE_LEGAL", expediente.id);
                cmd.Parameters.AddWithValue("@ETAPA", expediente.etapa);
                cmd.Parameters.AddWithValue("@SUBETAPA", expediente.subetapa);
                cmd.Parameters.AddWithValue("@FECHA_PAGO", expediente.fecha_pago);
                cmd.Parameters.AddWithValue("@MONTO_CANCELAR", expediente.monto_cancelar);
                cmd.Parameters.AddWithValue("@USUARIO_MODIFICA", expediente.nombre_usuario);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();
                cmd.ExecuteNonQuery();
                rstp = expediente.id;
            }
            catch (Exception ex)
            {
                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }
            return rstp;
        }

        public int actualizar_expediente_notificacion(Expediente_Judicial expediente)
        {
            int rstp = 0;
            try
            {
                //SqlCommand cmd = new SqlCommand(@"UPDATE SL_EXPEDIENTE_LEGAL SET ID_ETAPA = @ETAPA,
                //        ID_SUBETAPA = @SUBETAPA, EMBARGO_BANCO = @EMBARGO_BANCO, EMBARGO_SALARIO = @EMBARGO_SALARIO,EMBARGO_OTRO = @EMBARGO_OTRO 
                //        WHERE ID_EXPEDIENTE_LEGAL = @ID_EXPEDIENTE_LEGAL; ", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_ACTUALIZAR_EXPEDIENTE_NOTIFICACION", cxn);
                cmd.Parameters.AddWithValue("@ID_EXPEDIENTE_LEGAL", expediente.id);
                cmd.Parameters.AddWithValue("@ETAPA", expediente.etapa);
                cmd.Parameters.AddWithValue("@SUBETAPA", expediente.subetapa);
                cmd.Parameters.AddWithValue("@EMBARGO_BANCO", expediente.embargo_bancos);
                cmd.Parameters.AddWithValue("@EMBARGO_SALARIO", expediente.embargo_salario);
                cmd.Parameters.AddWithValue("@EMBARGO_OTRO", expediente.embargo_otro);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();
                cmd.ExecuteNonQuery();
                rstp = expediente.id;
            }
            catch (Exception ex)
            {
                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }
            return rstp;
        }

        public int actualizar_expediente_desestimiento(Expediente_Judicial expediente)
        {
            int rstp = 0;
            try
            {
                //SqlCommand cmd = new SqlCommand(@"UPDATE SL_EXPEDIENTE_LEGAL SET ID_ETAPA = @ETAPA,
                //ID_SUBETAPA = @SUBETAPA, MONTO_CANCELAR = @MONTO_CANCELAR, FECHA_PAGO = @FECHA_PAGO, USUARIO_MODIFICA = @USUARIO_MODIFICA, FECHA_MODIFICA = GETDATE() 
                //WHERE ID_EXPEDIENTE_LEGAL = @ID_EXPEDIENTE_LEGAL; ", cxn);


                //SqlCommand cmd = new SqlCommand(@"UPDATE SL_EXPEDIENTE_LEGAL SET ID_ETAPA = @ETAPA,
                //        ID_SUBETAPA = @SUBETAPA, MONTO_CANCELAR = @MONTO_CANCELAR, FECHA_PAGO = @FECHA_PAGO, EMBARGO_BANCO = @EMBARGO_BANCO, EMBARGO_SALARIO = @EMBARGO_SALARIO,EMBARGO_OTRO = @EMBARGO_OTRO 
                //        WHERE ID_EXPEDIENTE_LEGAL = @ID_EXPEDIENTE_LEGAL; ", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_ACTUALIZAR_EXPEDIENTE_DESESTIMIENTO", cxn);
                cmd.Parameters.AddWithValue("@ID_EXPEDIENTE_LEGAL", expediente.id);
                cmd.Parameters.AddWithValue("@ETAPA", expediente.etapa);
                cmd.Parameters.AddWithValue("@SUBETAPA", expediente.subetapa);
                cmd.Parameters.AddWithValue("@EMBARGO_BANCO", expediente.embargo_bancos);
                cmd.Parameters.AddWithValue("@EMBARGO_SALARIO", expediente.embargo_salario);
                cmd.Parameters.AddWithValue("@EMBARGO_OTRO", expediente.embargo_otro);
                cmd.Parameters.AddWithValue("@USUARIO_MODIFICA", expediente.nombre_usuario);
                cmd.Parameters.AddWithValue("@FECHA_PAGO", expediente.fecha_pago);
                cmd.Parameters.AddWithValue("@MONTO_CANCELAR", expediente.monto_recibido);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();
                cmd.ExecuteNonQuery();
                rstp = expediente.id;
            }
            catch (Exception ex)
            {
                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }
            return rstp;
        }

        public int actualizar_expediente_sentencia(Expediente_Judicial expediente)
        {
            int rstp = 0;
            try
            {
                //SqlCommand cmd = new SqlCommand(@"UPDATE SL_EXPEDIENTE_LEGAL SET ID_ETAPA = @ETAPA,
                //        ID_SUBETAPA = @SUBETAPA, SENTENCIA = @SENTENCIA
                //        WHERE ID_EXPEDIENTE_LEGAL = @ID_EXPEDIENTE_LEGAL;", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_ACTUALIZAR_EXPEDIENTE_DESESTIMIENTO", cxn);
                cmd.Parameters.AddWithValue("@ID_EXPEDIENTE_LEGAL", expediente.id);
                cmd.Parameters.AddWithValue("@ETAPA", expediente.etapa);
                cmd.Parameters.AddWithValue("@SUBETAPA", expediente.subetapa);
                cmd.Parameters.AddWithValue("@SENTENCIA", expediente.sentencia);
                cmd.Parameters.AddWithValue("@USUARIO_MODIFICA", expediente.nombre_usuario);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();
                cmd.ExecuteNonQuery();
                rstp = expediente.id;
            }
            catch (Exception ex)
            {
                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }
            return rstp;
        }

        public int registrar_historial_expediente(Historial_Expediente historial)
        {
            int rstp = 0;
            try
            {
                //SqlCommand cmd = new SqlCommand(@"INSERT INTO SL_HISTORIAL_EXPEDIENTE_LEGAL(ID_EXPEDIENTE_LEGAL,ID_ETAPA,ID_SUBETAPA,ARCHIVO_ADJUNTO
                //        ,DETALLES,USUARIO_INCLUYE,FECHA_INCLUYE,ESTADO) VALUES
                //        (@ID_EXPEDIENTE_LEGAL, @ID_ETAPA, @ID_SUBETAPA, @ARCHIVO_ADJUNTO, @DETALLES, @USUARIO_INCLUYE, GETDATE(), 1);SELECT SCOPE_IDENTITY();", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_REGISTRAL_HISTORIAL_EXPEDIENTE", cxn);
                cmd.Parameters.AddWithValue("@ID_EXPEDIENTE_LEGAL", historial.id);
                cmd.Parameters.AddWithValue("@ID_ETAPA", historial.etapa);
                cmd.Parameters.AddWithValue("@ID_SUBETAPA", historial.subetapa);
                cmd.Parameters.AddWithValue("@ARCHIVO_ADJUNTO", historial.ruta_archivos);
                cmd.Parameters.AddWithValue("@DETALLES", historial.detalles);
                cmd.Parameters.AddWithValue("@USUARIO_INCLUYE", historial.usuario);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();
                rstp = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }
            return rstp;
        }

        public List<Subetapas> mostrar_subetapas_por_etapa(int id_etapa)
        {
            List<Subetapas> subetapas = new List<Subetapas>();
            Subetapas subetapa = null;
            try
            {
                //SqlCommand cmd = new SqlCommand("SELECT ID_SUBETAPA, NOMBRE_SUBETAPA, DETALLES FROM SL_SUBETAPAS WHERE ESTADO = 1 AND ID_ETAPA = @ID_ETAPA;", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_MOSTRAR_SUBETAPAS_POR_ETAPA", cxn);
                cmd.Parameters.AddWithValue("@ID_ETAPA", id_etapa);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    subetapa = new Subetapas();
                    subetapa.id_subetapa = lector.GetInt32(0);
                    subetapa.nombre = lector.GetString(1);
                    subetapa.detalles = (lector.GetValue(2) != DBNull.Value ? lector.GetString(2) : "");
                    subetapas.Add(subetapa);
                }
            }
            catch (Exception ex)
            {
                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }
            return subetapas;
        }

        public bool registar_usuarios(string nombre_completo, string usuario, string clave, int perfil, string usuario_incluye)
        {
            bool rstp = false;
            try
            {
                //SqlCommand cmd = new SqlCommand(@"INSERT INTO SL_USUARIOS(NOMBRE_COMPLETO,USUARIO,CLAVE,PERFIL,USUARIO_INCLUYE,FECHA_INCLUYE,ESTADO) VALUES
                //        (@NOMBRE_COMPLETO, @USUARIO, @CLAVE, @PERFIL, @USUARIO_INCLUYE, GETDATE(), 1)", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_REGISTRAR_USUARIOS", cxn);
                cmd.Parameters.AddWithValue("@NOMBRE_COMPLETO", nombre_completo);
                cmd.Parameters.AddWithValue("@USUARIO", usuario);
                cmd.Parameters.AddWithValue("@CLAVE", clave);
                cmd.Parameters.AddWithValue("@PERFIL", perfil);
                cmd.Parameters.AddWithValue("@USUARIO_INCLUYE", usuario_incluye);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                SqlDataReader lector = cmd.ExecuteReader();

                cmd.ExecuteNonQuery();
                rstp = true;
            }
            catch (Exception ex)
            {
                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }

            return rstp;
        }

        public Usuario validacion_acceso(string usuario, string clave)
        {
            Usuario rstp = new Usuario();
            try
            {

                //SqlCommand cmd = new SqlCommand("SELECT * FROM SL_USUARIOS WHERE USUARIO = @USUARIO AND CLAVE = @CLAVE;", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_VALIDACION_ACCESO", cxn);
                cmd.Parameters.AddWithValue("@USUARIO", usuario);
                cmd.Parameters.AddWithValue("@CLAVE", clave);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    rstp.nombre_completo = lector.GetString(1); 
                    rstp.usuario = lector.GetString(2);
                    rstp.perfil = lector.GetInt32(6);
                    rstp.admin = lector.GetInt32(7);
                }
            }
            catch (Exception ex)
            {
                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }
            return rstp;
        }

        /*Para Gráfico*/
        public List<string> obtener_nombres_etapas()
        {
            List<string> nombres = new List<string>();
            try
            {
                //SqlCommand cmd = new SqlCommand("SELECT NOMBRE_ETAPA FROM SL_ETAPAS", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_NOMBRES_ETAPAS", cxn);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                nombres.Add("TODOS");
                SqlDataReader lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    nombres.Add(lector.GetString(0));
                }
            }
            catch (Exception ex)
            {
                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }
            return nombres;
        }

        public bool verificar_duplicidad(string numero_operacion)
        {
            bool encontrado = false;
            try
            {
                //SqlCommand cmd = new SqlCommand("SELECT NUMERO_OPERACION FROM SL_EXPEDIENTE_LEGAL WITH(NOLOCK) WHERE NUMERO_OPERACION = @NUMERO_OPERACION AND ESTADO = 1", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_VERIFICAR_DUPLICIDAD", cxn);
                cmd.Parameters.AddWithValue("@NUMERO_OPERACION", numero_operacion);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();
                SqlDataReader lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    encontrado = true;
                }
            }
            catch (Exception ex)
            {
                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }
            return encontrado;
        }

        public List<string> obtener_informacion_cliente_summa(string numero_operacion)
        {
            List<string> cliente = new List<string>();
            conectarSumma();
            try
            {
                //SqlCommand cmd = new SqlCommand(@"SELECT CLI.CLI_CODIGO_CLIENTE_ORIGINAL, CLI.CLI_NOMBRE_COMPLETO FROM REC_CLIENTES CLI WITH(NOLOCK)
                //        INNER JOIN REC_OPERACIONES_COBROS COB WITH(NOLOCK) ON COB.CLI_IDENTIFICACION = CLI.CLI_IDENTIFICACION
                //        WHERE COB.PAI_CODIGO = 88 AND COB.REA_CODIGO NOT IN ('CANC','PCANC','FA') AND COB.OPE_NUMERO_OPERACION = @NUMERO_OPERACION;", cxn_summa);
                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_INFORMACION_CLIENTE_SUMMA", cxn_summa);
                cmd.Parameters.AddWithValue("@NUMERO_OPERACION", numero_operacion);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn_summa.Open();
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    cliente.Add(lector.GetString(0));
                    cliente.Add(lector.GetString(1));
                }
            }
            catch (Exception ex)
            {
                error.logError(ex);
            }
            finally
            {
                cxn_summa.Close();
            }
            return cliente;
        }

        public int registrar_cliente(string nombre_cliente, string nit, string dpi, string usuario)
        {
            int rspta = 0;
            string identificacion = (nit != "" ? nit : dpi);
            try
            {
                //SqlCommand cmd = new SqlCommand(@"INSERT INTO SL_CLIENTES(ID_NIT,ID_DPI,ID_IDENTIFICACION,NOMBRE_COMPLETO,USUARIO_INCLUYE,FECHA_INCLUYE)
                //        VALUES (@NIT,@DPI,@ID_IDENTIFICACION,@NOMBRE_CLIENTE,@USUARIO,GETDATE());SELECT SCOPE_IDENTITY();", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_REGISTRAR_CLIENTE", cxn);
                cmd.Parameters.AddWithValue("@NOMBRE_CLIENTE", nombre_cliente);
                cmd.Parameters.AddWithValue("@NIT", nit);
                cmd.Parameters.AddWithValue("@DPI", dpi);
                cmd.Parameters.AddWithValue("@ID_IDENTIFICACION", identificacion);
                cmd.Parameters.AddWithValue("@USUARIO", usuario);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                rspta = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }
            return rspta;
        }

        public List<Registros_Robot> obtener_registros_robot()
        {
            List<Registros_Robot> registros = new List<Registros_Robot>();
            Registros_Robot registro = null;
            try
            {
                SqlCommand cmd = new SqlCommand(@"
                SELECT rc.numero_operacion AS NÚMERO_DE_OPERACIÓN, rc.elemento_de_busqueda AS ELEMENTO_DE_BÚSQUEDA, rc.numero_identificador_dpi AS DPI, rc.nombre_cliente AS NOMBRE_DEL_CLIENTE,
                rtc.lugar_trabajo AS LUGAR_DE_TRABAJO, rtc.ultimo_mes_registrado AS ÚLTIMO_MES_REGISTRADO, rtc.ultimo_anio_registrado AS ULTIMO_AÑO_REGISTRADO  FROM registro_clientes AS rc
                Inner Join registro_trabajo_cliente AS rtc ON rc.id_registro_cliente = rtc.id_registro_cliente WHERE rc.encontrado = 1 AND rtc.ultimo_anio_registrado = '2018'; ", cxn);
                cxn.Open();
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    registro = new Registros_Robot();
                    //registro.id = lector.GetInt32(0);
                    registro.numero_operacion = lector.GetString(0);
                    registro.nombre_cliente = lector.GetString(1);
                    registro.identificacion = lector.GetString(3);
                    registro.lugar_trabajo = lector.GetString(4);
                    registro.mes = lector.GetString(5);
                    registro.anio = lector.GetString(6);
                    registros.Add(registro);
                }
            }
            catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;
}

public List<Resumen_Expedientes_Legales> obtener_expedientes_legales()
{
    List<Resumen_Expedientes_Legales> registros = new List<Resumen_Expedientes_Legales>();
    Resumen_Expedientes_Legales registro = null;

    try
    {

                //SqlCommand cmd = new SqlCommand(@"SELECT TOP(500) EX.ID_EXPEDIENTE_LEGAL,ISNULL(EX.NUMERO_PROCESO,''),
                //        RTRIM(REPLACE(CONVERT(varchar(50), EX.NUMERO_OPERACION), CHAR(9), '')) as NUMERO_OPERACION,
                //        CL.NOMBRE_COMPLETO,ET.NOMBRE_ETAPA,SU.NOMBRE_SUBETAPA,EX.FECHA_INCLUYE, EX.ESTADO
                //        FROM SL_EXPEDIENTE_LEGAL AS EX WITH(NOLOCK)
                //        INNER JOIN SL_CLIENTES AS CL WITH(NOLOCK) ON CL.ID_CLIENTE = EX.ID_CLIENTE 
                //        INNER JOIN SL_ETAPAS AS ET WITH(NOLOCK) ON ET.ID_ETAPA = EX.ID_ETAPA 
                //        INNER JOIN SL_SUBETAPAS AS SU WITH(NOLOCK)
                //        ON SU.ID_SUBETAPA = EX.ID_SUBETAPA
                //        WHERE EX.ESTADO = 1 ORDER BY FECHA_INCLUYE DESC", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_EXPEDIENTES_LEGALES", cxn);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

        SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registro = new Resumen_Expedientes_Legales();

            registro.numero_expediente = lector.GetInt32(0);
            registro.numero_proceso = lector.GetString(1);
            registro.numero_operacion = lector.GetString(2);
            registro.nombre_cliente = lector.GetString(3);
            registro.etapa = lector.GetString(4);
            registro.subetapa = lector.GetString(5);
            registro.fecha_incluye = lector.GetDateTime(6).ToString();
            registro.estado = lector.GetInt32(7);
            registros.Add(registro);
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;
}

public List<Resumen_Expedientes_Legales> obtener_expedientes_legales_filtro(string nombre, string numero_proceso, string numero_operacion, int etapa, int subetapa, int estado)
{
    List<Resumen_Expedientes_Legales> registros = new List<Resumen_Expedientes_Legales>();
    Resumen_Expedientes_Legales registro = null;
    SqlCommand cmd = null;

    string consulta_etapa = string.Empty;
    string consulta_subetapa = string.Empty;
    string consulta_nombre = string.Empty;
    string consulta_numero_proceso = string.Empty;
    string consulta_numero_operacion = string.Empty;

    if (nombre != "")
    {
        consulta_nombre = " AND CL.NOMBRE_COMPLETO LIKE @NOMBRE";
    }

    if (numero_proceso != "")
    {
        consulta_numero_proceso = " AND NUMERO_PROCESO LIKE @NUMERO_PROCESO";
    }

    if (numero_operacion != "")
    {
        consulta_numero_operacion = " AND NUMERO_OPERACION LIKE @NUMERO_OPERACION";
    }

    if (etapa != 0)
    {
        consulta_etapa = " AND EX.ID_ETAPA = @ETAPA";
    }

    if (subetapa != 0)
    {
        consulta_subetapa = " AND EX.ID_SUBETAPA = @SUBETAPA";
    }

    try
    {

        cmd = new SqlCommand(@"SELECT EX.ID_EXPEDIENTE_LEGAL,ISNULL(EX.NUMERO_PROCESO,'') AS NUMERO_PROCESO,
         RTRIM(REPLACE(CONVERT(varchar(50), EX.NUMERO_OPERACION), CHAR(9), '')) 
		  as NUMERO_OPERACION,CL.NOMBRE_COMPLETO,ET.NOMBRE_ETAPA,SU.NOMBRE_SUBETAPA,EX.FECHA_INCLUYE, EX.ESTADO 
        FROM SL_EXPEDIENTE_LEGAL AS EX WITH(NOLOCK)
        INNER JOIN SL_CLIENTES AS CL WITH(NOLOCK) ON CL.ID_CLIENTE = EX.ID_CLIENTE 
        INNER JOIN SL_ETAPAS AS ET WITH(NOLOCK) ON ET.ID_ETAPA = EX.ID_ETAPA INNER JOIN SL_SUBETAPAS AS SU WITH(NOLOCK)
        ON SU.ID_SUBETAPA = EX.ID_SUBETAPA
        WHERE EX.ESTADO = @ESTADO " + consulta_nombre + consulta_numero_proceso + consulta_numero_operacion + 
        consulta_etapa + consulta_subetapa + @" ORDER BY FECHA_INCLUYE DESC", cxn);

        cxn.Open();

        cmd.Parameters.AddWithValue("@NOMBRE", "%" + nombre + "%");
        cmd.Parameters.AddWithValue("@NUMERO_PROCESO", "%" + numero_proceso + "%");
        cmd.Parameters.AddWithValue("@NUMERO_OPERACION", "%" + numero_operacion + "%");
        cmd.Parameters.AddWithValue("@ETAPA", etapa);
        cmd.Parameters.AddWithValue("@SUBETAPA", subetapa);
        cmd.Parameters.AddWithValue("@ESTADO", estado);

        SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registro = new Resumen_Expedientes_Legales();

            registro.numero_expediente = lector.GetInt32(0);
            registro.numero_proceso = lector.GetString(1);
            registro.numero_operacion = lector.GetString(2);
            registro.nombre_cliente = lector.GetString(3);
            registro.etapa = lector.GetString(4);
            registro.subetapa = lector.GetString(5);
            registro.fecha_incluye = lector.GetDateTime(6).ToString();
            registro.estado = lector.GetInt32(7);
            registros.Add(registro);
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;
}


public DataTable obtener_expedientes_Reporte(string nombre, string numero_proceso, string numero_operacion, int etapa, int subetapa, int estado, string fecha)
{

    DataTable data = null;

    SqlCommand cmd = null;

    string consulta_etapa = string.Empty;
    string consulta_subetapa = string.Empty;
    string consulta_nombre = string.Empty;
    string consulta_numero_proceso = string.Empty;
    string consulta_numero_operacion = string.Empty;

    if (nombre != "")
    {
        consulta_nombre = " AND CL.NOMBRE_COMPLETO LIKE @NOMBRE";
    }

    if (numero_proceso != "")
    {
        consulta_numero_proceso = " AND NUMERO_PROCESO LIKE @NUMERO_PROCESO";
    }

    if (numero_operacion != "")
    {
        consulta_numero_operacion = " AND NUMERO_OPERACION LIKE @NUMERO_OPERACION";
    }

    if (etapa != 0)
    {
        consulta_etapa = " AND EX.ID_ETAPA = @ETAPA";
    }

    if (subetapa != 0)
    {
        consulta_subetapa = " AND EX.ID_SUBETAPA = @SUBETAPA";
    }

    try
    {
                cmd = new SqlCommand(@"SELECT EX.ID_EXPEDIENTE_LEGAL EXPEDIENTE,
									CL.NOMBRE_COMPLETO CLIENTE,
									EX.NUMERO_OPERACION OPERACION,
									EX.NOMBRE_JUZGADO JUZGADO,
                                    
									ISNULL(EX.NUMERO_PROCESO,'') 'NUMERO DE PROCESO',
									EX.OFICIAL_A_CARGO OFICIAL,
                                    ISNULL(EX.MONTO_DEMANDA,0) 'MONTO DEMANDA',
									CASE WHEN EX.ESTADO =1 Then 'ADMITIDA'
										 WHEN EX.ESTADO =2 Then 'RECHAZADA'
										 WHEN EX.ESTADO = 0 Then 'CERRADO'
										END ESTADO,
									 IIF( CONVERT(nvarchar(25),EX.EMBARGO_BANCO) ='1', 'SI', 'NO') 'BANCO',
									 IIF( CONVERT(nvarchar(25),EX.EMBARGO_SALARIO) ='1', 'SI', 'NO') 'SALARIO',
									 IIF( CONVERT(nvarchar(25), EX.EMBARGO_OTRO) ='1', 'SI', 'NO') 'OTROS',
                                    ET.NOMBRE_ETAPA ETAPA,
								    SU.NOMBRE_SUBETAPA 'SUB ETAPA',
                                   
									EX.DETALLES DETALLE,
                                  
									 ISNULL(EX.MONTO_CANCELAR, 0) 'MONTO NEGOCIACION',
									 ISNULL(EX.MONTO_MENSUAL, 0) 'MONTO RECIBIDO',
                                      HE.fechaDOM 'FECHA_ACTUALIZACION'
                                    FROM SL_EXPEDIENTE_LEGAL AS EX WITH(NOLOCK)
                                    INNER JOIN SL_CLIENTES AS CL WITH(NOLOCK) ON CL.ID_CLIENTE = EX.ID_CLIENTE
                                    INNER JOIN SL_ETAPAS AS ET WITH(NOLOCK) ON ET.ID_ETAPA = EX.ID_ETAPA 
                                    INNER JOIN  (SELECT ID_EXPEDIENTE_LEGAL, MAX(CAST(FECHA_INCLUYE  AS DATE)) as fechaDOM FROM SL_HISTORIAL_EXPEDIENTE_LEGAL GROUP BY ID_EXPEDIENTE_LEGAL ) HE ON  HE.ID_EXPEDIENTE_LEGAL = EX.ID_EXPEDIENTE_LEGAL  
                                    INNER JOIN SL_SUBETAPAS AS SU WITH(NOLOCK)	
                                    ON SU.ID_SUBETAPA = EX.ID_SUBETAPA " + consulta_nombre + consulta_numero_proceso + consulta_numero_operacion + consulta_etapa + consulta_subetapa + @"
                                    WHERE EX.ESTADO=@ESTADO
                                    ORDER BY EX.FECHA_INCLUYE DESC", cxn);

        cmd.Parameters.AddWithValue("@NOMBRE", "%" + nombre + "%");
        cmd.Parameters.AddWithValue("@NUMERO_PROCESO", "%" + numero_proceso + "%");
        cmd.Parameters.AddWithValue("@NUMERO_OPERACION", "%" + numero_operacion + "%");
        cmd.Parameters.AddWithValue("@ETAPA", etapa);
        cmd.Parameters.AddWithValue("@SUBETAPA", subetapa);
        cmd.Parameters.AddWithValue("@ESTADO", estado);
       
        cxn.Open();


        SqlDataAdapter sda = new SqlDataAdapter();

        sda.SelectCommand = cmd;

        data = new DataTable();
        sda.Fill(data);

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return data;
}
















        public DataTable obtener_expedientes_Reporte_sin_mov(string nombre, string numero_proceso, string numero_operacion, int etapa, int subetapa, int estado, string fecha)
        {

            DataTable data = null;

            SqlCommand cmd = null;

            string consulta_etapa = string.Empty;
            string consulta_subetapa = string.Empty;
            string consulta_nombre = string.Empty;
            string consulta_numero_proceso = string.Empty;
            string consulta_numero_operacion = string.Empty;

            if (nombre != "")
            {
                consulta_nombre = " AND CL.NOMBRE_COMPLETO LIKE @NOMBRE";
            }

            if (numero_proceso != "")
            {
                consulta_numero_proceso = " AND NUMERO_PROCESO LIKE @NUMERO_PROCESO";
            }

            if (numero_operacion != "")
            {
                consulta_numero_operacion = " AND NUMERO_OPERACION LIKE @NUMERO_OPERACION";
            }

            if (etapa != 0)
            {
                consulta_etapa = " AND EX.ID_ETAPA = @ETAPA";
            }

            if (subetapa != 0)
            {
                consulta_subetapa = " AND EX.ID_SUBETAPA = @SUBETAPA";
            }

            try
            {
                cmd = new SqlCommand(@"SELECT EX.ID_EXPEDIENTE_LEGAL EXPEDIENTE,
									CL.NOMBRE_COMPLETO CLIENTE,
									EX.NUMERO_OPERACION OPERACION,
									EX.NOMBRE_JUZGADO JUZGADO,
                                    
									ISNULL(EX.NUMERO_PROCESO,'') 'NUMERO DE PROCESO',
									EX.OFICIAL_A_CARGO OFICIAL,
                                    ISNULL(EX.MONTO_DEMANDA,0) 'MONTO DEMANDA',
									CASE WHEN EX.ESTADO =1 Then 'ADMITIDA'
										 WHEN EX.ESTADO =2 Then 'RECHAZADA'
										 WHEN EX.ESTADO = 0 Then 'CERRADO'
										END ESTADO,
									 IIF( CONVERT(nvarchar(25),EX.EMBARGO_BANCO) ='1', 'SI', 'NO') 'BANCO',
									 IIF( CONVERT(nvarchar(25),EX.EMBARGO_SALARIO) ='1', 'SI', 'NO') 'SALARIO',
									 IIF( CONVERT(nvarchar(25), EX.EMBARGO_OTRO) ='1', 'SI', 'NO') 'OTROS',
                                    ET.NOMBRE_ETAPA ETAPA,
								    SU.NOMBRE_SUBETAPA 'SUB ETAPA',
                                   
									EX.DETALLES DETALLE,
                                  
									 ISNULL(EX.MONTO_CANCELAR, 0) 'MONTO NEGOCIACION',
									 ISNULL(EX.MONTO_MENSUAL, 0) 'MONTO RECIBIDO',
                                      HE.fechaDOM 'FECHA_ACTUALIZACION'
                                    FROM SL_EXPEDIENTE_LEGAL AS EX WITH(NOLOCK)
                                    INNER JOIN SL_CLIENTES AS CL WITH(NOLOCK) ON CL.ID_CLIENTE = EX.ID_CLIENTE
                                    INNER JOIN SL_ETAPAS AS ET WITH(NOLOCK) ON ET.ID_ETAPA = EX.ID_ETAPA 
                                    INNER JOIN  (SELECT ID_EXPEDIENTE_LEGAL,  DATEDIFF(MONTH,  MAX(CAST(FECHA_INCLUYE  AS DATE)), SYSDATETIME()) AS fechmont  , MAX(CAST(FECHA_INCLUYE  AS DATE)) as fechaDOM FROM SL_HISTORIAL_EXPEDIENTE_LEGAL GROUP BY ID_EXPEDIENTE_LEGAL ) HE ON  (HE.ID_EXPEDIENTE_LEGAL = EX.ID_EXPEDIENTE_LEGAL)  AND  (HE.fechmont > 4)
                                    INNER JOIN SL_SUBETAPAS AS SU WITH(NOLOCK)	
                                    ON SU.ID_SUBETAPA = EX.ID_SUBETAPA " + consulta_nombre + consulta_numero_proceso + consulta_numero_operacion + consulta_etapa + consulta_subetapa + @"
                                    WHERE EX.ESTADO=@ESTADO
                                    ORDER BY EX.FECHA_INCLUYE DESC", cxn);

                cmd.Parameters.AddWithValue("@NOMBRE", "%" + nombre + "%");
                cmd.Parameters.AddWithValue("@NUMERO_PROCESO", "%" + numero_proceso + "%");
                cmd.Parameters.AddWithValue("@NUMERO_OPERACION", "%" + numero_operacion + "%");
                cmd.Parameters.AddWithValue("@ETAPA", etapa);
                cmd.Parameters.AddWithValue("@SUBETAPA", subetapa);
                cmd.Parameters.AddWithValue("@ESTADO", estado);

                cxn.Open();


                SqlDataAdapter sda = new SqlDataAdapter();

                sda.SelectCommand = cmd;

                data = new DataTable();
                sda.Fill(data);

            }
            catch (Exception ex)
            {

                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }

            return data;
        }

































        public List<Informacion_Etapa> obtener_preparacion_demanda()
{
    List<Informacion_Etapa> registros = new List<Informacion_Etapa>();
    Informacion_Etapa registro = null;

    try
    {

                //SqlCommand cmd = new SqlCommand(@"SELECT EX.ID_EXPEDIENTE_LEGAL, EX.NUMERO_OPERACION, CL.NOMBRE_COMPLETO,SU.NOMBRE_SUBETAPA FROM SL_EXPEDIENTE_LEGAL AS EX WITH(NOLOCK) INNER JOIN SL_CLIENTES AS CL WITH(NOLOCK)
                //        ON CL.ID_CLIENTE = EX.ID_CLIENTE INNER JOIN SL_SUBETAPAS AS SU WITH(NOLOCK) ON SU.ID_SUBETAPA = EX.ID_SUBETAPA WHERE EX.ID_ETAPA = 1 AND EX.ESTADO = 1", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_PREPARACION_DEMANDA", cxn);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();


        SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registro = new Informacion_Etapa();

            registro.id = lector.GetInt32(0);
            registro.numero_operacion = lector.GetString(1);
            registro.nombre_cliente = lector.GetString(2);
            registro.subetapa = lector.GetString(3);
            registros.Add(registro);
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;
}

public List<Informacion_Etapa> obtener_arreglo_extra_judicial()
{
    List<Informacion_Etapa> registros = new List<Informacion_Etapa>();
    Informacion_Etapa registro = null;

    try
    {

                //SqlCommand cmd = new SqlCommand(@"SELECT EX.ID_EXPEDIENTE_LEGAL, EX.NUMERO_OPERACION, CL.NOMBRE_COMPLETO,TI.NOMBRE_TIPO_PAGO, EX.VIA_DE_PAGO,ISNULL(EX.MONTO_CANCELAR,0.00) AS MONTO_CANCELAR,ISNULL(EX.MONTO_CARGOS,0.00) AS MONTO_CARGOS, SU.NOMBRE_SUBETAPA FROM SL_EXPEDIENTE_LEGAL AS EX WITH(NOLOCK) INNER JOIN SL_CLIENTES AS CL WITH(NOLOCK)
                //        ON CL.ID_CLIENTE = EX.ID_CLIENTE INNER JOIN SL_SUBETAPAS AS SU WITH(NOLOCK) ON SU.ID_SUBETAPA = EX.ID_SUBETAPA INNER JOIN SL_TIPO_PAGO AS TI WITH(NOLOCK) ON TI.ID_TIPO_PAGO =
                //        EX.ID_TIPO_PAGO
                //        WHERE EX.ID_ETAPA = 2 AND EX.ESTADO = 1", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_EXPEDIENTES_LEGALES", cxn);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

        SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registro = new Informacion_Etapa();

            registro.id = lector.GetInt32(0);
            registro.numero_operacion = lector.GetString(1);
            registro.nombre_cliente = lector.GetString(2);
            registro.tipo_pago = lector.GetString(3);
            registro.via_pago = lector.GetString(4);
            registro.monto_cancelar = lector.GetDecimal(5).ToString("### ### ###.00");
            registro.monto_cargos = lector.GetDecimal(6).ToString("### ### ###.00");
            registros.Add(registro);
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;
}

public List<Informacion_Etapa> obtener_arreglo_presentacion_demanda()
{
    List<Informacion_Etapa> registros = new List<Informacion_Etapa>();
    Informacion_Etapa registro = null;

    try
    {

                //SqlCommand cmd = new SqlCommand(@"SELECT EX.ID_EXPEDIENTE_LEGAL, EX.NUMERO_OPERACION, CL.NOMBRE_COMPLETO,PR.NOMBRE_PROCESO_JUDICIAL, EX.NOMBRE_JUZGADO,EX.NUMERO_PROCESO, EX.OFICIAL_A_CARGO, SU.NOMBRE_SUBETAPA 
                //        FROM SL_EXPEDIENTE_LEGAL AS EX WITH(NOLOCK) INNER JOIN SL_CLIENTES AS CL WITH(NOLOCK) ON CL.ID_CLIENTE = EX.ID_CLIENTE INNER JOIN SL_SUBETAPAS AS SU WITH(NOLOCK) ON 
                //        SU.ID_SUBETAPA = EX.ID_SUBETAPA INNER JOIN SL_PROCESOS_JUDICIALES AS PR WITH(NOLOCK) ON PR.ID_PROCESO_JUDICIAL = EX.ID_PROCESO_JUDICIAL
                //        WHERE EX.ID_ETAPA = 3 AND EX.ESTADO = 1", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_ARREGLO_PRESENTACION_DEMANDA", cxn);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registro = new Informacion_Etapa();

            registro.id = lector.GetInt32(0);
            registro.numero_operacion = lector.GetString(1);
            registro.nombre_cliente = lector.GetString(2);
            registro.id_proceso_judicial = lector.GetString(3);
            registro.nombre_juzgado = lector.GetString(3);
            registro.numero_proceso = lector.GetString(3);
            registro.oficial = lector.GetString(3);
            registro.subetapa = lector.GetString(3);

            registros.Add(registro);
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;
}

public List<Informacion_Etapa> obtener_arreglo_judicial()
{
    List<Informacion_Etapa> registros = new List<Informacion_Etapa>();
    Informacion_Etapa registro = null;

    try
    {

                //SqlCommand cmd = new SqlCommand(@"SELECT EX.ID_EXPEDIENTE_LEGAL, EX.NUMERO_OPERACION, CL.NOMBRE_COMPLETO,SU.NOMBRE_SUBETAPA FROM SL_EXPEDIENTE_LEGAL AS EX WITH(NOLOCK) INNER JOIN SL_CLIENTES AS CL WITH(NOLOCK)
                //        ON CL.ID_CLIENTE = EX.ID_CLIENTE INNER JOIN SL_SUBETAPAS AS SU WITH(NOLOCK) ON SU.ID_SUBETAPA = EX.ID_SUBETAPA WHERE EX.ID_ETAPA = 4 AND EX.ESTADO = 1;", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_ARREGLO_JUDICIAL", cxn);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registro = new Informacion_Etapa();

            registro.id = lector.GetInt32(0);
            registro.numero_operacion = lector.GetString(1);
            registro.nombre_cliente = lector.GetString(2);
            registro.subetapa = lector.GetString(3);
            registros.Add(registro);
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;
}

public List<Informacion_Etapa> obtener_diligenciamiento()
{
    List<Informacion_Etapa> registros = new List<Informacion_Etapa>();
    Informacion_Etapa registro = null;

    try
    {

                //SqlCommand cmd = new SqlCommand(@"SELECT EX.ID_EXPEDIENTE_LEGAL, EX.NUMERO_OPERACION, CL.NOMBRE_COMPLETO,SU.NOMBRE_SUBETAPA FROM SL_EXPEDIENTE_LEGAL AS EX WITH(NOLOCK) INNER JOIN SL_CLIENTES AS CL WITH(NOLOCK)
                //        ON CL.ID_CLIENTE = EX.ID_CLIENTE INNER JOIN SL_SUBETAPAS AS SU WITH(NOLOCK) ON SU.ID_SUBETAPA = EX.ID_SUBETAPA WHERE EX.ID_ETAPA = 5 AND EX.ESTADO = 1;", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_DILIGENCIAMIENTO", cxn);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registro = new Informacion_Etapa();

            registro.id = lector.GetInt32(0);
            registro.numero_operacion = lector.GetString(1);
            registro.nombre_cliente = lector.GetString(2);
            registro.subetapa = lector.GetString(3);
            registros.Add(registro);
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;
}

public List<Tipo_Pago> obtener_tipo_pago()
{
    List<Tipo_Pago> registros = new List<Tipo_Pago>();
    Tipo_Pago registro = null;

    try
    {

                //SqlCommand cmd = new SqlCommand(@"SELECT ID_TIPO_PAGO,NOMBRE_TIPO_PAGO FROM SL_TIPO_PAGO WITH(NOLOCK) WHERE ESTADO = 1;", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_TIPO_PAGO", cxn);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registro = new Tipo_Pago();

            registro.id_tipo_pago = lector.GetInt32(0);
            registro.nombre = lector.GetString(1);
            registros.Add(registro);
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;
}

public List<Proceso_Judicial> obtener_proceso_judicial()
{

    List<Proceso_Judicial> registros = new List<Proceso_Judicial>();
    Proceso_Judicial registro = null;

    try
    {

                //SqlCommand cmd = new SqlCommand(@"SELECT ID_PROCESO_JUDICIAL,NOMBRE_PROCESO_JUDICIAL FROM SL_PROCESOS_JUDICIALES WITH(NOLOCK) WHERE ESTADO = 1;", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_PROCESO_JUDICIAL", cxn);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registro = new Proceso_Judicial();

            registro.id = lector.GetInt32(0);
            registro.nombre = lector.GetString(1);
            registros.Add(registro);
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;
}

public List<string> obtener_juzgados()
{

    List<string> registros = new List<string>();
    try
    {

                //SqlCommand cmd = new SqlCommand(@"SELECT NOMBRE_JUZGADO FROM SL_JUZGADOS WITH(NOLOCK) WHERE ESTADO = 1;", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_JUZGADOS", cxn);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registros.Add(lector.GetString(0));
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;

}

public List<string> obtener_oficiales()
{

    List<string> registros = new List<string>();
    try
    {

                //SqlCommand cmd = new SqlCommand(@"SELECT NOMBRE FROM SL_OFICIALES WITH(NOLOCK) WHERE ESTADO = 1;", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_OFICIALES", cxn);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registros.Add(lector.GetString(0));
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;

}

public List<Historial_Expediente> obtener_historial(int id_expediente)
{
    List<Historial_Expediente> registros = new List<Historial_Expediente>();
    Historial_Expediente registro = null;

    try
    {

                //SqlCommand cmd = new SqlCommand(@"SELECT ET.NOMBRE_ETAPA, SU.NOMBRE_SUBETAPA, HE.ARCHIVO_ADJUNTO,HE.DETALLES, HE.USUARIO_INCLUYE,HE.FECHA_INCLUYE FROM SL_HISTORIAL_EXPEDIENTE_LEGAL AS HE WITH(NOLOCK) 
                //        INNER JOIN SL_ETAPAS AS ET WITH(NOLOCK) ON ET.ID_ETAPA = HE.ID_ETAPA  INNER JOIN SL_SUBETAPAS AS SU WITH(NOLOCK) ON SU.ID_SUBETAPA = HE.ID_SUBETAPA
                //        WHERE HE.ID_EXPEDIENTE_LEGAL = @ID AND HE.ESTADO = 1 ORDER BY FECHA_INCLUYE DESC;", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_HISTORIAL", cxn);
                cmd.Parameters.AddWithValue("@ID", id_expediente);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();
               

        SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registro = new Historial_Expediente();
           
            registro.nombre_etapa = lector.GetString(0);
            registro.nombre_subetapa = lector.GetString(1);
            registro.ruta_archivos = lector.GetString(2);
            registro.detalles = lector.GetString(3);
            registro.usuario = lector.GetString(4);
            registro.fecha_registro = lector.GetDateTime(5).ToString();
                     registro.id = lector.GetInt32(6);

            registros.Add(registro);
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;

}
        ///////////////////////////////////////////////////////////////////////////////////////// OBTENER PDF ////////////////////////////////////////////////////////////////

        public List<Historial_Expediente> obtener_historial_pdf(int id_expediente)
        {
            List<Historial_Expediente> registros = new List<Historial_Expediente>();
            Historial_Expediente registro = null;

            try
            {

                //SqlCommand cmd = new SqlCommand(@"SELECT ET.NOMBRE_ETAPA, SU.NOMBRE_SUBETAPA, HE.ARCHIVO_ADJUNTO,HE.DETALLES, HE.USUARIO_INCLUYE,HE.FECHA_INCLUYE FROM SL_HISTORIAL_EXPEDIENTE_LEGAL AS HE WITH(NOLOCK) 
                //        INNER JOIN SL_ETAPAS AS ET WITH(NOLOCK) ON ET.ID_ETAPA = HE.ID_ETAPA  INNER JOIN SL_SUBETAPAS AS SU WITH(NOLOCK) ON SU.ID_SUBETAPA = HE.ID_SUBETAPA
                //        WHERE HE.ID_EXPEDIENTE_LEGAL = @ID AND HE.ESTADO = 1 ORDER BY FECHA_INCLUYE DESC;", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_PDF_VER", cxn);
                cmd.Parameters.AddWithValue("@ID", id_expediente);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();


                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    registro = new Historial_Expediente();

                    registro.nombre_etapa = lector.GetString(0);
                    registro.nombre_subetapa = lector.GetString(1);
                    registro.detalles = lector.GetString(2);
                    registro.ruta_archivos = "<a href='"+lector.GetString(3)+ "' target='_blank'>" + "PDF"+"</a>";
                    

                    registros.Add(registro);
                }

            }
            catch (Exception ex)
            {

                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }

            return registros;

        }




        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public List<Historial_Expediente> obtener_historial_etapa(int id_expediente, int etapa)
{
    List<Historial_Expediente> registros = new List<Historial_Expediente>();
    Historial_Expediente registro = null;

    try
    {

                //SqlCommand cmd = new SqlCommand(@"SELECT ET.NOMBRE_ETAPA, SU.NOMBRE_SUBETAPA, HE.ARCHIVO_ADJUNTO,HE.DETALLES, 
                //        HE.USUARIO_INCLUYE,HE.FECHA_INCLUYE FROM SL_HISTORIAL_EXPEDIENTE_LEGAL AS HE WITH(NOLOCK) 
                //        INNER JOIN SL_ETAPAS AS ET WITH(NOLOCK) ON ET.ID_ETAPA = HE.ID_ETAPA  
                //        INNER JOIN SL_SUBETAPAS AS SU WITH(NOLOCK) ON SU.ID_SUBETAPA = HE.ID_SUBETAPA
                //        WHERE HE.ID_EXPEDIENTE_LEGAL = @ID AND  HE.ESTADO = 1 AND HE.ID_ETAPA = @ID_ETAPA;", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_HISTORIAL_ETAPA", cxn);
                cmd.Parameters.AddWithValue("@ID_ETAPA", etapa);
        cmd.Parameters.AddWithValue("@ID", id_expediente);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registro = new Historial_Expediente();

            registro.nombre_etapa = lector.GetString(0);
            registro.nombre_subetapa = lector.GetString(1);
            registro.ruta_archivos = lector.GetString(2);
            registro.detalles = lector.GetString(3);
            registro.usuario = lector.GetString(4);
            registro.fecha_registro = lector.GetDateTime(5).ToString();

            registros.Add(registro);
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;

}

public int obtener_numero_expediente(string numero_operacion)
{
    int rspta = 0;

    try

    {
                //SqlCommand cmd = new SqlCommand(@"SELECT ID_EXPEDIENTE_LEGAL FROM SL_EXPEDIENTE_LEGAL WHERE NUMERO_OPERACION = @NUMERO_OPERACION AND ESTADO = 1;", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_NUMERO_OPERACION", cxn);

                cmd.Parameters.AddWithValue("@NUMERO_OPERACION", numero_operacion);

                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            rspta = lector.GetInt32(0);
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return rspta;
}


public Informacion_Demandado obtener_informacion_demanda(string numero_operacion)
{
    Informacion_Demandado registro = new Informacion_Demandado();

    try
    {
                //        SqlCommand cmd = new SqlCommand(@"SELECT TOP 1 CL.NOMBRE_COMPLETO, TP.NOMBRE_TIPO_PAGO, VIA_DE_PAGO, ISNULL(MONTO_CANCELAR,0.00) AS MONTO_CANCELAR,ISNULL(MONTO_CARGOS,0.00) AS MONTO_CARGOS,ISNULL(MONTO_TOTAL,0.00) AS MONTO_TOTAL, ISNULL(EX.MONTO_MENSUAL,0.00) AS MONTO_MENSUAL,TRACTOS,
                //ISNULL(EX.FECHA_INICIO_PAGO,'1900-01-01 00:00:00.000') AS FECHA_INICIO_PAGO,FECHA_FINALIZACION_PAGO,PR.NOMBRE_PROCESO_JUDICIAL,NUMERO_PROCESO, NOMBRE_JUZGADO, OFICIAL_A_CARGO,EX.DETALLES,ET.NOMBRE_ETAPA,SETA.NOMBRE_SUBETAPA
                //FROM SL_EXPEDIENTE_LEGAL AS EX WITH(NOLOCK)
                //INNER JOIN SL_TIPO_PAGO AS TP WITH(NOLOCK) ON TP.ID_TIPO_PAGO = EX.ID_TIPO_PAGO INNER JOIN SL_CLIENTES AS CL WITH(NOLOCK) ON
                //CL.ID_CLIENTE = EX.ID_CLIENTE INNER JOIN SL_PROCESOS_JUDICIALES AS PR WITH(NOLOCK) ON PR.ID_PROCESO_JUDICIAL = EX.ID_PROCESO_JUDICIAL INNER JOIN SL_ETAPAS AS ET WITH(NOLOCK) ON EX.ID_ETAPA = ET.ID_ETAPA INNER JOIN
                //SL_SUBETAPAS AS SETA WITH(NOLOCK) ON EX.ID_SUBETAPA = SETA.ID_SUBETAPA 
                //WHERE NUMERO_OPERACION LIKE @NUMERO_OPERACION ORDER BY EX.FECHA_INCLUYE DESC;", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_INFORMACION_DEMANDA", cxn);
                cmd.Parameters.AddWithValue("@NUMERO_OPERACION", "%" + numero_operacion + "%");
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registro.nombre_cliente = lector.GetString(0);
            registro.nombre_tipo_pago = lector.GetString(1);
            registro.via_pago = lector.GetString(2);
            registro.monto_cancelar = lector.GetDecimal(3);
            registro.monto_cargos = lector.GetDecimal(4);
            registro.monto_total = lector.GetDecimal(5);
            registro.monto_mensual = lector.GetDecimal(6);
            registro.tratos = int.Parse(lector["TRACTOS"].ToString());
            registro.fecha_ini_pago = lector["FECHA_INICIO_PAGO"].ToString();
            registro.fecha_fin_pago = lector["FECHA_FINALIZACION_PAGO"].ToString();
            registro.nombre_proceso_judicial = lector.GetString(10);
            registro.numero_proceso = lector.GetString(11);
            registro.nombre_juzgado = lector.GetString(12);
            registro.oficial = lector.GetString(13);
            registro.detalles = lector.GetString(14);
            registro.etapa = lector.GetString(15);
            registro.subetapa = lector.GetString(16);

        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registro;
}

public decimal obtener_total_etapas_gt()
{
    decimal resultado = 0;

    try
    {

                //SqlCommand cmd = new SqlCommand(@"SELECT COUNT(ID_EXPEDIENTE_LEGAL) FROM SL_EXPEDIENTE_LEGAL WHERE ESTADO = 1;", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_TOTAL_ETAPAS_GT", cxn);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            resultado = lector.GetInt32(0);

        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return resultado;
}

public decimal obtener_total_etapa_gt(int etapa)
{
    decimal resultado = 0;

    try
    {

                //SqlCommand cmd = new SqlCommand(@"SELECT COUNT(ID_ETAPA) FROM SL_EXPEDIENTE_LEGAL WHERE ESTADO = 1 AND ID_ETAPA = @ETAPA;", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_TOTAL_ETAPAS_GT_FILTRO", cxn);
                cmd.Parameters.AddWithValue("@ETAPA", etapa);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            resultado = lector.GetInt32(0);

        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return resultado;
}


public List<int> obtener_cantidad_etapas_gt()
{
    List<int> registros = new List<int>();

    try
    {

        SqlCommand cmd = new SqlCommand(@"SL_SP_CANTIDAD_ETAPAS", cxn);

        cxn.Open();
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registros.Add(lector.GetInt32(0));
            registros.Add(lector.GetInt32(1));
            registros.Add(lector.GetInt32(2));
            registros.Add(lector.GetInt32(3));
            registros.Add(lector.GetInt32(4));
            registros.Add(lector.GetInt32(5));
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;
}



public void registrar_rutas(string rutas, int numero_expediente)
{
    try
    {

                //SqlCommand cmd = new SqlCommand(@"UPDATE SL_HISTORIAL_EXPEDIENTE_LEGAL SET ARCHIVO_ADJUNTO = @RUTA WHERE ID_HISTORIAL = (SELECT TOP 1 ID_HISTORIAL FROM SL_HISTORIAL_EXPEDIENTE_LEGAL WHERE ID_EXPEDIENTE_LEGAL = @ID ORDER BY ID_HISTORIAL DESC)", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_REGISTRAR_RUTAS", cxn);
                cmd.Parameters.AddWithValue("@RUTA", rutas);
                cmd.Parameters.AddWithValue("@ID", numero_expediente);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                cmd.ExecuteNonQuery();

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

}


        ///////////////////////////////////////////////////////////////////// RUTAS VARIOS PDF'S ////////////////////////////////////////////////////////////////////////

        public void registrar_rutas_pdf(string rutas, int numero_expediente, string detalles)
        {
            try
            {

                //SqlCommand cmd = new SqlCommand(@"UPDATE SL_HISTORIAL_EXPEDIENTE_LEGAL SET ARCHIVO_ADJUNTO = @RUTA WHERE ID_HISTORIAL = (SELECT TOP 1 ID_HISTORIAL FROM SL_HISTORIAL_EXPEDIENTE_LEGAL WHERE ID_EXPEDIENTE_LEGAL = @ID ORDER BY ID_HISTORIAL DESC)", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_PDF", cxn);
                cmd.Parameters.AddWithValue("@RUTA", rutas);
                cmd.Parameters.AddWithValue("@ID", numero_expediente);
                cmd.Parameters.AddWithValue("@DET", detalles);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                error.logError(ex);
            }
            finally
            {
                cxn.Close();
            }

        }









        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool cerrar_proceso(int numero_expediente, string juztificacion)
{
    bool resultado = false;

    try
    {

                //SqlCommand cmd = new SqlCommand(@"UPDATE SL_EXPEDIENTE_LEGAL SET DETALLES = @JUZTIFICACION, ESTADO = 0 WHERE ID_EXPEDIENTE_LEGAL = @ID;", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_CERRAR_PROCESO", cxn);
                cmd.Parameters.AddWithValue("@ID", numero_expediente);
        cmd.Parameters.AddWithValue("@JUZTIFICACION", juztificacion);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                cmd.ExecuteNonQuery();

        resultado = true;

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return resultado;
}
public List<Expedientes_Finalizados> obtener_expedientes_finalizados()
{

    List<Expedientes_Finalizados> registros = new List<Expedientes_Finalizados>();
    Expedientes_Finalizados registro = null;


    try
    {

                //SqlCommand cmd = new SqlCommand(@"SELECT EX.ID_EXPEDIENTE_LEGAL, EX.NUMERO_OPERACION, CL.NOMBRE_COMPLETO, CL.ID_IDENTIFICACION,EX.DETALLES, EX.ESTADO FROM SL_EXPEDIENTE_LEGAL AS 
                //        EX WITH(NOLOCK) INNER JOIN SL_CLIENTES AS CL WITH(NOLOCK) ON CL.ID_CLIENTE = EX.ID_CLIENTE WHERE EX.ESTADO = 3;", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_EXPEDIENTES_FINALIZADOS", cxn);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();


                SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registro = new Expedientes_Finalizados();

            registro.numero_expediente = lector.GetInt32(0);
            registro.numero_operacion = lector.GetString(1);
            registro.nombre_cliente = lector.GetString(2);
            registro.identificacion = lector.GetString(3);
            registro.detalles = lector.GetString(4);
            registro.estado = lector.GetInt32(5).ToString();

            registros.Add(registro);
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;
}
public void registro_de_pago()
{

}

public void modificacion_de_pago()
{

}

public List<Pagos_Cuenta_Unica> obtener_pagos(string numero_operacion)
{
    List<Pagos_Cuenta_Unica> registros = new List<Pagos_Cuenta_Unica>();
    Pagos_Cuenta_Unica registro = null;

    try
    {

        SqlCommand cmd = new SqlCommand(@"SELECT GE.CONCEPTO_DE_PAGO, GE.MONTO_CANCELAR, GE.MONTO_CARGOS, GE.MONTO_RECIBIDO,(GE.MONTO_CANCELAR - GE.MONTO_RECIBIDO ) AS SALDO, 
                GE.FECHA_INCLUYE FROM SL_GESTION_DE_PAGOS AS GE WITH(NOLOCK) INNER JOIN SL_EXPEDIENTE_LEGAL AS EX WITH(NOLOCK) ON EX.ID_EXPEDIENTE_LEGAL = 
                GE.ID_EXPEDIENTE_LEGAL INNER JOIN SL_CLIENTES AS CL WITH(NOLOCK) ON CL.ID_CLIENTE = EX.ID_CLIENTE WHERE EX.ESTADO = 1 AND GE.NUMERO_OPERACION = @NUMERO_OPERACION;", cxn);

        cxn.Open();

        cmd.Parameters.AddWithValue("@NUMERO_OPERACION", numero_operacion);

        SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registro = new Pagos_Cuenta_Unica();
            registro.concepto_pago = lector.GetString(0);
            registro.monto_inicial = lector.GetDecimal(1);
            registro.monto_cargos = (lector[2] != DBNull.Value ? lector.GetDecimal(2) : 0);
            registro.monto_recibido = lector.GetDecimal(3);
            registro.saldo = lector.GetDecimal(4);
            registro.fecha = lector.GetDateTime(5).ToString();

            registros.Add(registro);
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;
}

public List<Pagos_Totales> obtener_pagos_totales()
{
    List<Pagos_Totales> registros = new List<Pagos_Totales>();
    Pagos_Totales registro = null;

    try
    {

        SqlCommand cmd = new SqlCommand(@"SELECT EX.ID_EXPEDIENTE_LEGAL,EX.NUMERO_OPERACION,CL.NOMBRE_COMPLETO,CL.ID_IDENTIFICACION,GE.MONTO_CANCELAR, GE.MONTO_CARGOS, GE.MONTO_RECIBIDO,(GE.MONTO_CANCELAR - GE.MONTO_RECIBIDO ) AS SALDO, 
                GE.ESTADO FROM SL_GESTION_DE_PAGOS AS GE WITH(NOLOCK) INNER JOIN SL_EXPEDIENTE_LEGAL AS EX WITH(NOLOCK) ON EX.ID_EXPEDIENTE_LEGAL = 
                GE.ID_EXPEDIENTE_LEGAL INNER JOIN SL_CLIENTES AS CL WITH(NOLOCK) ON CL.ID_CLIENTE = EX.ID_CLIENTE WHERE EX.ESTADO = 1;", cxn);

        cxn.Open();


        SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            registro = new Pagos_Totales();

            registro.numero_expediente = lector.GetInt32(0);
            registro.numero_operacion = lector.GetString(1);
            registro.nombre_cliente = lector.GetString(2);
            registro.identificacion = lector.GetString(3);
            registro.monto_inicial = lector.GetDecimal(4);
            registro.monto_cargos = lector.GetDecimal(5);
            registro.monto_recibido = lector.GetDecimal(6);
            registro.saldo = lector.GetDecimal(7);
            registro.estado = lector.GetInt32(8);

            registros.Add(registro);
        }

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return registros;
}

public bool registrar_pago(string numero_expediente, string numero_operacion, string concepto, decimal monto, DateTime fecha, string observaciones, string usuario)
{
    bool resultado = false;

    try
    {

        SqlCommand cmd = new SqlCommand(@"SL_SP_REGISTRAR_PAGO", cxn);

        cxn.Open();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID_EXPEDIENTE_LEGAL", numero_expediente);
        cmd.Parameters.AddWithValue("@NUMERO_OPERACION", numero_operacion);
        cmd.Parameters.AddWithValue("@CONCEPTO_DE_PAGO", concepto);
        cmd.Parameters.AddWithValue("@MONTO_RECIBIDO", monto);
        cmd.Parameters.AddWithValue("@FECHA_PAGO", fecha);
        cmd.Parameters.AddWithValue("@DETALLES", observaciones);
        cmd.Parameters.AddWithValue("@USUARIO_INCLUYE", usuario);

        cmd.ExecuteNonQuery();

        resultado = true;

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return resultado;
}

public decimal obtener_monto_pago_mensual(string numero_operacion)
{
    decimal monto = 0;

    try
    {

                //SqlCommand cmd = new SqlCommand(@"SELECT MONTO_MENSUAL FROM SL_EXPEDIENTE_LEGAL WHERE NUMERO_OPERACION = @NUMERO_OPERACION;", cxn);

                SqlCommand cmd = new SqlCommand(@"SP_SL_OBTENER_MONTO_PAGO_MENSUAL", cxn);
                cmd.Parameters.AddWithValue("@NUMERO_OPERACION", numero_operacion);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();


                SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {

            monto = lector.GetDecimal(0);

        }


    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return monto;
}

public decimal obtener_monto_cargos(string numero_operacion)
{
    decimal monto = 0;

    try
    {

        SqlCommand cmd = new SqlCommand(@"SELECT TOP 1 MONTO_CARGOS FROM SL_GESTION_DE_PAGOS WHERE NUMERO_OPERACION = @NUMERO_OPERACION ORDER BY ID_GESTION DESC;", cxn);

        cxn.Open();
        cmd.Parameters.AddWithValue("@NUMERO_OPERACION", numero_operacion);


        SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {

            monto = lector.GetDecimal(0);

        }


    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return monto;
}

public decimal obtener_monto_total_pagar(string numero_operacion)
{
    decimal monto = 0;

    try
    {

        SqlCommand cmd = new SqlCommand(@"SELECT TOP 1 (MONTO_CANCELAR - MONTO_RECIBIDO + MONTO_CARGOS) AS SALDO FROM SL_GESTION_DE_PAGOS 
                WHERE NUMERO_OPERACION = @NUMERO_OPERACION ORDER BY ID_GESTION DESC;", cxn);

        cxn.Open();
        cmd.Parameters.AddWithValue("@NUMERO_OPERACION", numero_operacion);


        SqlDataReader lector = cmd.ExecuteReader();

        while (lector.Read())
        {

            monto = lector.GetDecimal(0);

        }


    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return monto;
}

public bool anular_proceso(int numero_expediente, string justificacion_anulacion)
{
    bool resultado = false;

    try
    {

                //SqlCommand cmd = new SqlCommand(@"UPDATE SL_EXPEDIENTE_LEGAL SET DETALLES = @JUSTIFICACION, ESTADO = 2 WHERE ID_EXPEDIENTE_LEGAL = @ID;", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_ANULAR_PROCESO", cxn);

                cmd.Parameters.AddWithValue("@ID", numero_expediente);
        cmd.Parameters.AddWithValue("@JUSTIFICACION", justificacion_anulacion);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();

                cmd.ExecuteNonQuery();

        resultado = true;

    }
    catch (Exception ex)
    {

        error.logError(ex);
    }
    finally
    {
        cxn.Close();
    }

    return resultado;
}

        public int caso_en_vista(int num_expediente, int visto)
        {
            int rstp = 0;
            try
            {
                //SqlCommand cmd = new SqlCommand(@"UPDATE SL_EXPEDIENTE_LEGAL SET EN_VISTA = @VISTO WHERE ID_EXPEDIENTE_LEGAL = @ID;", cxn);
                SqlCommand cmd = new SqlCommand(@"SP_SL_CASO_EN_VISTA", cxn);

                cxn.Open();
                cmd.Parameters.AddWithValue("@ID", num_expediente);
                cmd.Parameters.AddWithValue("@VISTO", visto);
                cmd.CommandType = CommandType.StoredProcedure;
                cxn.Open();
                cmd.ExecuteNonQuery();
                rstp = 1;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cxn.Close();
            }
            return rstp;
        }

        public bool actualizar_estado_caso(int p_estado, string p_operacion, int p_id_exp, string p_num_proces)
        {
            bool result = false;
            try
            {
                cxn.Open();
                string query = @"[dbo].[SP_SL_ACTUALIZAR_ESTADO_CASO]";
                SqlCommand cmd = new SqlCommand(query, cxn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ESTADO", p_estado);
                cmd.Parameters.AddWithValue("@OPERACION", p_operacion);
                cmd.Parameters.AddWithValue("@ID_EXP", p_id_exp);
                cmd.Parameters.AddWithValue("@NUMEROPROCESS", p_num_proces);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
                cxn.Close();
            }
            catch (SqlException e)
            {
                result = false;
                cxn.Close();
            }
            return result;
        }

        internal void registrar_rutas(string rutas, string v)
        {
            throw new NotImplementedException();
        }
    }
}
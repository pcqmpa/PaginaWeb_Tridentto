using MySql.Data.MySqlClient;
using PaginaTridentto.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginaTridentto.Clases
{
    public class SeguridadDao
    {
        private DataHelper _dataHelper;
        public SeguridadDao()
        {
            _dataHelper = new DataHelper();
        }

        public bool AddUsuario(Usuario modelo,out string strMensaje)
        {
            try
            {
                var parametros = new List<MySqlParameter>()
                {
                    new MySqlParameter("usuario",modelo.StrEmail),
                    new MySqlParameter("password",modelo.StrPassword),
                    new MySqlParameter("nombre",modelo.StrNombre),
                    new MySqlParameter("grupo",5),
                    new MySqlParameter("email",modelo.StrEmail)

                };

                var mensaje = new MySqlParameter("mensaje", MySqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
                var log_respuesta = new MySqlParameter("log_respuesta", MySqlDbType.Bit) { Direction = ParameterDirection.Output };

                parametros.Add(mensaje);
                parametros.Add(log_respuesta);

                var res = _dataHelper.EjecutarSp<int>("sg_spAddUsuario", parametros);

                if (res >= 0)
                {
                    strMensaje = mensaje.Value.ToString();
                    return Convert.ToBoolean(log_respuesta.Value);
                }
                else
                {
                    strMensaje = "La operación no se pudo realizar no hay conexion xon el servidor";
                    return false;
                }



            }
            catch (Exception ex)
            {
                strMensaje = ex.Message;
                return false;
            }
        }


        public Usuario ValidarUsuario(string usuario,string password,out string strMensaje)
        {
            try
            {
                var parametros = new List<MySqlParameter>()
                {
                    new MySqlParameter("usuario",usuario),
                    new MySqlParameter("password",password)
                };

                var dt = _dataHelper.EjecutarSp<DataTable>("sg_spValidar_Usuario", parametros);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        var log_respuesta = Convert.ToBoolean(dt.Rows[0]["logRespuesta"]);

                        if (log_respuesta)
                        {
                            var datos = new Usuario();

                            datos.Id = Convert.ToInt64(dt.Rows[0]["id"]);
                            datos.StrUsuario = dt.Rows[0]["strUsuario"].ToString();
                            datos.StrNombre = dt.Rows[0]["strNombre"].ToString();
                            datos.IdGrupo = Convert.ToInt16(dt.Rows[0]["idGrupo"]);
                            datos.StrNombreGrupo = dt.Rows[0]["Nombre_Grupo"].ToString();

                            strMensaje = "";
                            return datos;

                        }
                        else
                        {
                            strMensaje = dt.Rows[0]["Mensaje"].ToString();
                            return null;
                        }
                    }
                    else
                    {
                        strMensaje = "El usuario no existe";
                        return null;
                    }
                }
                else
                {
                    strMensaje = "No hay conexion con el servidor";
                    return null;
                }

            }
            catch (Exception ex)
            {
                strMensaje = ex.Message;
                return null;
            }
        }
    }
}

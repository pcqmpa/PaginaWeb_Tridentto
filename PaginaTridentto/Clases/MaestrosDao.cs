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
    public class MaestrosDao
    {

        private DataHelper _dataHelper;
        public MaestrosDao()
        {
            _dataHelper = new DataHelper();
        }

        public List<Paises> ListaPaises()
        {
            try
            {
                var dt = _dataHelper.EjecutarSp<DataTable>("ma_spListaPaises", null);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        var lista = new List<Paises>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            lista.Add(new Paises { Id = Convert.ToInt16(dt.Rows[i]["id"]), StrNombre = dt.Rows[i]["strNombre"].ToString() });

                        }

                        return lista;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<Departamentos> ListaDepartamentos()
        {
            try
            {
                var dt = _dataHelper.EjecutarSp<DataTable>("ma_spListaDepartamentos", null);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        var listado = new List<Departamentos>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            listado.Add(new Departamentos { Id = Convert.ToInt16(dt.Rows[i]["id"]), StrNombre = dt.Rows[i]["strNombre"].ToString() });

                        }

                        return listado;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }


        public List<Ciudades> ListaCiudades(int idDepartamento)
        {
            try
            {
                var parametros = new List<MySqlParameter>()
                {
                    new MySqlParameter("dtp",idDepartamento)
                };

                var dt = _dataHelper.EjecutarSp<DataTable>("ma_spCiudades", parametros);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        var lista = new List<Ciudades>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            lista.Add(new Ciudades { Id = Convert.ToInt16(dt.Rows[i]["id"]),IdDepartamento= Convert.ToInt16(dt.Rows[i]["iddpt"]),StrNombre=dt.Rows[i]["strNombre"].ToString() });


                        }

                        return lista;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool AddDatosComplementariosUsuario(DatosComplemenatriosUsuario modelo,out string strMensaje)
        {
            try
            {
                var parametros = new List<MySqlParameter>()
                {
                    new MySqlParameter("usuario",modelo.IdUsuario),
                    new MySqlParameter("pais",modelo.IdPais),
                    new MySqlParameter("departamento",modelo.IdDepartamento),
                    new MySqlParameter("ciudad",modelo.IdCiudad),
                    new MySqlParameter("telefono",modelo.StrTelefono),
                    new MySqlParameter("mobil",modelo.StrMobil)
                };

                var mensaje = new MySqlParameter("mensaje", MySqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
                var log_respuesta = new MySqlParameter("log_respuesta", MySqlDbType.Bit) { Direction = ParameterDirection.Output };

                parametros.Add(mensaje);
                parametros.Add(log_respuesta);

                var res = _dataHelper.EjecutarSp<int>("sg_spAddDatosComplementariosUsuario", parametros);

                if (res >= 0)
                {
                    strMensaje = mensaje.Value.ToString();
                    return Convert.ToBoolean(log_respuesta.Value);
                }
                else
                {
                    strMensaje = "No hay conexion con el servidor";
                    return false;
                }


            }
            catch (Exception ex)
            {

                strMensaje = ex.Message;
                return false;
            }
        
        }

        public DatosComplemenatriosUsuario ConsultaOtrosDatosUsuario(Int64 idUsuario)
        {
            try
            {
                var parametros = new List<MySqlParameter>()
                {
                    new MySqlParameter("usuario",idUsuario)
                };

                var dt = _dataHelper.EjecutarSp<DataTable>("sg_spConsultaOtros_Datos", parametros);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0) 
                    {

                        var datos = new DatosComplemenatriosUsuario();

                        datos.IdUsuario = Convert.ToInt64(dt.Rows[0]["idUsuario"]);
                        datos.IdPais = Convert.ToInt16(dt.Rows[0]["idPais"]);
                        datos.IdDepartamento = Convert.ToInt16(dt.Rows[0]["idDepartamento"]);
                        datos.IdCiudad = Convert.ToInt16(dt.Rows[0]["idCiudad"]);
                        datos.StrTelefono = dt.Rows[0]["strTelefonoFijo"].ToString();
                        datos.StrMobil = dt.Rows[0]["strMobil"].ToString();

                        return datos;
                    }
                    else
                    {
                        return null;
                    }


                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}

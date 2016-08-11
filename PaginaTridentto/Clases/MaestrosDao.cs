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
    }
}

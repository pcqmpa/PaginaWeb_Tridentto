using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginaTridentto.Clases
{
    public class DataHelper
    {
        private MySqlConnection _connection;
        private bool _connection_open;
        public DataHelper()
        {

        }

        public T EjecutarSp<T>(string spName, List<MySqlParameter> parametros)
        {

            GetConnection();

            try
            {
                var cmd = new MySqlCommand(spName, _connection) { CommandType = System.Data.CommandType.StoredProcedure };
                cmd.CommandTimeout = 0;

                if (parametros != null)
                {
                    cmd.Parameters.AddRange(parametros.ToArray());
                }

                //Validamos el tipo de dato a retornar


                if (typeof(T) == typeof(DataSet))
                {
                    var ds = new DataSet();
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(ds);
                    }

                    return (T)(Object)ds;
                }

                if (typeof(T) == typeof(DataTable))
                {
                    var dt = new DataTable();

                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }

                    return (T)(Object)dt;
                }

                if (typeof(T) == typeof(MySqlDataReader))
                {
                    var reader = cmd.ExecuteReader();
                    return (T)(Object)reader;
                }

                if (typeof(T) == typeof(int))
                {
                    var entero = cmd.ExecuteNonQuery();
                    return (T)(Object)entero;
                }

                if (typeof(T) == typeof(bool))
                {
                    bool retorno = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    return (T)(Object)retorno;

                }
                else
                {
                    throw new NotSupportedException(string.Format("El tipo {0} no es soportado", typeof(T).Name));
                }




            }
            finally
            {
                if (_connection_open == true)
                {
                    _connection.Close();
                    _connection_open = false;
                }
            }





        }

        private void GetConnection()
        {
            _connection_open = false;

            _connection = new MySqlConnection();

            _connection.ConnectionString = Properties.Settings.Default.conexion_datos;

            if (Open_Local_Connection())
            {
                _connection_open = true;
            }



        }

        private bool Open_Local_Connection()
        {
            try
            {
                _connection.Open();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
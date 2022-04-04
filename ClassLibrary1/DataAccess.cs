using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public abstract class DataAccess
    {
        public SqlConnection connection;

        ViveroEntities viveroEntities = new ViveroEntities();

        public DataAccess()
        {
            var connectionString = viveroEntities.Database.Connection.ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        public object Query(string consulta, Dictionary<string, object> parametros)
        {
            var sqlCommand = new SqlCommand(consulta, connection);

            //.sqlCommand.BeginExecuteNonQuery

            //.sqlCommand.BeginExecuteReader

            var sqlAdapter = new SqlDataAdapter(sqlCommand);

            DataTable resultado = new DataTable();

            string valor;

            object objeto;

            if (parametros != null)
            {
                foreach (KeyValuePair<string, object> auxiliar in parametros)
                {
                    valor = auxiliar.Key;
                    objeto = auxiliar.Value;

                    sqlCommand.Parameters.AddWithValue(valor, objeto);

                }
            }

            sqlAdapter.Fill(resultado);

            return resultado;
        }

        protected IEnumerable<T> QueryObject<T>(string consulta, Dictionary<string, object> parameters) where T : class, new()
        {
            var sqlCommand = new SqlCommand(consulta, connection);

            using (sqlCommand)
            {
                using (var sqlAdapter = new SqlDataAdapter(sqlCommand))
                {
                    DataTable resultad = new DataTable();

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            sqlCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
                        }
                    }

                    sqlAdapter.Fill(resultad);

                    return ParseFinal<T>(resultad);
                }
            }
        }

        private IEnumerable<T> ParseFinal<T>(DataTable data) where T : class, new()
        {
            var lista = new List<T>();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                var propiedades = GetProperties<T>();

                var entidad = new T();

                var row = data.Rows[i];

                foreach (var property in propiedades)
                {
                    if (row.Table.Columns.Contains(property.Name))
                    {
                        var valor = row[property.Name];

                        if (valor == DBNull.Value)
                        {
                            var valorTipo = property.DeclaringType;
                            if (valorTipo is string)
                            {
                                valor = string.Empty;
                            }
                            else if (valorTipo is int)
                            {
                                valor = 0;
                            }
                            else
                            {
                                valor = null;
                            }
                        }

                        property.SetValue(entidad, valor);
                    }
                }

                lista.Add(entidad);
            }

            return lista;
        }


        private PropertyInfo[] GetProperties<T>() where T : class, new()
        {
            var type = new T().GetType();

            var properties = type.GetProperties();

            return properties;
        }
    }
}

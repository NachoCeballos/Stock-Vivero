using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public abstract class BaseRepository<T> : DataAccess
    {
        //_tabla = nombre de la tabla
        //_id = nombre de id_tabla
        private readonly string _tabla, _id;

        public BaseRepository(string tabla, string id)
        {
            _tabla = tabla;
            _id = $"Id{id}";
        }
        public virtual T Agregar<T>(Dictionary<string, object> valuePairs) where T : class, new()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            string nombreVariable;

            var properties = GetProperties<T>();

            var consulta = $"INSERT INTO [dbo].[{_tabla}](";

            foreach (var propertie in properties)
            {
                nombreVariable = propertie.Name;

                if (nombreVariable != _id) consulta += $"[{nombreVariable}],";
            }

            consulta = consulta.TrimEnd(new char[] { ',' });
            consulta += ")VALUES(";

            foreach (var propertie in properties)
            {
                nombreVariable = propertie.Name;

                if (nombreVariable != _id)
                {
                    consulta += $"@i{nombreVariable},";

                    parametros.Add($"i{nombreVariable}", valuePairs[nombreVariable]);
                }
            }

            //consulta += string.Join(",", properties.Select(x => x.Name));
            consulta = consulta.TrimEnd(new char[] { ',' });
            consulta += ")";

            Query(consulta, parametros);

            return default(T);
        }

        public virtual T Modificar<T>(int id, Dictionary<string, object> valuePairs) where T : class, new()
        {
            string nombreVariable;

            var properties = GetProperties<T>();

            var consulta = $"UPDATE {_tabla} SET ";

            foreach (var propertie in properties)
            {
                nombreVariable = propertie.Name;
                if (nombreVariable != _id)
                {
                    consulta += $"{nombreVariable}='{valuePairs[nombreVariable]}',";
                }
            }

            consulta = consulta.TrimEnd(new char[] { ',' });

            consulta += $"WHERE {_id}='{id}'";

            Query(consulta, null);

            return default(T);
        }

        public virtual T Eliminar<T>(int id) where T : class, new()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            var consulta = $"DELETE FROM[dbo].[{_tabla}] WHERE {_id}=@Id";

            parametros.Add("@Id", id);

            Query(consulta, parametros);

            return default(T);
        }

        public virtual T TraerPorId<T>(int id) where T : class, new()
        {
            var consulta = "SELECT * FROM " + _tabla + " WHERE " + _id + "=@iId";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("iId", id);

            return QueryObject<T>(consulta, parametros).FirstOrDefault();
        }

        private PropertyInfo[] GetProperties<T>() where T : class, new()
        {
            var type = new T().GetType();

            var properties = type.GetProperties();

            return properties;
        }
    }
}

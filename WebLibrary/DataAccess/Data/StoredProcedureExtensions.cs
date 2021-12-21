using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{

    public  class Parameter
    {
        public  String ParameterName { get; set; }
        public  Object Value { get; set; }
        public  System.Data.DbType DbType { get; set; }
    }

    public static class StoredProcedureExtensions
    {

        public static DbCommand LoadStoredProcedure(this DbContext context, string procedureName)
        {
            DbCommand cmd = context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = procedureName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            return cmd;
        }

        public static DbCommand WithSqlParams(this DbCommand cmd, IEnumerable<Parameter> nameValues)
        {
            foreach (var pair in nameValues)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = pair.ParameterName;
                param.Value = pair.Value ?? DBNull.Value;
                param.DbType = pair.DbType;
                cmd.Parameters.Add(param);
            }
            return cmd;
        }


        public static IList<T> ExecuteStoredProcedure<T>(this DbCommand command)
            where T : class
        {
            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();
                try
                {
                    using (var reader = command.ExecuteReader())
                    {
                        return reader.MapToList<T>();
                    }
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        private static IList<T> MapToList<T>(this DbDataReader dr)
        {
            var resultList = new List<T>();
            var props = typeof(T).GetRuntimeProperties();

            var colMapping = dr.GetColumnSchema()
                .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
                .ToDictionary(key => key.ColumnName.ToLower());
            if (dr.HasRows)
            {

                while(dr.Read())
                {
                    T item = Activator.CreateInstance<T>();
                    foreach (var prop in props)
                    {
                        var value = dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                        prop.SetValue(item, value == DBNull.Value ? null : value);
                    }
                    resultList.Add(item);
                }
            }
            return resultList;
        }

    }
}

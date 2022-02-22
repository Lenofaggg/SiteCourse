using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace WebApplication2.Controllers
{
    public class QueryExc
    {
        SqlConnection connection = 
            new SqlConnection("Data Source=localhost;Initial Catalog=autopark;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
        
        public object SelectorQuery(string queryStr)
        {          
                if (queryStr.StartsWith("SELECT"))
                {
                    return Select(new SqlCommand(queryStr, connection));
                }
                else
                {
                    return InsUpdDel(new SqlCommand(queryStr, connection));
                } 
        }

        public object Select(SqlCommand cmd)
        {
            using ( connection )
            {
                connection.Open();
                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        var model = Read(reader).ToList();
                        connection.Close();
                        return model;
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    return String.Format("Возникло исключение: {0}", ex.Message);
                }
            }
        }
        private static IEnumerable<object[]> Read(DbDataReader reader)
        {
            while (reader.Read())
            {
                var values = new List<object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader[i]);
                }
                yield return values.ToArray();
            }
        }

        public object InsUpdDel(SqlCommand cmd)
        {
            using (connection)
            {
                connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    connection.Close();

                }
                catch(Exception ex)
                {
                    connection.Close();
                    return (String.Format("Возникло исключение: {0}", ex.Message));
                }
            }
            return ("Изменения применены");
        }
    }
}
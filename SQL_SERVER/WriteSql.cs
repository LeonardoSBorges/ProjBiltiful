using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_SERVER
{
    public class WriteSql
    {
        private SqlConnection _connection = new ConnectionSQL().Connection_SQL_Server();

        public void PushNewRegister(string newRegister)
        {
            using (_connection)
            {
                _connection.Open();
                SqlCommand conn = new SqlCommand(newRegister, _connection);
                conn.ExecuteNonQuery();
                _connection.Close();
            }
        }
        public void UpdateRegister(string newRegister)
        {
            using (_connection)
            {
                _connection.Open();
                SqlCommand conn = new SqlCommand(newRegister, _connection);
                conn.ExecuteNonQuery();
                _connection.Close();
            }
        }
        public string SearchData(string sqlCommand)
        {
            string getValue = "";
            _connection.Open();
            using (SqlCommand conn = new SqlCommand(sqlCommand, _connection))
            {
                using (SqlDataReader reader = conn.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        getValue = $"{reader.GetValue(0)}, {reader.GetValue(1)}, {reader.GetValue(2)}, {reader.GetValue(3)}, {reader.GetValue(4)}, {reader.GetValue(5)}, {reader.GetValue(6)}";
                    }
                }
            }
            _connection.Close();
            return getValue;
        }
        
    }
}

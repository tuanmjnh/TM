using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Connection
{
    public class SQLServer
    {
        private string _connectionString = "MainContext";
        public SqlConnection Connection;
        //public SQLServer()
        //{
        //    Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[_connectionString].ToString());
        //    Con.Open();
        //}
        public SQLServer(bool ConfigurationManager = true)
        {
            Connection = new SqlConnection(ConfigurationManager ? System.Configuration.ConfigurationManager.ConnectionStrings[_connectionString].ToString() : _connectionString);
            Connection.Open();
        }
        public SQLServer(string ConnectionString, bool ConfigurationManager = true)
        {
            _connectionString = ConnectionString;
            Connection = new SqlConnection(ConfigurationManager ? System.Configuration.ConfigurationManager.ConnectionStrings[_connectionString].ToString() : _connectionString);
            Connection.Open();
        }
        public void Close()
        {
            try
            {
                if (Connection != null && Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            catch (Exception) { throw; }
        }
    }
}

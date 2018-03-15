using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Connection
{
    public class Oracle
    {
        private string _connectionString = "HNIVNPTBACKAN1";
        public OracleConnection Connection;
        //public SQLServer()
        //{
        //    Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[_connectionString].ToString());
        //    Con.Open();
        //}
        public Oracle(bool ConfigurationManager = true)
        {
            Connection = new OracleConnection(ConfigurationManager ? System.Configuration.ConfigurationManager.ConnectionStrings[_connectionString].ToString() : _connectionString);
            Connection.Open();
        }
        public Oracle(string ConnectionString, bool ConfigurationManager = true)
        {
            _connectionString = ConnectionString;
            Connection = new OracleConnection(ConfigurationManager ? System.Configuration.ConfigurationManager.ConnectionStrings[_connectionString].ToString() : _connectionString);
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
    public class InstanceOracle
    {
        public static OracleConnection OracleHNI
        {
            get
            {
                var conInf = new TM.Oracle.ConnectionServerInf();
                conInf.UserId = "VNPTBACKAN1";
                conInf.Password = "abc123";
                conInf.Host = "10.10.20.15";
                conInf.Port = "1521";
                conInf.Service_Name = "VNPSYN";
                return TM.Oracle.Extras.OracleConnection(conInf);
            }
        }
        public static OracleConnection OracleTMVN
        {
            get
            {
                var conInf = new TM.Oracle.ConnectionServerInf();
                conInf.UserId = "tmvn";
                conInf.Password = "tmvnpt";
                conInf.Host = "10.17.20.99";
                conInf.Port = "1521";
                conInf.Service_Name = "vnpt";
                return TM.Oracle.Extras.OracleConnection(conInf);
            }
        }
        public static OracleConnection OracleVNPT
        {
            get
            {
                var conInf = new TM.Oracle.ConnectionServerInf();
                conInf.UserId = "vnpt";
                conInf.Password = "tmvnpt";
                conInf.Host = "10.17.20.99";
                conInf.Port = "1521";
                conInf.Service_Name = "vnpt";
                return TM.Oracle.Extras.OracleConnection(conInf);
            }
        }
        public static OracleConnection OracleCuoc
        {
            get
            {
                return TM.Oracle.Extras.OracleConnection(new TM.Oracle.ConnectionServerInf()
                {
                    UserId = "cuoc",
                    Password = "tmvnpt",
                    Host = "10.17.20.99",
                    Port = "1521",
                    Service_Name = "vnpt"
                });
            }
        }
    }
}
namespace TM.Oracle
{
    public class Extras
    {
        private string _UserId, _Password, _DataSource, _Host, _Port, _Service_Name;
        public Extras() { }
        public static OracleConnection OracleConnection(ConnectionInf conInf)
        {
            string _con = $"User Id={conInf.UserId};Password={conInf.Password};Data Source={conInf.DataSource};";
            OracleConnection conn = new OracleConnection(_con);  // C#
            if (conn.State == ConnectionState.Open) conn.Close();
            conn.Open();
            return conn;
        }
        public static OracleConnection OracleConnection(ConnectionServerInf conSVInf)
        {
            string _con = $"User Id={conSVInf.UserId};Password={conSVInf.Password};Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={conSVInf.Host})(PORT={conSVInf.Port}))(CONNECT_DATA=(SERVICE_NAME={conSVInf.Service_Name})));";
            OracleConnection conn = new OracleConnection(_con);  // C#
            if (conn.State == ConnectionState.Open) conn.Close();
            conn.Open();
            return conn;
        }
        public Extras(ConnectionInf conInf)
        {
            _UserId = conInf.UserId;
            _Password = conInf.Password;
            _DataSource = conInf.DataSource;
        }
        public Extras(ConnectionServerInf conInf)
        {
            _UserId = conInf.UserId;
            _Password = conInf.Password;
            _Host = conInf.Host;
            _Port = conInf.Port;
            _Service_Name = conInf.Service_Name;
        }
        public OracleConnection OracleConnection(bool server = false)
        {
            string _con = !server ?
                $"User Id={_UserId};Password={_Password};Data Source={_DataSource};" :
                $"User Id={_UserId};Password={_Password};Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={_Host})(PORT={_Port}))(CONNECT_DATA=(SERVICE_NAME={_Service_Name})));";
            OracleConnection conn = new OracleConnection(_con);  // C#
            conn.Open();
            return conn;
        }
    }
    public class ConnectionInf
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string DataSource { get; set; }
    }
    public class ConnectionServerInf
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Service_Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace TM.Connection
{
    public class Oracle
    {
        public static OracleConnection OracleHNI
        {
            get
            {
                var conInf = new TM.DataAcess.ConnectionServerInf();
                conInf.UserId = "VNPTBACKAN1";
                conInf.Password = "abc123";
                conInf.Host = "10.10.20.15";
                conInf.Port = "1521";
                conInf.Service_Name = "VNPSYN";
                return TM.DataAcess.Oracle.OracleConnection(conInf);
            }
        }
        public static OracleConnection OracleTMVN
        {
            get
            {
                var conInf = new TM.DataAcess.ConnectionServerInf();
                conInf.UserId = "tmvn";
                conInf.Password = "tmvnpt";
                conInf.Host = "10.17.20.99";
                conInf.Port = "1521";
                conInf.Service_Name = "vnpt";
                return TM.DataAcess.Oracle.OracleConnection(conInf);
            }
        }
        public static OracleConnection OracleVNPT
        {
            get
            {
                var conInf = new TM.DataAcess.ConnectionServerInf();
                conInf.UserId = "vnpt";
                conInf.Password = "tmvnpt";
                conInf.Host = "10.17.20.99";
                conInf.Port = "1521";
                conInf.Service_Name = "vnpt";
                return TM.DataAcess.Oracle.OracleConnection(conInf);
            }
        }
        public static OracleConnection OracleCuoc
        {
            get
            {
                return TM.DataAcess.Oracle.OracleConnection(new TM.DataAcess.ConnectionServerInf()
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

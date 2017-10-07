using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
//using System.Data.OracleClient;

namespace TM.SQL
{
    //This is collection and modify by tuanmjnh - TM
    public class DBStatic
    {
        public static string ConstantConnectionString = "MainContext";
        //public static string _Connection { get { return ConstantConnectionString; } set { ConstantConnectionString = value; } }
        public static SqlConnection Connection(string ConnectionString)
        {
            try
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionString].ToString());
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();
                return con;
            }
            catch (Exception) { return null; }
        }
        public static SqlConnection Connection()
        {
            return Connection(ConstantConnectionString);
        }

        public static void ConnectionOpen()
        {
            if (Connection() != null && Connection().State == ConnectionState.Closed) Connection().Open();
        }
        public static void ConnectionClose()
        {
            if (Connection() != null && Connection().State == ConnectionState.Open) { Connection().Close(); }//Connection().Dispose(); } }
        }
        public static void ClearCache()
        {
            System.Collections.Generic.List<string> keyList = new System.Collections.Generic.List<string>();
            System.Collections.IDictionaryEnumerator CacheEnum = System.Web.HttpContext.Current.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
                keyList.Add(CacheEnum.Key.ToString());
            foreach (string key in keyList)
                System.Web.HttpContext.Current.Cache.Remove(key);
        }
        #region DB Access Functions
        public SqlCommand getCommand(string query)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        return cmd;
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetCommand", ex); }
            finally { }
        }
        public static DataTable ToDataTable(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (cmd.Connection = con)
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataTableCommand", ex); }
            finally { }
        }
        public static DataTable ToDataTable(string sql)
        {
            try
            {
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                cmd.CommandTimeout = 0;
                                da.Fill(dt);
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataTableQuery", ex); }
            finally { }
        }
        public static DataTable ToDataTable(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataTableStoredProcedureName", ex); }
            finally { }
        }
        public static DataSet ToDataSet(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    cmd.Connection = con;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataTableQuery", ex); }
            finally { }
        }
        public static DataSet ToDataSet(string sql)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataSetQuery", ex); }
            finally { }
        }
        public static DataSet ToDataSet(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataSetStoredProcedureName", ex); }
            finally { }
        }
        public static bool Execute(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public static bool Execute(string sql, string extraEX)
        {
            try
            {
                extraEX = (extraEX != null || !String.IsNullOrWhiteSpace(extraEX)) ? extraEX + ": " : "";
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex) { throw new Exception(extraEX + ex.Message); }
            finally { }
        }
        public static bool Execute(string sql)
        {
            return Execute(sql, "");
        }
        public static bool ExecuteNonQuery(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public static bool Execute(string[] StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            //ClearCache();
            //Connection().Open();
            using (SqlConnection con = Connection())
            {
                using (SqlTransaction tran = con.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = con;
                            cmd.Transaction = tran;
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (ArrayParam != null)
                                foreach (SqlParameter param in ArrayParam)
                                    cmd.Parameters.Add(param);
                            for (int i = 0; i < StoredProcedureName.Length; i++)
                                cmd.CommandText = StoredProcedureName[i];
                            cmd.ExecuteNonQuery();
                            tran.Commit();
                            return true;
                        }
                    }
                    catch (Exception ex) { tran.Rollback(); throw new Exception(ex.Message); }
                    finally { }
                }
            }
        }
        public static int ExecuteScalar(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    cmd.Connection = con;
                    var rs = cmd.ExecuteScalar();
                    return rs != null ? Convert.ToInt32(rs) : 0;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public static int ExecuteScalar(string sql)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;
                        var rs = cmd.ExecuteScalar();
                        return rs != null ? Convert.ToInt32(rs) : 0;
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public static int ExecuteScalar(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                //ClearCache();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        var rs = cmd.ExecuteScalar();
                        return rs != null ? Convert.ToInt32(rs) : 0;
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public static int ExecuteScalar(string[] StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            //ClearCache();
            //Connection().Open();
            using (SqlConnection con = Connection())
            {
                using (SqlTransaction tran = con.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = con;
                            cmd.Transaction = tran;
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (ArrayParam != null)
                                foreach (SqlParameter param in ArrayParam)
                                    cmd.Parameters.Add(param);
                            for (int i = 0; i < StoredProcedureName.Length; i++)
                                cmd.CommandText = StoredProcedureName[i];
                            var rs = cmd.ExecuteScalar();
                            return rs != null ? Convert.ToInt32(rs) : 0;
                        }
                    }
                    catch (Exception ex) { throw new Exception(ex.Message); }
                    finally { }
                }
            }
        }
        public static string ExecuteScalarStr(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    cmd.Connection = con;
                    var rs = cmd.ExecuteScalar();
                    return rs != null ? rs.ToString() : string.Empty;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public static string ExecuteScalarStr(string sql)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;
                        var rs = cmd.ExecuteScalar();
                        return rs != null ? rs.ToString() : string.Empty;
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public static string ExecuteScalarStr(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        var rs = cmd.ExecuteScalar();
                        return rs != null ? rs.ToString() : string.Empty;
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public static SqlDataReader GetDataReader(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    cmd.Connection = con;
                    using (SqlDataReader datareader = cmd.ExecuteReader())
                    {
                        return cmd.ExecuteReader();
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public static SqlDataReader ToDataReader(string sql)
        {
            try
            {
                //ClearCache();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataReader datareader = cmd.ExecuteReader())
                        {
                            return cmd.ExecuteReader();
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public static SqlDataReader ToDataReader(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        using (SqlDataReader datareader = cmd.ExecuteReader())
                        {
                            return cmd.ExecuteReader();
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        #endregion
    }
    public class DB
    {
        private string ConnectionString = "MainContext";
        //public static string _Connection { get { return ConstantConnectionString; } set { ConstantConnectionString = value; } }
        public DB(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        public DB()
        {

        }
        public SqlConnection Connection()
        {
            try
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionString].ToString());
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();
                return con;
            }
            catch (Exception) { return null; }
        }
        public void ConnectionOpen()
        {
            if (Connection() != null && Connection().State == ConnectionState.Closed) Connection().Open();
        }
        public void ConnectionClose()
        {
            if (Connection() != null && Connection().State == ConnectionState.Open) { Connection().Close(); }//Connection().Dispose(); } }
        }
        public void ClearCache()
        {
            System.Collections.Generic.List<string> keyList = new System.Collections.Generic.List<string>();
            System.Collections.IDictionaryEnumerator CacheEnum = System.Web.HttpContext.Current.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
                keyList.Add(CacheEnum.Key.ToString());
            foreach (string key in keyList)
                System.Web.HttpContext.Current.Cache.Remove(key);
        }
        #region DB Access Functions
        public SqlCommand getCommand(string query)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        return cmd;
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetCommand", ex); }
            finally { }
        }
        public DataTable ToDataTable(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (cmd.Connection = con)
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataTableCommand", ex); }
            finally { }
        }
        public DataTable ToDataTable(string sql)
        {
            try
            {
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                cmd.CommandTimeout = 0;
                                da.Fill(dt);
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataTableQuery", ex); }
            finally { }
        }
        public DataTable ToDataTable(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataTableStoredProcedureName", ex); }
            finally { }
        }
        public DataSet ToDataSet(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    cmd.Connection = con;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataTableQuery", ex); }
            finally { }
        }
        public DataSet ToDataSet(string sql)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataSetQuery", ex); }
            finally { }
        }
        public DataSet ToDataSet(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataSetStoredProcedureName", ex); }
            finally { }
        }
        public bool Execute(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool Execute(string sql, string extraEX)
        {
            try
            {
                extraEX = (extraEX != null || !String.IsNullOrWhiteSpace(extraEX)) ? extraEX + ": " : "";
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex) { throw new Exception(extraEX + ex.Message); }
            finally { ConnectionClose(); }
        }
        public bool Execute(string sql)
        {
            return Execute(sql, "");
        }
        public bool ExecuteNonQuery(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool Execute(string[] StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            //ClearCache();
            //Connection().Open();
            using (SqlConnection con = Connection())
            {
                using (SqlTransaction tran = con.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = con;
                            cmd.Transaction = tran;
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (ArrayParam != null)
                                foreach (SqlParameter param in ArrayParam)
                                    cmd.Parameters.Add(param);
                            for (int i = 0; i < StoredProcedureName.Length; i++)
                                cmd.CommandText = StoredProcedureName[i];
                            cmd.ExecuteNonQuery();
                            tran.Commit();
                            return true;
                        }
                    }
                    catch (Exception ex) { tran.Rollback(); throw new Exception(ex.Message); }
                    finally { }
                }
            }
        }
        public int ExecuteScalar(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    cmd.Connection = con;
                    var rs = cmd.ExecuteScalar();
                    return rs != null ? Convert.ToInt32(rs) : 0;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public int ExecuteScalar(string sql)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;
                        var rs = cmd.ExecuteScalar();
                        return rs != null ? Convert.ToInt32(rs) : 0;
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public int ExecuteScalar(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                //ClearCache();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        var rs = cmd.ExecuteScalar();
                        return rs != null ? Convert.ToInt32(rs) : 0;
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public int ExecuteScalar(string[] StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            //ClearCache();
            //Connection().Open();
            using (SqlConnection con = Connection())
            {
                using (SqlTransaction tran = con.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = con;
                            cmd.Transaction = tran;
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (ArrayParam != null)
                                foreach (SqlParameter param in ArrayParam)
                                    cmd.Parameters.Add(param);
                            for (int i = 0; i < StoredProcedureName.Length; i++)
                                cmd.CommandText = StoredProcedureName[i];
                            var rs = cmd.ExecuteScalar();
                            return rs != null ? Convert.ToInt32(rs) : 0;
                        }
                    }
                    catch (Exception ex) { throw new Exception(ex.Message); }
                    finally { }
                }
            }
        }
        public string ExecuteScalarStr(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    cmd.Connection = con;
                    var rs = cmd.ExecuteScalar();
                    return rs != null ? rs.ToString() : string.Empty;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public string ExecuteScalarStr(string sql)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;
                        var rs = cmd.ExecuteScalar();
                        return rs != null ? rs.ToString() : string.Empty;
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public string ExecuteScalarStr(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        var rs = cmd.ExecuteScalar();
                        return rs != null ? rs.ToString() : string.Empty;
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public SqlDataReader GetDataReader(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    cmd.Connection = con;
                    using (SqlDataReader datareader = cmd.ExecuteReader())
                    {
                        return cmd.ExecuteReader();
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public SqlDataReader ToDataReader(string sql)
        {
            try
            {
                //ClearCache();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataReader datareader = cmd.ExecuteReader())
                        {
                            return cmd.ExecuteReader();
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public SqlDataReader ToDataReader(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        using (SqlDataReader datareader = cmd.ExecuteReader())
                        {
                            return cmd.ExecuteReader();
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        #endregion
    }
}
namespace TM.SQLServer
{
    public class Backup
    {
        private string _database, _path;
        private readonly string _connectionString;
        private readonly string[] _systemDatabaseNames = { "master", "tempdb", "model", "msdb" };
        //public Backup(string database, string path = @"D:\", string connectionString = "MainContext")
        //{
        //    _connectionString = connectionString;
        //    _database = database;
        //    _fileName = database + ".bak";
        //    _path = path;
        //}
        public Backup(string path = @"D:\", string connectionString = "MainContext")
        {
            _connectionString = connectionString;
            _path = path;
        }
        public bool Backing(string database)
        {
            try
            {
                var localServer = new Microsoft.SqlServer.Management.Smo.Server();
                var bkpDBFullWithCompression = new Microsoft.SqlServer.Management.Smo.Backup();
                /* Specify whether you want to back up database or files or log */
                bkpDBFullWithCompression.Action = Microsoft.SqlServer.Management.Smo.BackupActionType.Database;
                /* Specify the name of the database to back up */
                bkpDBFullWithCompression.Database = database;
                /* You can use back up compression technique of SQL Server 2008,
                 * specify CompressionOption property to On for compressed backup */
                bkpDBFullWithCompression.CompressionOption = Microsoft.SqlServer.Management.Smo.BackupCompressionOptions.On;
                bkpDBFullWithCompression.Devices.AddDevice(_path + database + "_" + DateTime.Now.ToString("yyyyMMdd") + ".bak", Microsoft.SqlServer.Management.Smo.DeviceType.File);
                bkpDBFullWithCompression.BackupSetName = database + " database Backup - Compressed";
                bkpDBFullWithCompression.BackupSetDescription = database + " database - Full Backup with Compressin - only in SQL Server 2008";
                bkpDBFullWithCompression.SqlBackup(localServer);
                return true;
            }
            catch (Exception) { throw; }
        }
        public bool BackingAll()
        {
            try
            {
                foreach (string databaseName in GetAllUserDatabases())
                    Backing(databaseName);
                return true;
            }
            catch (Exception) { throw; }
        }
        public System.Collections.Generic.IEnumerable<string> GetAllUserDatabases()
        {
            var databases = new System.Collections.Generic.List<String>();
            DataTable databasesTable;
            using (var connection = TM.SQL.DBStatic.Connection(_connectionString))
            {
                databasesTable = connection.GetSchema("Databases");
                connection.Close();
            }
            foreach (DataRow row in databasesTable.Rows)
            {
                string databaseName = row["database_name"].ToString();
                if (_systemDatabaseNames.Contains(databaseName)) continue;
                databases.Add(databaseName);
            }
            return databases;
        }


        public void BackupDatabase(string databaseName)
        {
            string filePath = BuildBackupPathWithFilename(databaseName);
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = String.Format("BACKUP DATABASE [{0}] TO DISK='{1}'", databaseName, filePath);
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        private string BuildBackupPathWithFilename(string databaseName)
        {
            string filename = string.Format("{0}-{1}.bak", databaseName, DateTime.Now.ToString("yyyy-MM-dd"));
            return System.IO.Path.Combine(_path, filename);
        }
    }
}
namespace TM.DataAcess
{
    public class Oracle
    {
        private string _UserId, _Password, _DataSource, _Host, _Port, _Service_Name;
        public Oracle() { }
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
        public Oracle(ConnectionInf conInf)
        {
            _UserId = conInf.UserId;
            _Password = conInf.Password;
            _DataSource = conInf.DataSource;
        }
        public Oracle(ConnectionServerInf conInf)
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
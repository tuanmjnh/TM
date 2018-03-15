using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Helper;
using Dapper;

namespace TM.Dapper
{
    public static class CRUDOracle
    {
        const string _BEGIN = "BEGIN\n";
        const string _END = "\nEND;";
        public static string InsertQry<T>(T entity)
        {
            var str = $"INSERT INTO {entity.GetClassName()}(";
            foreach (var i in entity.GetProperties())
                str += $"{i},";
            str = $"{str.TrimEnd(',')}) VALUES(";
            foreach (var i in entity.GetType().GetProperties())
            {
                if (i.PropertyType.Name.ToLower() == "datetime")
                    str += i.GetValue(entity, null).ToString().ParseDateTime();// "TO_DATE('" + DateTime.Parse(i.GetValue(entity, null).ToString()).ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD HH24:mi:ss')";
                else if (i.PropertyType.Name.ToLower() != "string")
                    str += i.GetValue(entity, null);
                else
                    str += $"'{(string.IsNullOrEmpty(i.GetValue(entity, null).ToString()) ? i.GetValue(entity, null).ToString().Trim() : "")}'";
                str += ",";
            }
            return $"{str.TrimEnd(',')})";
        }
        public static dynamic Insert<T>(this System.Data.IDbConnection connection, T entity, System.Data.IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            try
            {
                connection.Query(InsertQry(entity));
                return 1;
            }
            catch (Exception) { throw; }
        }
        public static dynamic InsertList<T>(this System.Data.IDbConnection connection, IEnumerable<T> entity, int? jump = 500, System.Data.IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            try
            {
                //var types = entity.GetType().Name== "List`1";
                var index = 0;
                var qry = _BEGIN;
                foreach (var i in entity)
                {
                    qry += InsertQry(i) + ";";
                    index++;
                    if (index % jump == 0)
                    {
                        qry += _END;
                        connection.Query(qry);
                        qry = _BEGIN;
                    }
                }
                if (index % jump > 0)
                {
                    qry += _END;
                    connection.Query(qry);
                }
            }
            catch (Exception) { throw; }
            return 1;
        }
        public static string UpdateQryProperty<T>(T entity, System.Reflection.PropertyInfo i)
        {
            var str = "";
            if (i.PropertyType.Name.ToLower() == "datetime")
                str += $"{i.Name}='{i.GetValue(entity, null).ToString().ParseDateTime()}',";
            else if (i.PropertyType.Name.ToLower() != "string")
                str += $"{i.Name}={i.GetValue(entity, null)},";
            else
                str += $"{i.Name}='{(string.IsNullOrEmpty(i.GetValue(entity, null).ToString()) ? i.GetValue(entity, null).ToString().Trim() : "")}',";
            return str;
        }
        public static string UpdateQry<T>(T entity)
        {
            var str = $"UPDATE {entity.GetClassName()} SET ";
            var keyProperty = new string[2];
            var strID = "WHERE ";
            foreach (var i in entity.GetType().GetProperties())
            {
                var attribute = Attribute.GetCustomAttribute(i, typeof(System.ComponentModel.DataAnnotations.KeyAttribute)) as System.ComponentModel.DataAnnotations.KeyAttribute;
                if (attribute != null) // This property has a KeyAttribute
                    strID += UpdateQryProperty(entity, i).TrimEnd(',');
                else
                {
                    str += UpdateQryProperty(entity, i);
                    //if (i.PropertyType.Name.ToLower() == "datetime")
                    //    str += $"{i.Name}='{i.GetValue(entity, null).ToString().ParseDateTime()}',";
                    //else if (i.PropertyType.Name.ToLower() != "string")
                    //    str += $"{i.Name}={i.GetValue(entity, null)},";
                    //else
                    //    str += $"{i.Name}='{i.GetValue(entity, null)}',";
                }
            }
            return str = str.TrimEnd(',') + strID;
        }
        public static dynamic UpdateList<T>(this System.Data.IDbConnection connection, IEnumerable<T> entity, int? jump = 500, System.Data.IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            try
            {

            }
            catch (Exception) { throw; }
            return 1;
        }
        public static string ParseDateTime(this string datetime)
        {
            var rs = $"TO_DATE('{DateTime.Parse(datetime).ToString("yyyy/MM/dd hh:mm:ss")}', 'YYYY/MM/DD HH24:mi:ss')";
            return rs;
        }
    }
}

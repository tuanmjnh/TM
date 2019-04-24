using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Dapper;

// #if NETSTANDARD1_3
// using DataException = System.InvalidOperationException;
// #else
// using System.Threading;
// #endif
namespace Dapper.Contrib.Extensions {
    public static partial class SqlMapperExtensions {
        public static long InsertDynamic(this IDbConnection connection, object dynamicToInsert, string tableName, string keyProperties = null, IDbTransaction transaction = null, int? commandTimeout = null) {
            var isList = false;
            var adapter = GetFormatter(connection);
            var wasClosed = connection.State == ConnectionState.Closed;
            if (wasClosed) connection.Open();
            int returnVal = 0;
            //
            var entityToInsert = new List<ICollection<KeyValuePair<string, Object>> > ();
            var _entityToInsert = new List<System.Dynamic.ExpandoObject>();
            if (dynamicToInsert is Array)
                isList = true;
            else if (dynamicToInsert is List<System.Dynamic.ExpandoObject>) {
                isList = true;
                _entityToInsert = (List<System.Dynamic.ExpandoObject>) dynamicToInsert;
                //
                if (_entityToInsert.Count < 1) return -1;
                //DapperRow
                //
                var index = 0;
                foreach (var item in _entityToInsert) {
                    entityToInsert.Add((ICollection<KeyValuePair<string, Object>>) item);
                    index++;
                    returnVal++;
                }
            } else if (dynamicToInsert is IEnumerable<dynamic>) {
                //const string _BEGIN = "begin\n";
                //const string _END = "\nend;";
                //var qry = "";
                foreach (var item in (IEnumerable<dynamic>) dynamicToInsert) {
                    var x = new List<KeyValuePair<string, Object>>();
                    foreach (KeyValuePair<string, object> property in item) {
                        //if (property.Value == null) continue;
                        x.Add(new KeyValuePair<string, Object>(property.Key, property.Value));
                        //var x = property.Key;
                        //var y = property.Value;
                    }
                    entityToInsert.Add(x);
                }
                // foreach (var item in (IEnumerable<dynamic>) dynamicToInsert) {
                //     //qry += getInsertQuery(tableName, item);
                //     string a = $"{_BEGIN}{getInsertQuery(tableName, item)}{_END}";
                //     connection.Execute(a);
                //     returnVal++;
                // }
                // qry = $"{_BEGIN}{qry}{_END}";
                // return returnVal;
            } else {
                entityToInsert.Add((ICollection<KeyValuePair<string, Object>>) dynamicToInsert);
                returnVal++;
            }
            if (isList) { }
            var cmd = getCmdInsert(adapter, entityToInsert[0], tableName, keyProperties);
            connection.Execute(cmd, entityToInsert, transaction, commandTimeout);
            if (wasClosed) connection.Close();
            return returnVal;
        }
        public static string getCmdInsert(ISqlAdapter adapter, ICollection<KeyValuePair<string, Object>> entityToInsert, string tableName, string keyProperties = null) {
            var sbColumnList = new StringBuilder(null);
            var sbParameterList = new StringBuilder(null);
            var index = 0;
            foreach (var item in entityToInsert) {
                var property = (KeyValuePair<string, object>) item;
                //
                if (!string.IsNullOrEmpty(keyProperties) && property.Key.ToUpper() == keyProperties.ToUpper()) continue;
                else index++;
                // Column
                adapter.AppendColumnName(sbColumnList, property.Key);
                if (index < entityToInsert.Count)
                    sbColumnList.Append(",");
                // Parameter
                adapter.AppendParametr(sbParameterList, property.Key);
                if (index < entityToInsert.Count)
                    sbParameterList.Append(",");
            }
            var cmd = $"insert into {tableName} ({sbColumnList.ToString().Trim(',')}) values ({sbParameterList.ToString().Trim(',')})";
            return cmd;
        }
        public static string GetClassName<T>(this T obj) {
            return obj.GetType().Name;
        }
        public static string getInsertQuery(string tableName, IDictionary<string, Object> obj) { //IEnumerable<dynamic> 
            var sbColumnList = "";
            var sbParameterList = "";
            var cmd = "";
            foreach (KeyValuePair<string, object> property in obj) {
                if (property.Value == null) continue;
                // Column
                sbColumnList += $"{property.Key}, ";
                // Parameter
                sbParameterList += $"{mapObj(property.Value)}, ";
            }
            cmd += $"insert into {tableName} ({sbColumnList.Trim().Trim(',')}) values ({sbParameterList.Trim().Trim(',')});";
            // foreach (var i in entity.GetProperties())
            //     str += $"{i},";
            // str = $"{str.TrimEnd(',')}) VALUES(";
            // foreach (var i in entity.GetType().GetProperties()) {
            //     if (i.PropertyType.Name.ToLower() == "datetime")
            //         str += i.GetValue(entity, null) == null ? "null" : i.GetValue(entity, null).ToString().ParseDateTime(); // "TO_DATE('" + DateTime.Parse(i.GetValue(entity, null).ToString()).ToString("yyyy/MM/dd hh:mm:ss") + "','YYYY/MM/DD HH24:mi:ss')";
            //     else if (i.PropertyType.Name.ToLower() != "string")
            //         str += i.GetValue(entity, null);
            //     else
            //         str += $"'{(i.GetValue(entity, null) == null ? "" : i.GetValue(entity, null).ToString().Trim())}'";
            //     str += ",";
            // }
            return cmd;
        }
        public static string mapObj(object obj) {
            var type = obj.GetType().Name.ToLower();
            if (type == "guid")
                return $"'{obj.ToString().Replace("-", "").ToUpper()}'";
            else if (type == "datetime")
                return obj.ToString().ParseDateTime();
            else if (type != "string") {
                return obj.ToString();
            } else
                return $"'{obj}'";
        }
        public static string ParseDateTime(this string datetime) {
            var rs = $"TO_DATE('{DateTime.Parse(datetime).ToString("yyyy/MM/dd hh:mm:ss")}', 'YYYY/MM/DD HH24:mi:ss')";
            return rs;
        }
    }
}
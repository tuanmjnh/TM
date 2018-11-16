using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace TM.Helper
{
    public class TMHelperJson
    {
        string _path = "";
        public TMHelperJson(string path)
        {
            _path = path;
        }
        public T LoadJson<T>()
        {
            try
            {
                using (var r = new StreamReader(_path))
                {
                    var json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public dynamic LoadJson()
        {
            try
            {
                using (var r = new StreamReader(_path))
                {
                    var json = r.ReadToEnd();
                    var items = Newtonsoft.Json.Linq.JObject.Parse(json);
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

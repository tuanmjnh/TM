using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Helper
{
    public static class ConvertCharacter
    {
        private static char[] tcvnchars = {
            'a','µ','¸','¶','·','¹','¨','»','¾','¼',
            '½','Æ','©','Ç','Ê','È','É','Ë','®','e',
            'Ì','Ð','Î','Ï','Ñ','ª','Ò','Õ','Ó','Ô',
            'Ö','i','×','Ý','Ø','Ü','Þ','o','ß','ã',
            'á','â','ä','«','å','è','æ','ç','é','¬',
            'ê','í','ë','ì','î','u','ï','ó','ñ','ò',
            'ô','­','õ','ø','ö','÷','ù','y','ú','ý',
            'û','ü','þ','¡','»','¾','¼','½','Æ','¢',
            'Ç','Ê','È','É','Ë','§','£','Ò','Õ','Ó',
            'Ô','Ö','¤','å','è','æ','ç','é','¥','ê',
            'í','ë','ì','î','¦','õ','ø','ö','÷','ù',
            'y','ú','ý','û','ü','þ','i','×','Ý','Ø',
            'Ü','Þ'
        };//'í'

        private static char[] unichars = {
            'a','à','á','ả','ã','ạ','ă','ằ','ắ','ẳ',
            'ẵ','ặ','â','ầ','ấ','ẩ','ẫ','ậ','đ','e',
            'è','é','ẻ','ẽ','ẹ','ê','ề','ế','ể','ễ',
            'ệ','i','ì','í','ỉ','ĩ','ị','o','ò','ó',
            'ỏ','õ','ọ','ô','ồ','ố','ổ','ỗ','ộ','ơ',
            'ờ','ớ','ở','ỡ','ợ','u','ù','ú','ủ','ũ',
            'ụ','ư','ừ','ứ','ử','ữ','ự','y','ỳ','ý',
            'ỷ','ỹ','ỵ','Ă','ằ','ắ','ẳ','ẵ','ặ','Â',
            'ầ','ấ','ẩ','ẫ','ậ','Đ','Ê','ề','ế','ể',
            'ễ','ệ','Ô','ồ','ố','ổ','ỗ','ộ','Ơ','ờ',
            'ớ','ở','ỡ','ợ','Ư','ừ','ứ','ử','ữ','ự',
            'Y','ỳ','ý','ỷ','ỹ','ỵ','I','ì','í','ỉ',
            'ĩ','ị'
        };//(Char)769

        private static Dictionary<string, string> tcvnchars_fix = new Dictionary<string, string>() {
            { "¬́" ,"í"},{"©̣","Ë"},{"©́","Ê"},{"í","Ý"},{"ị","Þ"}
        };


        private static char[] convertTable = null;
        private static void SetCharDefault()
        {
            if (convertTable == null)
            {
                convertTable = new char[256];
                for (int i = 0; i < 256; i++)
                    convertTable[i] = (char)i;
            }
        }
        //static void Converter()
        //{
        //    convertTable = new char[256];
        //    for (int i = 0; i < 256; i++)
        //        convertTable[i] = (char)i;
        //    for (int i = 0; i < tcvnchars.Length; i++)
        //        convertTable[tcvnchars[i]] = unichars[i];
        //}
        //public static string TCVN3ToUnicode(this string value)
        //{
        //    convertTable = new char[256];
        //    for (int i = 0; i < 256; i++)
        //        convertTable[i] = (char)i;
        //    for (int i = 0; i < tcvnchars.Length; i++)
        //        convertTable[tcvnchars[i]] = unichars[i];
        //    //
        //    char[] chars = value.ToCharArray();
        //    for (int i = 0; i < chars.Length; i++)
        //        if (chars[i] < (char)256)
        //            chars[i] = convertTable[chars[i]];
        //    return new string(chars);
        //}
        //public static string UnicodeToTCVN3(this string value)
        //{
        //    convertTable = new char[256];
        //    for (int i = 0; i < 256; i++)
        //        convertTable[i] = (char)i;
        //    for (int i = 0; i < unichars.Length; i++)
        //        convertTable[unichars[i]] = tcvnchars[i];
        //    //
        //    char[] chars = value.ToCharArray();
        //    for (int i = 0; i < chars.Length; i++)
        //        if (chars[i] < (char)256)
        //            chars[i] = convertTable[chars[i]];
        //    return new string(chars);
        //}
        public static string TCVN3ToUnicode(this string value)
        {
            try
            {
                char[] chars = value.Trim().ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    var tmp = chars[i];
                    int index = Array.IndexOf(tcvnchars, chars[i]);
                    if (index != -1)
                        chars[i] = unichars[index];
                }
                return new string(chars);
            }
            catch (Exception) { return value; }
        }
        //public static string TCVN3ToUnicode(this string value)
        //{
        //    try
        //    {
        //        var rs = "";
        //        var chars = value.Trim();
        //        for (int i = 0; i < chars.Length; i++)
        //        {
        //            int index = Array.IndexOf(tcvnchars, chars[i].ToString());
        //            if (index != -1)
        //                rs += unichars[index];
        //            else
        //                rs += chars[i].ToString();
        //        }
        //        return rs;
        //    }
        //    catch (Exception) { return value; }
        //}
        public static T TCVN3ToUnicode<T>(this T item)
        {
            var properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var p in properties)
            {
                if (p.PropertyType != typeof(string) || !p.CanWrite || !p.CanRead) { continue; }
                var value = p.GetValue(item) as string;
                if (value != null)
                    p.SetValue(item, value.TCVN3ToUnicode());
            }
            return item;
        }
        public static List<T> TCVN3ToUnicode<T>(this List<T> collection)
        {
            foreach (var item in collection) TCVN3ToUnicode(item);
            return collection;
        }
        public static IEnumerable<T> TCVN3ToUnicode<T>(this IEnumerable<T> collection)
        {
            foreach (var item in collection) TCVN3ToUnicode(item);
            return collection;
        }
        //
        public static string UnicodeToTCVN3(this string value)
        {
            try
            {
                SetCharDefault();
                var chars = value.Trim().ToCharArray();
                var chars_rs = new List<char>();
                var TX = tcvnchars;
                var V = unichars;
                var old_index = -1;
                for (int i = 0; i < chars.Length; i++)
                {
                    var tmp = chars[i];
                    if (Array.IndexOf(convertTable, tmp) == -1)
                        tmp = tmp.ToString().ToLower()[0];
                    if (tmp == 769)
                    {
                        if (old_index != -1) chars_rs[chars_rs.Count - 1] = tcvnchars[old_index + 2];
                        else
                        {
                            int index = Array.IndexOf(unichars, chars_rs[chars_rs.Count - 1].ToString().ToLower()[0]);
                            chars_rs[chars_rs.Count - 1] = tcvnchars[index + 2];
                        }
                            
                    }
                    else if (tmp == 803)
                    {
                        if (old_index != -1) chars_rs[chars_rs.Count - 1] = tcvnchars[old_index + 5];
                        else
                        {
                            int index = Array.IndexOf(unichars, chars_rs[chars_rs.Count - 1].ToString().ToLower()[0]);
                            chars_rs[chars_rs.Count - 1] = tcvnchars[index + 2];
                        }
                    }
                    else
                    {
                        int index = Array.IndexOf(unichars, tmp);
                        if (index != -1)
                        {
                            old_index = index;
                            tmp = tcvnchars[index];
                        }
                        chars_rs.Add(tmp);
                    }
                }
                var rs = "";
                foreach (var i in chars_rs)
                    rs += i;
                return rs;
            }
            catch (Exception) { return value; }
        }
        //public static string UnicodeToTCVN3(this string value)
        //{
        //    try
        //    {
        //        SetCharDefault();
        //        var chars = value.Trim().ToCharArray();
        //        for (int i = 0; i < chars.Length; i++)
        //        {
        //            var tmp = chars[i];
        //            if (Array.IndexOf(convertTable, tmp) == -1)
        //                tmp = tmp.ToString().ToLower()[0];
        //            int index = Array.IndexOf(unichars, tmp);
        //            if (index != -1)
        //                chars[i] = tcvnchars[index];
        //        }
        //        return new string(chars);
        //    }
        //    catch (Exception) { return value; }
        //}
        public static T UnicodeToTCVN3<T>(this T item)
        {
            var properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var p in properties)
            {
                if (p.PropertyType != typeof(string) || !p.CanWrite || !p.CanRead) { continue; }
                var value = p.GetValue(item) as string;
                if (value != null)
                    p.SetValue(item, value.TCVN3ToUnicode());
            }
            return item;
        }
        public static List<T> UnicodeToTCVN3<T>(this List<T> collection)
        {
            foreach (var item in collection) UnicodeToTCVN3(item);
            return collection;
        }
        public static IEnumerable<T> UnicodeToTCVN3<T>(this IEnumerable<T> collection)
        {
            foreach (var item in collection) UnicodeToTCVN3(item);
            return collection;
        }
    }
}

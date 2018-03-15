using System;

namespace TM.Helper
{
    public static class Regex
    {
        public static bool isEmpty(this string s)
        {
            if (s == string.Empty) return true;
            else return false;
        }
        public static bool isNumber(this string s)
        {
            if (!isEmpty(s))
                if (System.Text.RegularExpressions.Regex.IsMatch(s, @"^[0-9]+$")) return true;
                else return false;
            else return false;
        }
        public static bool isDecimal(this string s)
        {
            if (!isEmpty(s))
                if (System.Text.RegularExpressions.Regex.IsMatch(s, @"^[0-9]*\.?[0-9]+$")) return true;
                else return false;
            else return false;
        }
        public static bool isEmail(this string s)
        {
            if (!isEmpty(s))
                if (System.Text.RegularExpressions.Regex.IsMatch(s, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")) return true;
                else return false;
            else return false;
        }
        public static bool isPhone(this string s)
        {
            if (!isEmpty(s))
                if (System.Text.RegularExpressions.Regex.IsMatch(s, @"\d{9,15}")) return true;
                else return false;
            else return false;
        }
        public static bool isMatch(this string s, string x)
        {
            if (!isEmpty(s) && !isEmpty(x))
                if (s == x) return true;
                else return false;
            else return false;
        }
        public static bool isBiger(this int one, int two)
        {
            if (one > two) return true;
            else return false;
        }
        public static bool isSmaller(this int one, int two)
        {
            if (one < two) return true;
            else return false;
        }
        public static bool isBiger(this decimal one, decimal two)
        {
            if (one > two) return true;
            else return false;
        }
        public static bool isSmaller(this decimal one, decimal two)
        {
            if (one < two) return true;
            else return false;
        }
        public static string RemoveWord(this string s)
        {
            try
            {
                return System.Text.RegularExpressions.Regex.Replace(s, @"[a-zA-Z]", "");
            }
            catch (Exception) { return null; }
        }
        public static string RemoveWord(this string s, string chars)
        {
            try
            {
                s = s.Replace(chars, "");
                return s.RemoveWord();
            }
            catch (Exception) { return null; }
        }
        public static string RemoveWord(this string s, string[] chars)
        {
            try
            {
                foreach (var item in chars)
                    s = s.Replace(item, "");
                return s.RemoveWord();
            }
            catch (Exception) { return null; }
        }
        public static int RemoveWordToInt(this string s)
        {
            try
            {
                return int.Parse(System.Text.RegularExpressions.Regex.Replace(s, @"[a-zA-Z]", ""));
            }
            catch (Exception) { return 0; }
        }
        public static string RemoveNumber(this string s)
        {
            try
            {
                return System.Text.RegularExpressions.Regex.Replace(s, @"[\d]", "");
            }
            catch (Exception) { return null; }
        }
    }
}
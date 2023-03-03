using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Common
{
    /// <summary>
    /// string 扩展方法类
    /// </summary>
    public static class StringExtension
    {
        const string numberRegex = "^[0-9]*$";

        const string CharactersRegex = "^[A-Za-z]+$";


        /// <summary>
        /// 判断string是否是整数int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInteger(this string str)
        {
            if (str == null)
                return false;
            return int.TryParse(str, out var result);
        }



        /// <summary>
        /// 是否浮点数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDouble(this string str)
        {
            if (str == null) return false;
            return double.TryParse(str, out var result);
        }

        /// <summary>
        /// 判断string字符串是否是纯数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumber(this string str)
        {
            if (str == null) return false;
            return Regex.IsMatch(str, numberRegex);
        }

        /// <summary>
        /// 判断string字符串是否纯字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsCharacters(this string str)
        {
            if (str == null) return false;
            return Regex.IsMatch(str, CharactersRegex);
        }

    }
}

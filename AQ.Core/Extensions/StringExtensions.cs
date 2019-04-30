using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 首字母转大写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUpperFirstChar(this string str)
        {
            if (string.IsNullOrEmpty(str) || Char.IsUpper(str[0]))
            {
                return str;
            }
            if (str.Length > 1)
            {
                return char.ToUpper(str[0]) + str.Substring(1, str.Length - 1);
            }
            else
            {
                return str.ToUpper();
            }
        }

        /// <summary>
        /// 首字母转小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToLowerFirstChar(this string str)
        {
            if (string.IsNullOrEmpty(str) || Char.IsLower(str[0]))
            {
                return str;
            }
            if (str.Length > 1)
            {
                return char.ToLower(str[0]) + str.Substring(1, str.Length - 1);
            }
            else
            {
                return str.ToLower();
            }
        }


    }
}

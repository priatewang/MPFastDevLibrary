using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Common
{
    /// <summary>
    /// 快速转换类型
    /// </summary>
    public class SuperConvert
    {
        /// <summary>
        /// 使用UTF8编码将byte数组转成字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ConvertToString(byte[] data)
        {
            return Encoding.UTF8.GetString(data, 0, data.Length);
        }



        /// <summary>
        /// 使用指定字符编码将byte数组转成字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ConvertToString(byte[] data, Encoding encoding)
        {
            return encoding.GetString(data, 0, data.Length);
        }


        /// <summary>
        /// byte数组转为16进制两位字符串，默认以“-”分割
        /// </summary>
        /// <param name="data"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string ByteArrayToStringHex(byte[] data,string split="-")
        {
            if (split=="-")
            {
                return BitConverter.ToString(data);
            }
            return BitConverter.ToString(data).Replace("-", split);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static byte[] HexStringToBtyeArray(string data,char split='-')
        {
            var values = data.Split(split);
            byte[] result = new byte[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                result[i] = Convert.ToByte(values[i], 16);
            }
            return result;
        }

    }
}

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
        public static string ByteArrayToStringHex(byte[] data, string split = "-")
        {
            if (split == "-")
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
        public static byte[] HexStringToBtyeArray(string data, char split = '-')
        {
            var values = data.Split(split);
            byte[] result = new byte[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                result[i] = Convert.ToByte(values[i], 16);
            }
            return result;
        }

        /// <summary>
        /// Byte数组转double数组
        /// </summary>
        /// <param name="bytes">传入Byte数组</param>
        /// <param name="length">double数组长度</param>
        /// <param name="index">Byte数组起始位置，默认为0</param>
        /// <param name="round">四舍五入小数点，默认为0不处理</param>
        /// <returns>double数组</returns>
        public static double[] ToDoubleArray(byte[] bytes, int length, int index = 0, int round = 0)
        {
            int typeLen = sizeof(double);
            int len = (bytes.Length - index) / typeLen;
            if (len < length)
            {
                length = len;
            }
            double[] result = new double[len];
            for (int i = 0; i < length; i++)
            {
                result[i] = BitConverter.ToDouble(bytes, index + typeLen * i);
                if (round != 0)
                {
                    result[i] = Math.Round(result[i]);
                }
            }

            return result;
        }

        /// <summary>
        /// double数组转Byte数组
        /// </summary>
        /// <param name="doubles">需要转换的double数组</param>
        /// <returns></returns>
        public static byte[] ToByteArray(double[] doubles)
        {
            int typeLen = sizeof(double);
            byte[] result = new byte[doubles.Length * typeLen];
            for (int i = 0; i < doubles.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(doubles[i]);
                bytes.CopyTo(result, i * typeLen);
            }
            return result;
        }

        /// <summary>
        /// DateTime类转Unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long DateTimeToTimeStamp(DateTime dateTime)
        {
            var ts = dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            return (long)ts.TotalMilliseconds;
        }

        /// <summary>
        /// Unix时间戳转DateTime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime TimeStampToDateTime(long timeStamp)
        {
            var t2 = new DateTime(1970, 1, 1, 8, 0, 0, 0);
            var t3 = t2.AddMilliseconds(timeStamp);
            return t3;
        }
    }
}

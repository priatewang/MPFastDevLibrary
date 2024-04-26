using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MPFastDevLibrary.Serialize
{
    /// <summary>
    /// 序列化辅助类，二进制和xml的序列化和反序列化
    /// </summary>
    public class SerializableHelper
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
        /// 使用UTF8编码将字符串转成byte数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ConvertToByte(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// 使用指定字符编码将字符串转成byte数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] ConvertToByte(string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }

        /// <summary>
        /// 序列化操作，将对象转为二进制文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        public static void ToBinaryFile<T>(T obj, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
            }
        }

        /// <summary>
        /// 反序列化操作，将二进制文件反序列为对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static T FromBinary<T>(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                T obj = (T)bf.Deserialize(fs);
                return obj;
            }
        }

        public static void ToXml<T>(T obj, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(obj.GetType());
                xs.Serialize(fs, obj);
            }
        }

        public static T FromXml<T>(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                XmlSerializer bf = new XmlSerializer(typeof(T));
                T obj = (T)bf.Deserialize(fs);
                return obj;
            }
        }
    }
}

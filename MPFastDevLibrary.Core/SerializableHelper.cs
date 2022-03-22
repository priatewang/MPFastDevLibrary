using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MPFastDevLibrary.Common
{
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
        /// 序列化操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="path"></param>
        public static void SerializeMethod<T>(T list, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, list);
            }
        }

        /// <summary>
        /// 反序列化操作，将二进制文件反序列为对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static T ReserializeMethod<T>(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {

                BinaryFormatter bf = new BinaryFormatter();
                T list = (T)bf.Deserialize(fs);
                return list;
            }
        }

        /// <summary>
        /// 将对象序列化为二进制数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] SerializeToXml(object obj)
        {
            MemoryStream stream = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            xs.Serialize(stream, obj);

            byte[] data = stream.ToArray();
            stream.Close();

            return data;
        }

        /// <summary>
        /// 将XML数据反序列化为指定类型对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T DeserializeWithXml<T>(byte[] data)
        {
            MemoryStream stream = new MemoryStream();
            stream.Write(data, 0, data.Length);
            stream.Position = 0;
            XmlSerializer xs = new XmlSerializer(typeof(T));
            object obj = xs.Deserialize(stream);
            stream.Close();

            return (T)obj;
        }


        /// <summary>
        /// 将XML数据反序列化为指定类型对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T DeserializeWithXml<T>(byte[] data, Type[] extraTypes)
        {
            MemoryStream stream = new MemoryStream();
            stream.Write(data, 0, data.Length);
            stream.Position = 0;
            XmlSerializer xs = new XmlSerializer(typeof(T), extraTypes);
            object obj = xs.Deserialize(stream);
            stream.Close();

            return (T)obj;
        }


        /// <summary>
        /// 将对象序列化为Byte数组
        /// </summary>
        /// <param name="obj">需要序列化的对象</param>
        /// <returns></returns>
        public static byte[] SerializeData(object obj)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, obj);//序列化为二进制流
            return ms.ToArray();
        }

        /// <summary>
        /// 将Byte数组反序列化为指定对象
        /// </summary>
        /// <param name="byt">需要反序列化的数组</param>
        /// <returns></returns>
        public static object DeserializeData(byte[] byt)
        {
            //创建一个作为内存后备存储的MemoryStream流
            MemoryStream ms = new MemoryStream(byt);
            //创建一个以二进制格式序列化和反序列化对象或连接对象的整个图形。
            BinaryFormatter bf = new BinaryFormatter();
            //将接收到的二进制流反序列化为指定对象
            return bf.Deserialize(ms);
        }

    }
}

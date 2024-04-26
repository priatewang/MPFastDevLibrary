/*----------------------------------------------------------------
// 创建时间：2022/5/28 13:53:55
// 开发者： WQ
// 文件名： Serialize
// CLR版本：4.0.30319.42000
// 命名空间：MPFastDevLibrary
// 功能描述：
// 使用说明：
----------------------------------------------------------------*/
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
    /// 序列化
    /// </summary>
    public class Serialize
    {
        #region 二进制

        /// <summary>
        /// 二进制序列化转化为二进制数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] BinarySerialize(object obj)
        {
            if (obj == null)
                return null;
            MemoryStream stream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, obj);
            byte[] data = stream.ToArray();
            stream.Close();
            return data;
        }

        /// <summary>
        /// 反序列化，将二进制数组转为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T BinaryDeserialize<T>(byte[] data)
        {
            if (data.Length == 0)
                return default;
            MemoryStream stream = new MemoryStream();
            stream.Write(data, 0, data.Length);
            BinaryFormatter bf = new BinaryFormatter();
            object obj = bf.Deserialize(stream);
            stream.Close();
            return (T)obj;
        }
        #endregion

        #region XML序列化

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
        /// 将对象序列化为二进制数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] XmlSerialize(object obj)
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
        public static T XmlDeserialize<T>(byte[] data)
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
        /// 将XML数据反序列化为指定类型对象（未指定继承关系自动转化的情况下使用）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="extraTypes">可转化的子类</param>
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

        #endregion
    }
}

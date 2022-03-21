using MPFastDevLibrary.Common;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MP.Common_Example
{
    /// <summary>
    /// 序列化使用示例
    /// </summary>
    public class SerializableExampe
    {

        /// <summary>
        /// 使用xml反序列化 示例
        /// </summary>
        public void UseXmlDeserialize()
        {
            string path = @"XXX.xml";
            var datas = SerializableHelper.ConvertToByte(File.ReadAllText(path), Encoding.Unicode);
            SeriableTest test = SerializableHelper.DeserializeWithXml<SeriableTest>(datas);
        }

        /// <summary>
        /// 将对象序列为xml 示例
        /// </summary>
        public void UseSeriableXml()
        {
            SeriableTest test = new SeriableTest();
            var datas = SerializableHelper.SerializeToXml(test);
            var s = SerializableHelper.ConvertToString(datas);
            File.WriteAllText("XXX.xml", s);
        }


    }


    [Serializable]//序列化标签
    public class SeriableTest
    {
        public string Name { get; set; }

        public string Id { get; set; }


        [XmlIgnore]//序列化和反序列化 忽略
        public int Length { get; set; }
    }
}

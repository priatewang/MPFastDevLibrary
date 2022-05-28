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


    [Serializable]//序列化标签
    public class SeriableTest
    {
        public string Name { get; set; }

        public string Id { get; set; }


        [XmlIgnore]//序列化和反序列化 忽略
        public int Length { get; set; }
    }
}

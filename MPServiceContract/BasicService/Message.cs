using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MPServiceContract.BasicService
{
    [DataContract]
    public class Message
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        [DataMember]
        public int ID { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [DataMember]
        public string Content { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [DataMember]
        public string User { get; set; }

        /// <summary>
        /// 消息生成时间
        /// </summary>
        [DataMember]
        public DateTime Time { get; set; }


    }
}

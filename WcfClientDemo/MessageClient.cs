using MPServiceContract.BasicService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace WcfClientDemo
{
    public class MessageClient : ClientBase<IMessageService>, IMessageService
    {

        /// <summary>
        /// 
        /// </summary>
        public static readonly Binding MyBinding = new NetTcpBinding()
        {
            MaxBufferPoolSize = 2147483647,
            MaxReceivedMessageSize = 2147483647,
            MaxBufferSize = 2147483647,
            CloseTimeout = new TimeSpan(10, 0, 0),
            OpenTimeout = new TimeSpan(0, 0, 10),
            SendTimeout = new TimeSpan(0, 0, 20),
            ReceiveTimeout = new TimeSpan(10, 0, 0),
            Security =
                {
                    Mode = SecurityMode.None
                }
        }; //服务定义一个通道绑定  

        public MessageClient(MessageCallback callback, string remoteaddress)  
            : base(new InstanceContext(callback), MyBinding, new EndpointAddress(remoteaddress))
        {

        }


        public void Register(string clientName)
        {
            Channel.Register(clientName);
        }

        public void SendMessage(MPServiceContract.BasicService.Message message)
        {
            Channel?.SendMessage(message);
        }

        public void SendStringMessage(string msg)
        {
            Channel?.SendStringMessage(msg);
        }
    }
}

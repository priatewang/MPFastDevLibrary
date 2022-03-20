using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace MPServiceController
{
    public class NetServiceHost : IDisposable
    {
        public string Name { get; set; }

        /// <summary>
        /// 服务是否开启
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return _isOpen;
            }
        }

        private bool _isOpen;//是否已打开通道

        /// <summary>
        /// 基地址
        /// </summary>
        public string BaseAddress = "net.tcp://localhost:8080";

        public string OptionalAddress = "talk"; //可选地址 

        private string _serviceAddress = "";

        private ServiceHost _host;//服务对象
        /// <summary>
        /// 服务契约实现类型  
        /// </summary>
        public readonly Type ContractType;
        /// <summary>
        /// 服务契约接口
        /// </summary>
        public readonly Type InterfaceType;


        #region 不同Binding

        /// <summary>
        /// 长连接的绑定
        /// </summary>
        public static readonly Binding LongBinding = new NetTcpBinding()
        {
            MaxBufferPoolSize = 2147483647,
            MaxReceivedMessageSize = 2147483647,
            MaxBufferSize = 2147483647,
            CloseTimeout = new TimeSpan(1, 0, 0),
            OpenTimeout = new TimeSpan(0, 0, 10),
            SendTimeout = new TimeSpan(0, 0, 20),
            ReceiveTimeout = new TimeSpan(20, 0, 0),
            Security =
                {
                    Mode = SecurityMode.None
                }
        }; //服务定义一个通道绑定 

        #endregion


        public NetServiceHost(Type interfaceType, Type contractType)
        {
            InterfaceType = interfaceType;
            ContractType = contractType;

            Name = contractType.Name;
            _host = new ServiceHost(ContractType);
        }

        //public NetServiceHost(string ip, int port, string optionalAddress)
        //{
        //    BaseAddress = string.Format("net.tcp://{0}:{1}", ip, port.ToString());
        //    _host = new ServiceHost(ContractType, new Uri[] { new Uri(BaseAddress) });//创建服务对象
        //    _host.AddServiceEndpoint(InterfaceType, LongBinding, OptionalAddress);//添加终结点  
        //}

        public void AddServiceEndpoint(string ip, int port, string optionalAddress)
        {
            _serviceAddress = string.Format("net.tcp://{0}:{1}", ip, port.ToString());
            _host.AddServiceEndpoint(InterfaceType, LongBinding, new Uri(_serviceAddress));//添加终结点  

        }

        public void Open()
        {
            try
            {
                _host.Open();
                _isOpen = true;
                Console.WriteLine($"服务：{Name},open success！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"服务：{Name},open fail！");
                throw ex;
            }
        }

        public void Close()
        {
            try
            {
                _host.Close();
                _isOpen = false;
                Console.WriteLine($"服务：{Name},close success！");
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public void Dispose()
        {
            if (_host != null)
            {
                (_host as IDisposable).Dispose();
                _isOpen = false;
            }
        }
    }
}

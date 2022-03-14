using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MPServiceContract.BasicService
{
    [ServiceContract(CallbackContract = typeof(IMessageCallback))]
    public interface IMessageService : IService
    {
        [OperationContract]
        void Register(string clientName);

        [OperationContract]
        void SendStringMessage(string msg);

        [OperationContract]
        void SendMessage(Message message);
    }
}

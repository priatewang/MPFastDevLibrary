using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MPServiceContract.BasicService
{
    [ServiceContract]
    public interface IMessageCallback
    {
        [OperationContract]
        void SendStringMessage(string msg);

        [OperationContract]
        void SendMessage(Message message);
    }
}

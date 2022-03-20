using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MPServiceContract.BasicService
{
    public class MessageService : IMessageService, IMeesageEvent
    {
        public event Action<string> StringMessageRecived;

        public event Action<Message> MessageRecived;

        public event Action<string, IMessageCallback> RegisterChanged;

        private string _clientCode;

        public IMessageCallback client;

        public static Dictionary<string, IMessageCallback> Clients = new Dictionary<string, IMessageCallback>();

        public void Register(string clientName)
        {
            _clientCode = clientName;
            IMessageCallback callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
            RegisterChanged?.Invoke(_clientCode, callback);
            OperationContext.Current.Channel.Closed += Channel_Closed;
            Clients.Add(_clientCode, callback);
            client = callback;
            //MyHandler += CommService_MyHandler;
            Console.WriteLine(_clientCode + " connect.");
        }


        public void SendMessage(Message message)
        {
            MessageRecived?.Invoke(message);
        }

        public void SendStringMessage(string msg)
        {
            StringMessageRecived?.Invoke(msg);
        }


        private void Channel_Closed(object sender, EventArgs e)
        {
            Clients.Remove(_clientCode);
            Console.WriteLine(_clientCode + " disconnect.");
        }
    }
}

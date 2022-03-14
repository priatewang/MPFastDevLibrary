using MPServiceContract.BasicService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfClientDemo
{
    public class MessageCallback : IMessageCallback
    {
        public void SendMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public void SendStringMessage(string msg)
        {
            throw new NotImplementedException();
        }
    }
}

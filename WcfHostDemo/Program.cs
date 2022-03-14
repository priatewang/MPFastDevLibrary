using MPServiceContract.BasicService;
using MPServiceController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfHostDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            NetServiceHost netServiceHost = new NetServiceHost(typeof(IMessageService), typeof(MessageService));
            netServiceHost.AddServiceEndpoint("localhost", 12001, "mex");
            netServiceHost.Open();
            ServiceManager.Services.Add(netServiceHost);


            Console.ReadKey();
        }
    }
}

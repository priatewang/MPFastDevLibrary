using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageCallback callback = new MessageCallback();
            MessageClient client = new MessageClient(callback, "net.tcp://localhost:12001");
            client.Register("111");

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakeageTest
{

    public class User
    {
        public IMyService service { get; set; }
        public User(IMyService myService)
        {
            service=myService;
        }

        public void Send()
        {
            Console.WriteLine(" User Send  ok....");
            service.Send("User Service Send");
        }
    }


    public interface IMyService
    {
        void Send(string msg);
    }


    public interface IMyService2
    {
        void Read();
    }

    public class MyService : IMyService
    {
        int num = 0;
        public void Read()
        {
            Console.WriteLine("use num: " + num);
        }

        public void Send(string msg)
        {
            num++;
            Console.WriteLine($" User MyService Send  {msg}....");
        }
    }
}

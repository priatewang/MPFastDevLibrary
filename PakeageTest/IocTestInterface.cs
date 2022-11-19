using ConsoleAppTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakeageTest
{

    public interface ITestService
    {
        void Send(string msg);

    }

    public class AService : ITestService
    {
        public void Send(string msg)
        {
            Console.WriteLine("AService :" + msg);
        }
    }


    public class BService : ITestService
    {
        public void Send(string msg)
        {
            Console.WriteLine("BService :" + msg);
        }
    }


    internal class IocTestInterface
    {
        ITestService _testService;

        public IocTestInterface()
        {
            _testService = Program._testService;
        }

        public IocTestInterface(ITestService testService)
        {
            _testService = testService;
        }


        public void UseService(string mesg)
        {
            Program._testService.Send(mesg);
        }




    }
}

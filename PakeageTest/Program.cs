
using Autofac;
using PakeageTest;
using System;
using System.IO.Compression;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            #region Autofac测试
            var builder = new ContainerBuilder();
            // builder.RegisterType<MyService>().As<IMyService>();
            builder.RegisterType(typeof(User));
            builder.RegisterType(typeof(MyService)).As(typeof(IMyService));
            var container = builder.Build();

            var ims = container.Resolve<IMyService>();
            var user=container.Resolve<User>();

            ims.Send("111");

            user.Send();

            #endregion

            Console.ReadKey();
        }
        static void Write1(object o)
        {
            Console.WriteLine("write1:" + o.ToString());

        }
    }
}



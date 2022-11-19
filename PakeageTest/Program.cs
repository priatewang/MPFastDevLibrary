
using Autofac;
using PakeageTest;
using System;
using System.IO.Compression;
using System.Net.Http.Headers;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            #region Autofac测试
            //var builder = new ContainerBuilder();
            //// builder.RegisterType<MyService>().As<IMyService>();
            //builder.RegisterType(typeof(User));
            //builder.RegisterType(typeof(MyService)).As(typeof(IMyService));
            //var container = builder.Build();

            //var ims = container.Resolve<IMyService>();
            //var user=container.Resolve<User>();

            //ims.Send("111");

            //user.Send();

            #endregion

            //#region Ioc两个实现类切换测试
            //var builder = new ContainerBuilder();
            //builder.RegisterType<AService>().As<ITestService>().SingleInstance();
            //builder.RegisterType<IocTestInterface>();
            //var container = builder.Build();

            //var test = container.Resolve<IocTestInterface>();

            //test.UseService("test 修改前");

            //builder.RegisterType<BService>().As<ITestService>().SingleInstance();
            //var test2 = container.Resolve<IocTestInterface>();
            //test2.UseService("test2 修改后，不修改container");

            //test.UseService("test 修改后,不修改类");

            //var container2 = builder.Build();
            //var test3 = container2.Resolve<IocTestInterface>();
            //test3.UseService("test3 修改后，修改container");

            //#endregion

            #region 静态切换
            IocTestInterface test = new IocTestInterface();
            test.UseService("切换前");

            _testService = new BService();

            test.UseService("切换后");

            #endregion


            Console.ReadKey();
        }

        public static ITestService _testService = new AService();



        static void Write1(object o)
        {
            Console.WriteLine("write1:" + o.ToString());

        }
    }
}



using MP.Common_Example;
using System;
using System.Reflection;
using MPFastDevLibrary.Ioc;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            AttTest attTest = new AttTest();
            var att = typeof(AttTest).GetCustomAttribute<AutoIocAttribute>();

            Console.WriteLine(att.RelationClassType);


            ContainerBuilder builder = new ContainerBuilder();
            builder.AutoRegisterIoc();
            //builder.RegisterType<User>();
            //builder.RegisterType<IMyService, MyService>(InstanceType.AbsoluteSingle);
            //builder.RegisterType<IMyService2, MyService>(InstanceType.AbsoluteSingle);

            IContainer container = builder.Build();

            var user = container.Resolve<User>();
            user.Send();

            var service = container.Resolve<IMyService>();
            service.Send("hahahha");
            service.Send("hahahha2222");

            var service2 = container.Resolve<IMyService2>();
            service2.Read();

            Console.ReadKey();
        }
    }
}

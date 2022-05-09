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
            builder.RegisterType<User>();
            builder.RegisterType<IMyService, MyService>();

            IContainer container = builder.Build();

            var user = container.Resolve<User>();
            user.Send();

            var service = container.Resolve<IMyService>();
            service.Send("hahahha");

            Console.ReadKey();
        }
    }
}

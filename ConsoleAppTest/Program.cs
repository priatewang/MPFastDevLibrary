//#define IOCTEST
#define XLH
using MP.Common_Example;
using System;
using System.Reflection;
using MPFastDevLibrary.Ioc;
using MPFastDevLibrary.Common;


namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            #region IOC测试
#if IOCTEST
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
#endif

            #endregion

            #region 序列化测试
#if XLH

            SeriableTest t = new SeriableTest()
            {
                Id = "12345",
                Length = 5,
                Name = "WQ",
            };
            //SerializableHelper.ToXml(t, "111.xml");
            SerializableHelper.ToBinaryFile(t, "111.binary");
            //Console.WriteLine("out 111.xml");

            var t1 = SerializableHelper.FromBinary<SeriableTest>("111.binary");
            Console.WriteLine($"t1---id:{t1.Id};name:{t1.Name}");
            var t2 = SerializableHelper.FromXml<SeriableTest>("111.xml");
            Console.WriteLine($"t1---id:{t2.Id};name:{t2.Name}");
#endif
            #endregion

            Console.ReadKey();
        }
    }
}

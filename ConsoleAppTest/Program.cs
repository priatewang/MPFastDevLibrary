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
        }
    }
}

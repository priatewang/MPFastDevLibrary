/*----------------------------------------------------------------
// 创建时间：2022/5/6 20:54:16
// 开发者： WQ
// 文件名： User
// CLR版本：4.0.30319.42000
// 命名空间：ConsoleAppTest
// 功能描述：
// 使用说明：
----------------------------------------------------------------*/
using MPFastDevLibrary.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    [AutoIoc]
    public class User
    {

        public void Send()
        {
            Console.WriteLine(" User Send  ok....");
        }
    }


    [AutoIoc(Mode =InstanceType.AbsoluteSingle,RelationClassType =typeof(MyService))]
    public interface IMyService
    {
        void Send(string msg);
    }


    [AutoIoc(Mode =InstanceType.AbsoluteSingle,RelationClassType =typeof(MyService))]
    public interface IMyService2
    {
        void Read();
    }

    public class MyService : IMyService,IMyService2
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

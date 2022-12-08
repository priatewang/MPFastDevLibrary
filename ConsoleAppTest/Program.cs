//#define IOCTEST
//#define XLH
using MP.Common_Example;
using System;
using System.Reflection;
using MPFastDevLibrary.Ioc;
using MPFastDevLibrary.Common;
using MPFastDevLibrary.Mvvm;
using MPFastDevLibrary.EventMessage;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Documents;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Windows.Input.StylusPlugIns;
using System.Diagnostics;

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

            #region 时间戳转换
#if SJC
            //BaseViewModel model = new BaseViewModel();
            //BaseViewModel.SetUINameSapce("");
            var t1 = DateTime.Now.ToUniversalTime();
            Console.WriteLine(t1.ToString());
            var ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long s = (long)ts.TotalMilliseconds;
            Console.WriteLine(s);
            var t2 = DateTime.SpecifyKind(new DateTime(1970, 1, 1, 8, 0, 0, 0), DateTimeKind.Local);
            Console.WriteLine(t2.ToString());
            //var t2 = new DateTime(1970, 1, 1, 8, 0, 0, 0);
          var t3=  t2.AddMilliseconds(s);
            Console.WriteLine(t3.ToString());
#endif
            #endregion
            #region Task测试

#if false

            Messager.Default.Subscribe("w1", Write1);
            Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    Messager.Default.Publish("w1", "task1:" + i);
                }
            });
            Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    Messager.Default.Publish("w1", "task2:" + (i + 1000));
                }
            });
            Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    Messager.Default.Publish("w1", "task3:" + (i * 1000));
                }
            });
#endif

            #endregion
            for (int i = 0; i < 20; i++)
            {
                DeviceValues.Add((10000001 + i).ToString(), false);
            }

            {
                var str = "10000001,[10000002|10000003]";
                SplitString(str);
            }

            Stopwatch sw = new Stopwatch();
            sw.Restart();
            for (int i = 0; i < 10000; i++)
            {
                var str = "10000001,[10000002|10000003],[(10000004,10000005)|(10000006,[(10000007,10000008)|(10000009,10000010)|(10000011,10000012,10000013)|(10000014,10000015,10000016)])]";
                //Console.WriteLine(str); 
                //Console.WriteLine(str);
               
                SplitString(str);
               
            }
            sw.Stop();
            TimeSpan timespan = sw.Elapsed;
            Console.WriteLine("程序耗时:'{0}'ms", timespan.TotalMilliseconds);
           

            //var str = "10000001,[10000002|10000003],[(10000004,10000005)|(10000006,[(10000007,10000008)|(10000009,10000010)|(10000011,10000012,110000012)|(10000013,10000014,10000015)])]";
            //Stopwatch sw = new Stopwatch();
            //sw.Restart();
            //SplitString(str);
            //sw.Stop();
            //TimeSpan timespan = sw.Elapsed;
            //Console.WriteLine("程序耗时:'{0}'ms", timespan.TotalMilliseconds);
            //sw.Restart();
            //SplitString(str);
            //sw.Stop();
            //timespan = sw.Elapsed;
            //Console.WriteLine("程序耗时:'{0}'ms", timespan.TotalMilliseconds);
            Console.ReadKey();
        }

        /// <summary>
        /// 设备开关键值对
        /// </summary>
         static Dictionary<string,bool> DeviceValues=new Dictionary<string,bool>();

        static bool SplitString(string str, char sign = ',')
        {
            bool res = true;
            bool isAnd = sign == ',';
            //正则
            //Regex regex = new Regex(",(?=[^\\)]*(?:\\(|$))");
            //if (!isAnd)
            //{
            //    regex = new Regex(@"\|(?=[^\)]*(?:\(|$))");
            //}
            //var values = regex.Split(str);
            //普通直接拆分
            string[] values;
            if (isAnd)
            {
                values = str.Split(',');
            }
            else
            {
                values = str.Split("|");
            }
            List<string> list = new List<string>();
            //拆分字符串
            string tmp = "";
            for (int i = 0; i < values.Length; i++)
            {
                tmp += values[i];
                if (Regex.IsMatch(tmp, "^[0-9]*$"))
                {
                    list.Add(values[i]);
                    tmp = "";
                    continue;
                }
                if (MidBracketMatch(tmp) == 0 && SmallBracketMatch(tmp) == 0)
                {

                    list.Add(tmp);
                    tmp = "";
                    continue;

                }
                tmp += sign;
            }
            //测试打印
            //Console.WriteLine("进入的字符串: " + str);
            //foreach (var item in list)
            //{
            //    Console.Write(item + "  ");
            //}
            //Console.WriteLine();
            //Console.WriteLine("--------处理完成---------");
            //处理开关值
            for (int i = 0; i < list.Count; i++)
            {
                if (Regex.IsMatch(list[i], "^[0-9]*$"))
                {
                    if (isAnd)
                    {
                        res = res && GetValue(list[i]);

                    }
                    else
                    {
                        res = res || GetValue(list[i]);
                    }
                    continue;
                }
                else
                {
                    var value = list[i].Substring(1, list[i].Length - 2);
                    bool subRes = false;
                    if (list[i][0] == '[')
                    {
                        subRes= SplitString(value,'|');
                    }
                    else
                    {
                        subRes= SplitString(value, ',');
                    }
                    if (isAnd)
                    {
                        res = res && subRes;

                    }
                    else
                    {
                        res = res || subRes;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// 判断中括号是否成对
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static int MidBracketMatch(string str)
        {
            return CharCount(str, '[') - CharCount(str, ']');
            //return str.Count(x => x == '[') - str.Count(x => x == ']');
        }

        /// <summary>
        /// 判断小括号是否成对
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static int SmallBracketMatch(string str)
        {
           // return str.Count(x => x == '(') - str.Count(x => x == ')');
           return CharCount(str,'(')-CharCount(str,')');
        }

        static int CharCount(string str,char c)
        {
           
            return str.Split(c).Length - 1;
        }

        /// <summary>
        /// 获取设备的开关值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static bool GetValue(string id)
        {
            return DeviceValues[id];
        }


        static void Write1(object o)
        {
            Console.WriteLine("write1:" + o.ToString());

        }
    }






}

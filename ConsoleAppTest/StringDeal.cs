using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    public class StringDeal
    {
        /// <summary>
        /// 设备开关键值对
        /// </summary>
        public static Dictionary<string, bool> DeviceValues = new Dictionary<string, bool>();

        public static bool SplitString(string str, char sign = ',')
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
                        subRes = SplitString(value, '|');
                    }
                    else
                    {
                        subRes = SplitString(value, ',');
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
        public static int MidBracketMatch(string str)
        {
            return str.Count(x => x == '[') - str.Count(x => x == ']');
        }

        /// <summary>
        /// 判断小括号是否成对
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int SmallBracketMatch(string str)
        {
            return str.Count(x => x == '(') - str.Count(x => x == ')');
        }

        /// <summary>
        /// 获取设备的开关值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool GetValue(string id)
        {
            return DeviceValues[id];
        }
    }
}

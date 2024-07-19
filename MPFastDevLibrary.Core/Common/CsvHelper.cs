using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.SqlServer.Server;

namespace MPFastDevLibrary.Common
{
    /// <summary>
    /// Csv读写帮助类，支持
    /// </summary>
    public class CsvHelper
    {
        #region Csv跟DataTable转换

        /// <summary>
        /// 读取Csv文件，加载到DataTable
        /// </summary>
        /// <param name="path">csv文件路径</param>
        /// <param name="hasTitle">是否有标题行</param>
        /// <param name="SafeLevel">安全等级：0:错误格式行正常添加；1：错误行忽略（不添加），2：出现错误弹出异常</param>
        /// <returns></returns>
        public static DataTable ReadCsvToDataTable(
            string path,
            bool hasTitle = false,
            int SafeLevel = 0
        )
        {
            DataTable dt = new DataTable();
            var lines = ReadCsv(path, false);
            bool isFirst = true;

            foreach (var item in lines)
            {
                string[] values = item.Split(',');
                if (isFirst)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        dt.Columns.Add();
                    }
                    if (hasTitle)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            dt.Columns[i].ColumnName = values[i];
                        }
                        continue;
                    }
                    isFirst = false;
                }
                if (values.Length == dt.Columns.Count)
                {
                    dt.Rows.Add(values);
                }
                else
                {
                    switch (SafeLevel)
                    {
                        case 0:
                            if (values.Length > dt.Columns.Count)
                            {
                                var res = values.ToList();
                                res.RemoveRange(dt.Columns.Count, values.Length - dt.Columns.Count);
                                dt.Rows.Add(res.ToArray());
                            }
                            else
                            {
                                dt.Rows.Add(values);
                            }
                            break;
                        case 1:
                            continue;
                        default:
                            throw new Exception("CSV格式错误：表格各行列数不一致");
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// 以文件流形式读取Csv文件，加载到DataTable
        /// </summary>
        /// <param name="path">csv文件路径</param>
        /// <param name="hasTitle">是否有标题行</param>
        /// <param name="SafeLevel">安全等级：0:错误格式行正常添加；1：错误行忽略（不添加），2：出现错误弹出异常</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static DataTable ReadCsvByStream(
            string path,
            bool hasTitle = false,
            int SafeLevel = 0
        )
        {
            DataTable dt = new DataTable();
            bool isFirst = true;

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    if (isFirst)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            dt.Columns.Add();
                        }
                        isFirst = false;
                    }

                    //有表头则添加
                    if (hasTitle)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            dt.Columns[i].ColumnName = values[i];
                        }
                        hasTitle = false;
                    }
                    else
                    {
                        if (values.Length == dt.Columns.Count)
                        {
                            dt.Rows.Add(values);
                        }
                        else
                        {
                            switch (SafeLevel)
                            {
                                case 0:
                                    if (values.Length > dt.Columns.Count)
                                    {
                                        var res = values.ToList();
                                        res.RemoveRange(
                                            dt.Columns.Count,
                                            values.Length - dt.Columns.Count
                                        );
                                        dt.Rows.Add(res.ToArray());
                                    }
                                    else
                                    {
                                        dt.Rows.Add(values);
                                    }
                                    break;
                                case 1:
                                    continue;
                                default:
                                    throw new Exception("CSV格式错误：表格各行列数不一致");
                            }
                        }
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// 以文件流形式将DataTable写入csv文件
        /// </summary>
        /// <param name="dt">DataTable对象</param>
        /// <param name="path">文件路径</param>
        /// <param name="hasTitle">是否有标题行</param>
        /// <returns></returns>
        public static bool WriteToCsvByDataTable(DataTable dt, string path, bool hasTitle = false)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                //输出标题行（如果有）
                if (hasTitle)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sw.Write(dt.Columns[i].ColumnName);
                        if (i != dt.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.WriteLine();
                }

                //输出文件内容
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sw.Write(dt.Rows[i][j].ToString());
                        if (j != dt.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.WriteLine();
                }
            }

            return true;
        }

        #endregion


        #region Csv文件转换类集合对象

        /// <summary>
        /// 读取Csv，返回对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static List<T> ReadCsv<T>(string path)
            where T : class, new()
        {
            //读取Csv，返回对象集合,通过表头跟属性名称匹配，使用反射实现
            var result = new List<T>();

            var lines = File.ReadAllLines(path).ToList();
            //var lines = path.ToList();
            var headers = lines[0].Split(',');
            lines.RemoveAt(0);
            foreach (var item in lines)
            {
                var t = new T();
                var values = item.Split(',');
                //使用for实现
                for (int i = 0; i < headers.Length; i++)
                {
                    var prop = t.GetType().GetProperty(headers[i]);
                    if (prop != null)
                    {
                        if (prop.PropertyType == typeof(string))
                        {
                            prop.SetValue(t, values[i]);
                        }
                        else
                        {
                            //根据对象类型进行转换
                            var parseMethod = prop.PropertyType.GetMethod(
                                "Parse",
                                new[] { typeof(string) }
                            );
                            if (parseMethod != null)
                            {
                                var value = parseMethod.Invoke(null, new object[] { values[i] });
                                prop.SetValue(t, value);
                            }
                            else if (prop.PropertyType is Type)
                            {
                                prop.SetValue(t, Type.GetType(values[i]));
                            }
                        }
                    }
                }
                result.Add(t);
            }
            return result;
        }

        /// <summary>
        /// 对象集合反写入csv文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">对象集合</param>
        /// <param name="path">写入文件路径</param>
        /// <param name="headers">需要的属性名称集合</param>
        public static void WriteCsv<T>(IEnumerable<T> list, string path, string[] headers = null)
        {
            //读取集合，根据类对象，反向写入到csv，可输入表头，如不输入，按类的属性顺序自动排列，先处理成字符串列表，然后一次性写入
            var lines = new List<string>();
            if (headers != null)
            {
                lines.Add(string.Join(",", headers));
            }
            else
            {
                var props = typeof(T).GetProperties();
                headers = new string[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    headers[i] = props[i].Name;
                }
                lines.Add(string.Join(",", headers));
            }

            foreach (var item in list)
            {
                var values = new List<string>();
                foreach (var header in headers)
                {
                    var prop = typeof(T).GetProperty(header);
                    if (prop != null)
                    {
                        var value = prop.GetValue(item);
                        values.Add(value == null ? "" : value.ToString());
                    }
                }
                lines.Add(string.Join(",", values));
            }
            // return lines;
            File.WriteAllLines(path, lines);
        }

        #endregion



        /// <summary>
        /// 读取Csv，返回行集合
        /// </summary>
        /// <param name="path"></param>
        /// <param name="hasTitle"></param>
        /// <returns></returns>
        public static List<string> ReadCsv(string path, bool hasTitle)
        {
            if (!File.Exists(path))
                return new List<string>();

            var lines = File.ReadAllLines(path).ToList();
            if (hasTitle)
            {
                lines.RemoveAt(0);
            }
            return lines;
        }

        /// <summary>
        /// 读取Csv，使用委托执行自定义操作
        /// </summary>
        /// <param name="path"></param>
        /// <param name="action"></param>
        /// <param name="hasTitle"></param>
        public static void ReadCsvUseAction(string path, Action action, bool hasTitle = false)
        {
            var lines = ReadCsv(path, hasTitle);
            foreach (var item in lines)
            {
                action?.Invoke();
            }
        }
    }
}

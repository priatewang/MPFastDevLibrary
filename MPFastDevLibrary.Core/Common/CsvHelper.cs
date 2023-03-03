using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MPFastDevLibrary.Common
{
    /// <summary>
    /// Csv读写帮助类，支持
    /// </summary>
    public class CsvHelper
    {
        /// <summary>
        /// 读取Csv文件，加载到DataTable
        /// </summary>
        /// <param name="path">csv文件路径</param>
        /// <param name="hasTitle">是否有标题行</param>
        /// <param name="SafeLevel">安全等级：0:错误格式行正常添加；1：错误行忽略（不添加），2：出现错误弹出异常</param>
        /// <returns></returns>
        public static DataTable ReadCsvToDataTable(string path, bool hasTitle = false, int SafeLevel = 0)
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
        public static DataTable ReadCsvByStream(string path, bool hasTitle = false, int SafeLevel = 0)
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
                }
            }

            return dt;
        }

        /// <summary>
        /// 以文件流形式将DataTable写入csv文件
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="path"></param>
        /// <param name="hasTitle"></param>
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

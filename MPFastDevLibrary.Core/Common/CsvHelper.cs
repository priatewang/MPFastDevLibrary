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
    public class CsvHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="hasTitle"></param>
        /// <param name="SafeLevel">0:错误格式行正常添加；1：错误行忽略（不添加），2：出现错误弹出异常</param>
        /// <returns></returns>
        public static DataTable ReadCsvToDataTable(string path, bool hasTitle = false, int SafeLevel = 0)
        {

            DataTable dt = new DataTable();
            var lines = ReadCsv(path, false);
            if (hasTitle)
            {
                string title = lines[0];
                string[] values = title.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    dt.Columns.Add();
                }
                lines.RemoveAt(0);
            }

            foreach (var item in lines)
            {
                string[] values = item.Split(',');
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


    }
}

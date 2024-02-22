using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Extensions
{
    /// <summary>
    /// List扩展0
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// 如果不存在，则添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="t"></param>
        public static void AddWithout<T>(this List<T> list, T t)
        {
            if (!list.Contains(t))
            {
                list.Add(t);
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="dels"></param>
        public static void DeleteRange<T>(this List<T> list, IEnumerable<T> dels)
        {
            list.RemoveAll(t => dels.Contains(t));
        }
    }
}

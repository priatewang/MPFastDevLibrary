﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Extensions
{
    /// <summary>
    /// ObservableCollection扩展
    /// </summary>
    public static class ObservableCollectionExtensions
    {
        /// <summary>
        /// 批量添加ObservableCollection的项（循环添加）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obs"></param>
        /// <param name="collection"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddRange<T>(this ObservableCollection<T> obs, IEnumerable<T> collection)
        {
            if (obs == null)
            {
                throw new ArgumentNullException();
            }
            if (collection == null)
            {
                return;
            }
            foreach (var item in collection)
            {
                obs.Add(item);
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obs"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool RemoveRange<T>(
            this ObservableCollection<T> obs,
            IEnumerable<T> collection
        )
        {
            if (obs == null)
                throw new ArgumentNullException();
            if (collection == null)
                return false;
            foreach (var item in collection)
            {
                obs.Remove(item);
            }
            return true;
        }

        /// <summary>
        /// 条件添加，如果不存在则添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obs"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool AddNotExist<T>(this ObservableCollection<T> obs, T item)
        {
            if (obs == null)
                throw new ArgumentNullException();
            if (item == null)
                return false;
            if (obs.Contains(item))
                return false;
            obs.Add(item);
            return true;
        }
    }
}

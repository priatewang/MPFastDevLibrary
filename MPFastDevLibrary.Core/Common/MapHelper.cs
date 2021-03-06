using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Common
{
    /// <summary>
    /// 映射工具类
    /// </summary>
    public class MapHelper
    {

        /// <summary>
        /// 类映射，用于同属性名的不同类快速拷贝
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <typeparam name="T">输入类型</typeparam>
        /// <param name="model">输入对象</param>
        /// <returns></returns>
        public static R Mapping<R, T>(T model)
        {
            R result = Activator.CreateInstance<R>();
            foreach (PropertyInfo info in typeof(R).GetProperties())
            {
                PropertyInfo pro = typeof(T).GetProperty(info.Name);
                if (pro != null)
                    info.SetValue(result, pro.GetValue(model));
            }
            return result;
        }

    }
}

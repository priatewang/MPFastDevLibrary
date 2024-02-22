using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Extensions
{
    /// <summary>
    /// Assembly方法扩展
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// 获取项目下面所有包含spaceName字段的程序集
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="spaceName"></param>
        /// <returns></returns>
        public static List<Assembly> GetReferanceAssemblies(this AppDomain domain, string spaceName)
        {
            var list = new List<Assembly>();
            domain
                .GetAssemblies()
                .ToList()
                .ForEach(i =>
                {
                    if (i.FullName.Contains(spaceName))
                    {
                        list.Add(i);
                    }
                    GetReferanceAssemblies(i, list, spaceName);
                });
            return list;
        }

        static void GetReferanceAssemblies(Assembly assembly, List<Assembly> list, string spaceName)
        {
            assembly
                .GetReferencedAssemblies()
                .ToList()
                .ForEach(i =>
                {
                    if (!i.Name.Contains(spaceName))
                        return;
                    var ass = Assembly.Load(i);
                    if (!list.Contains(ass))
                    {
                        list.Add(ass);
                        GetReferanceAssemblies(ass, list, spaceName);
                    }
                });
        }
    }
}

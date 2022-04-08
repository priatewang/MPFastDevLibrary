using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Ioc
{
    public class ContainerBuilder
    {
        public ContainerBuilder()
        {

        }

        public void AutoRegisterIoc()
        {
            var assembies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var item in assembies)
            {
                ///跳过系统程序集
                if (item.FullName.Contains("System"))
                {
                    continue;
                }
                var types = item.GetTypes();

                Func<Attribute[], bool> IsHaveAutoIocAttribute = o =>
                 {
                     foreach (Attribute attribute in o)
                     {
                         if (attribute is AutoIocAttribute)
                         {
                             return true;
                         }
                     }
                     return false;
                 };

                foreach (var it in types)
                {
                    if (IsHaveAutoIocAttribute(Attribute.GetCustomAttributes(it, true)))
                    {
                        Attribute attribute = Attribute.GetCustomAttribute(it, typeof(AutoIocAttribute));

                        //处理ioc自动注册

                    }
                    
                }



            }
        }

    }
}

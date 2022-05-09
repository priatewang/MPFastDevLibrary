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
            serviceDescriptors = new ServiceCollection();
        }

        IServiceCollection serviceDescriptors;

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

        public void RegisterType<TService>(InstanceType type = InstanceType.Normal) where TService : class
        {
            serviceDescriptors.Add(new ServiceDescriptor(typeof(TService), type));
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="IService">源类型（接口）</typeparam>
        /// <typeparam name="TService">目标类型（类）</typeparam>
        /// <param name="type"></param>
        public void RegisterType<IService, TService>(InstanceType type = InstanceType.Normal) where TService : class
        {
            serviceDescriptors.Add(new ServiceDescriptor(typeof(IService), typeof(TService), type));
            if (type == InstanceType.AbsoluteSingle)
            {
                //如果为绝对唯一，为TService添加唯一服务对象
                int id = typeof(TService).GetHashCode();
                serviceDescriptors.Add(new ServiceDescriptor(typeof(TService), type));

            }
        }


        public IContainer Build()
        {
            return new Container(serviceDescriptors);

        }

    }
}

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

        /// <summary>
        /// 自动加载程序集中需要注册的对象
        /// </summary>
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
                        //处理ioc自动注册
                        AutoIocAttribute attribute = (AutoIocAttribute)Attribute.GetCustomAttribute(it, typeof(AutoIocAttribute));
                        if (attribute.RelationClassType == null)
                        {
                            serviceDescriptors.Add(new ServiceDescriptor(it, attribute.Mode));
                        }
                        else
                        {
                            serviceDescriptors.Add(new ServiceDescriptor(it, attribute.RelationClassType, attribute.Mode));
                            if (attribute.Mode==InstanceType.AbsoluteSingle)
                            {
                                AbsoluteSingleRegister(attribute.RelationClassType);

                            }
                        }
                        

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
                AbsoluteSingleRegister(typeof(TService));
            }
        }

        private void AbsoluteSingleRegister(Type type)
        {
            serviceDescriptors.Add(new ServiceDescriptor(type, InstanceType.AbsoluteSingle));

        }


        public IContainer Build()
        {
            return new Container(serviceDescriptors);

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace MPFastDevLibrary.Common
{
    public class IocContainer
    {
        public static IContainer _container = null;

        public static void SetContainer(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// 获取 IDal 的实例化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            try
            {
                if (_container == null)
                {
                    //Initialise();
                    throw new Exception("IOC实例为实例化!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("IOC实例化出错!" + ex.Message);
            }

            return _container.Resolve<T>();
        }
    }
}

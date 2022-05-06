/*----------------------------------------------------------------
// 创建时间：2022/5/6 19:54:58
// 开发者： WQ
// 文件名： Container
// CLR版本：4.0.30319.42000
// 命名空间：MPFastDevLibrary.Ioc
// 功能描述：
// 使用说明：
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Ioc
{
    public class Container : IContainer
    {
        private IServiceCollection _serviceDescriptors;


        public Container(IServiceCollection serviceDescriptors)
        {
            _serviceDescriptors = serviceDescriptors;
        }


        private static TService CastInstance<TService>(object instance)
        {
            try
            {
                // Allow a cast from null object to null TService.
                return (TService)instance;
            }
            catch (InvalidCastException castException)
            {
                return default(TService);
            }
        }

        public TService Get<TService>()
        {
            int id = typeof(TService).GetHashCode();
            return CastInstance<TService>(_serviceDescriptors[id].GetService());
        }

        public TService Resolve<TService>()
        {
            return Get<TService>();
        }
    }
}

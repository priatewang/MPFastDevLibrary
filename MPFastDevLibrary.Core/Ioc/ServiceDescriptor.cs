/*----------------------------------------------------------------
// 创建时间：2022/5/6 20:01:15
// 开发者： WQ
// 文件名： ServiceDescriptor
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
    public class ServiceDescriptor
    {
        /// <summary>
        /// 接口类型
        /// </summary>
        public Type Source { get; set; }

        /// <summary>
        /// 目标类型
        /// </summary>
        public Type TargetService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }

        public int TargetID { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public InstanceType ServiceType { get; set; }


        /// <summary>
        /// 单例模式的唯一对象
        /// </summary>
        public object Instance { get; set; }


        public ServiceDescriptor(Type type, InstanceType iocType = InstanceType.Normal)
            : this(type, type, iocType)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="id"></param>
        /// <param name="iocType"></param>
        public ServiceDescriptor(Type source, Type target, InstanceType iocType = InstanceType.Normal)
        {
            Source = source;
            TargetService = target;
            ID = source.GetHashCode();
            TargetID = target.GetHashCode();
            ServiceType = iocType;
            if (ServiceType == InstanceType.Singleton)
            {
                Instance = CreateInstance(target);
            }
        }

        public object GetService(IServiceCollection descriptors)
        {
            switch (ServiceType)
            {
                case InstanceType.Normal:
                    return CreateInstance(TargetService);
                case InstanceType.Singleton:
                    return Instance;
                case InstanceType.AbsoluteSingle:
                    if (TargetID == ID)
                    {
                        return Instance;
                    }
                    else
                    {
                        return descriptors[TargetID].GetService(descriptors);
                    }
                default:
                    return CreateInstance(TargetService);
            }
        }


        private object CreateInstance(Type type)
        {
            return System.Activator.CreateInstance(type);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Ioc
{
    /// <summary>
    /// 实例类型
    /// </summary>
    public enum InstanceType
    {
        /// <summary>
        /// 正常模式,多例
        /// </summary>
        Normal,
        /// <summary>
        /// 单例模式
        /// </summary>
        Singleton,

        /// <summary>
        /// 绝对唯一
        /// </summary>
        AbsoluteSingle,

    }

    public class AutoIocAttribute : Attribute
    {
        public InstanceType Mode { get; set; }

        /// <summary>
        /// 接口服务对应实现类
        /// </summary>
        public Type RelationClassType { get; set; }

        public AutoIocAttribute()
        {

        }

        public AutoIocAttribute(InstanceType mode=InstanceType.Normal)
        {
            Mode = mode;
        }

        public AutoIocAttribute(Type type,InstanceType mode=InstanceType.Normal)
        {
            RelationClassType = type;
            Mode = mode;
        }
    }
}

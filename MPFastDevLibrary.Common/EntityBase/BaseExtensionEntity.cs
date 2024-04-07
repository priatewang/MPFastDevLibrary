using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Common
{
    /// <summary>
    /// 附加假删除的实体基类
    /// </summary>
    public class BaseExtensionEntity : BaseModifyEntity
    {
        /// <summary>
        /// 是否删除 1是，0否
        /// </summary>
        public int? IsDelete { get; set; }
    }
}

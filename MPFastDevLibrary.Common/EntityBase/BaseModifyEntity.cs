using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Common
{
    /// <summary>
    /// 具有修改时间和属性
    /// </summary>
    public class BaseModifyEntity : BaseCreateEntity
    {
        [Description("修改时间")]
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? ModifyUserId { get; set; }
    }
}

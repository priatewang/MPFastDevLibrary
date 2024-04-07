using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Common
{
    public class BaseCreateEntity : BaseEntity
    {
        [SugarColumn(IsNullable = true)]
        [Description("创建时间")]
        public DateTime? CreateTime { get; set; }

        [SugarColumn(IsNullable = true)]
        public int CreateUserId { get; set; }
    }
}

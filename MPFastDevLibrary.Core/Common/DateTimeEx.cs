using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Common
{
    /// <summary>
    /// DataTime时间扩展方法
    /// </summary>
    public static class DateTimeEx
    {
        static readonly string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };

        /// <summary>
        /// 转换成星期几
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToWeekDay_CH(this DateTime dt)
        {
            var num = (int)dt.DayOfWeek;

            return weekdays[num];
        }

        /// <summary>
        /// 获取周数（第几周）
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int ToWeekNumber(this DateTime dt)
        {
            var firstDate = new DateTime(dt.Year, 1, 1);
            int dayCount = (int)(dt - firstDate).TotalDays;
            dayCount += Convert.ToInt32(firstDate.DayOfWeek);
            return (int)Math.Ceiling(dayCount / 7.0);
        }


    }
}

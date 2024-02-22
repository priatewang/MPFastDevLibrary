using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Common
{
    public class DateTimeHelper
    {
        /// <summary>
        /// 分钟转时分格式（HH:mm）
        /// </summary>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static string MinuteToFormatHHmm(int minutes)
        {
            if (minutes <= 0)
                return "00:00";
            var hour = minutes / 60;
            var minute = minutes % 60;
            return hour.ToString("D2") + ":" + minute.ToString("D2");
        }

        /// <summary>
        /// 秒数转时分秒格式
        /// </summary>
        /// <param name="sec"></param>
        /// <returns></returns>
        public static string SecendToFormatHms(int seconds)
        {
            if (seconds <= 0)
                return "00:00:00";

            var minutes = seconds / 60;
            var hour = minutes / 60;
            var minute = minutes % 60;
            var second = seconds % 60;

            return $"{hour.ToString("D2")}:{minute.ToString("D2")}:{second.ToString("D2")}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Common
{
    /// <summary>
    /// 枚举转换
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举项的字符串集合 
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns></returns>
        public static List<string> GetEnumContents<T>() where T : Enum
        {
            return Enum.GetNames(typeof(T)).ToList();
        }

        /// <summary>
        /// 获取枚举所有项的描述集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<string> GetEnumDescriptions<T>() where T : Enum
        {

            return Enum.GetValues(typeof(T)).Cast<Enum>().Select(x => x.GetDescription()).ToList();
        }

        /// <summary>
        /// 获取枚举值的描述（需要添加[Description]特性）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;


        }

        /// <summary>
        /// 将描述转成枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static T EnumConvertByDescription<T>(string desc)
        {
            var fields = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public);
            var field = fields.FirstOrDefault(w => (w.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute)?.Description == desc);
            if (field == null)
                return default(T);
            return (T)Enum.Parse(typeof(T), field.Name);
        }

    }
}

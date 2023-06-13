using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MPFastDevLibrary.Mvvm
{
    public class Array2DToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var array2d = value as double[,];
            string res = "";
            for (int i = 0; i < array2d.GetLength(0); i++)
            {
                string two = "";
                for (int j = 0; j < array2d.GetLength(1); j++)
                {
                    two += array2d[i, j].ToString() + ",";
                }
                two = two.Remove(two.Length - 1);
                res += two + "|";
            }
            res = res.Remove(res.Length - 1);

            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return default(double[,]);
            var str = value.ToString();
            var array = str.Split('|');
            int cout = array[0].Split(',').Count();
            double[,] doubles = new double[array.Count(), cout];
            for (int i = 0; i < array.Length; i++)
            {
                var values = array[i].Split(',');
                for (int j = 0; j < values.Length; j++)
                {
                    if (double.TryParse(values[j], out double d))
                    {
                        doubles[i, j] = d;

                    }
                }
            }
            return doubles;
        }
    }
}

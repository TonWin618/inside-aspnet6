using System.ComponentModel;
using System.Globalization;

namespace App
{
    [TypeConverter(typeof(PointTypeConverter))]
    public readonly record struct Point(double X, double Y);

    public class PointTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => sourceType == typeof(string);//支持源字符串的类型转换

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            var split = (value.ToString() ?? "0.0,0.0").Split(',');
            double x = double.Parse(split[0].Trim().TrimStart('('));
            double y = double.Parse(split[1].Trim().TrimEnd(')'));
            return new Point(x, y);
        }
    }
}
namespace App
{
    public class FormatOptions
    {
        public DateTimeFormatOptions DateTime { get; set; } = new DateTimeFormatOptions();
        public CurrencyDecimalFormatOptions CurrencyDecimal { get; set; } = new CurrencyDecimalFormatOptions();
        //去除构造函数
    }
}
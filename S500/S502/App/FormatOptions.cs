using Microsoft.Extensions.Configuration;

namespace App
{
    public class FormatOptions
    {
        public DateTimeFormatOptions DateTime { get; set; }
        public CurrencyDecimalFormatOptions CurrencyDecimal { get; set; }
        //从配置节中读取配置节
        public FormatOptions(IConfiguration config)
        {
            DateTime = new DateTimeFormatOptions(config.GetSection("DateTime"));
            CurrencyDecimal = new CurrencyDecimalFormatOptions(config.GetSection("CurrencyDecimal"));
        }
    }
}
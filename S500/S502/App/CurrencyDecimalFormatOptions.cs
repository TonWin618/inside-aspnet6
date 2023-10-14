using Microsoft.Extensions.Configuration;

namespace App
{
    public class CurrencyDecimalFormatOptions
    {
        public int Digits { get; set; }
        public string Symbol { get; set; }
        //从配置节中读取配置
        public CurrencyDecimalFormatOptions(IConfiguration config)
        {
            Digits = int.Parse(config["Digits"]);
            Symbol = config["Symbol"];
        }
    }
}
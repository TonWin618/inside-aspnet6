using App;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

var options = new ConfigurationBuilder()
        //要将该文件设置为“自动复制到输出目录”
        .AddJsonFile("appsettings.json")
        .Build()
        .GetSection("format")
        .Get<FormatOptions>();//通过自动绑定机制获取对应的Options对象

var dateTime = options.DateTime;
var currencyDecimal = options.CurrencyDecimal;

Console.WriteLine("DateTime:");
Console.WriteLine($"\tLongDatePattern: {dateTime.LongDatePattern}");
Console.WriteLine($"\tLongTimePattern: {dateTime.LongTimePattern}");
Console.WriteLine($"\tShortDatePattern: {dateTime.ShortDatePattern}");
Console.WriteLine($"\tShortTimePattern: {dateTime.ShortTimePattern}");

Console.WriteLine("CurrencyDecimal:");
Console.WriteLine($"\tDigits:{currencyDecimal.Digits}");
Console.WriteLine($"\tSymbol:{currencyDecimal.Symbol}");
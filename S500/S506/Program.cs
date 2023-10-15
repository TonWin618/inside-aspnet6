using App;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Primitives;

var config = new ConfigurationBuilder()
        //此处监听的是bin目录下的appsettings.json文件
        .AddJsonFile("appsettings.json", true, true)
        .Build();

ChangeToken.OnChange(() => config.GetReloadToken(), () =>
{
        var options = config.GetSection("format").Get<FormatOptions>();
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
});

Console.Read();//避免程序直接结束运行



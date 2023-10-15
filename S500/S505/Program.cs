using App;
using Microsoft.Extensions.Configuration;

//运行命令为：dotnet run /env {environment name}

//获取命令行参数/env在args中的序号
var index = Array.IndexOf(args, "/env"); 

//读取/env参数后的内容，没有或者为空则默认为开发环境
var environment = index > -1 ? args[index + 1] : "Development";


var options = new ConfigurationBuilder()
        //要将下列文件设置为“自动复制到输出目录”
        .AddJsonFile("appsettings.json", false)//false表示该文件必须
        .AddJsonFile($"appsettings.{environment}.json", true)//true表示该文件可选
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
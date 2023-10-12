using App;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;

//大小写均可
var source = new Dictionary<string,string>
{
    ["LongDatePattern"] = "dddd, MMMM d, yyyy",
    ["longTimePattern"] = "h:mm:ss tt",
    ["ShortDatePattern"] = "M/d/yyyy",
    ["shortTimePattern"] = "h:mm tt"
};

var config = new ConfigurationBuilder()
    .Add(new MemoryConfigurationSource{ InitialData = source})
    .Build();

var options = new DateTimeFormatOptions(config);
Console.WriteLine($"LongDatePattern: {options.LongTimePattern}");
Console.WriteLine($"LongDatePattern: {options.LongTimePattern}");
Console.WriteLine($"LongDatePattern: {options.ShortDatePattern}");
Console.WriteLine($"LongDatePattern: {options.ShortTimePattern}");

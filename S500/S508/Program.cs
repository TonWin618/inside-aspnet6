using App;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

var source = new Dictionary<string, string>
{
    ["point"] = "(123,456)"
};

var root = new ConfigurationBuilder()
    .AddInMemoryCollection(source)
    .Build();

//这里使用TypeConverter特性指定的PointTypeConverter来进行类型转换
var point = root.GetValue<Point>("point");
Console.WriteLine(point);
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

var source = new Dictionary<string, string?>
{
    ["foo"] = null,
    ["bar"] = "",
    ["baz"] = "123"
};

var root = new ConfigurationBuilder()
    .AddInMemoryCollection(source)
    .Build();

//针对object，直接返回原始值，即字符串或者null
Console.WriteLine(root.GetValue<object>("foo") == null);
Console.WriteLine("".Equals(root.GetValue<object>("bar")));
Console.WriteLine("123".Equals(root.GetValue<object>("baz")));

//针对普通类型
Console.WriteLine(root.GetValue<int>("foo") == 0);
Console.WriteLine(root.GetValue<int>("baz") == 123);

//针对Nullable<T>
Console.WriteLine(root.GetValue<int?>("foo") == null);
Console.WriteLine(root.GetValue<int?>("bar") == null);
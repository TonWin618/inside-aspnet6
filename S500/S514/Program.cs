using Microsoft.Extensions.Configuration;

//需添加Microsoft.Extensions.Configuration.CommandLine的包引用
try
{
    //命令行参数的全名和缩写映射
    var mapping = new Dictionary<string, string>
    {
        ["-a"] = "architecture",
        ["-arch"] = "architecture"
    };
    var configuration = new ConfigurationBuilder()
        .AddCommandLine(args, mapping)
        .Build();
    Console.WriteLine($"Architecture: {configuration["architecture"]}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

//启动命令
    //单参数
        //dotnet run architecture=x64
        //dotnet run /architecture=x64
        //dotnet run -architecture=x64
        //dotnet run --architecture=x64
        //dotnet run -a=x64
        //dotnet run -arch=x64
    //双参数
        //dotnet run -a x64
        //dotnet run -arch x64
        //dotnet run --architecture x64
        //dotnet run /architecture x64
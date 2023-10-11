using App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

static void Print(int layer, string name) => Console.WriteLine($"{new string(' ', layer * 4)}{name}");
new ServiceCollection()
    //使用物理文件系统
    .AddSingleton<IFileProvider>(new PhysicalFileProvider(@"D:\Projects\TestProjects\InsideAspNet6\S400\S401\Test"))
    .AddSingleton<IFileSystem, FileSystem>()
    .BuildServiceProvider()
    .GetRequiredService<IFileSystem>()
    .ShowStructure(Print);

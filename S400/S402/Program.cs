using App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;

var content = await new ServiceCollection()
    .AddSingleton<IFileProvider>(new PhysicalFileProvider(@"D:\Projects\TestProjects\InsideAspNet6\S400\S402\Test"))
    .AddSingleton<IFileSystem, FileSystem>()
    .BuildServiceProvider()
    .GetRequiredService<IFileSystem>()
    .ReadAllTextAsync("Folder2/b.txt");

Console.WriteLine(content);

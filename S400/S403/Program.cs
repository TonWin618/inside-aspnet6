using App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;
using System.Reflection;
using System.Text;

var assembly = Assembly.GetEntryAssembly()!;

var content = await new ServiceCollection()
    //添加嵌入文件系统
    .AddSingleton<IFileProvider>(new EmbeddedFileProvider(assembly))
    .AddSingleton<IFileSystem, FileSystem>()
    .BuildServiceProvider()
    .GetRequiredService<IFileSystem>()
    .ReadAllTextAsync("data.txt");

//从程序集嵌入的资源中获取流，嵌入资源的名称包括命名空间
var stream =  assembly.GetManifestResourceStream($"{assembly.GetName().Name}.data.txt");
var buffer = new byte[stream!.Length];
stream.Read(buffer,0,buffer.Length);

Console.WriteLine(content);
Console.WriteLine(Encoding.Default.GetString(buffer));


using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System.Text;

using var fileProvider = new PhysicalFileProvider(@"D:\Projects\TestProjects\InsideAspNet6\S400\S404\Test");
string? original = null;

//对文件进行监控，传入参数分别为更改消息提供者和更改消息消费者
ChangeToken.OnChange(() => fileProvider.Watch("data.txt"), Callback);

while (true)
{
    //更改文件中的内容为当前时间
    File.WriteAllText(@"D:\Projects\TestProjects\InsideAspNet6\S400\S404\Test\data.txt", DateTime.Now.ToString());
    await Task.Delay(2000);
}

async void Callback()
{
    var stream = fileProvider.GetFileInfo("data.txt").CreateReadStream();
    {
        var buffer = new byte[stream.Length];
        await stream.ReadAsync(buffer);
        var current = Encoding.Default.GetString(buffer);
        if (current != original)
        {
            //在控制台输出文件中的内容
            Console.WriteLine(original = current);
        }
    }
}

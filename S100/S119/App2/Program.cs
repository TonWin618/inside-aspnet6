using Shared;
using Microsoft.AspNetCore.Mvc;
using App2;
using Dapr;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IResultCache, ResultCache>()
    .AddDaprClient();
var app = builder.Build();

//添加云事件中间件
app.UseCloudEvents();
app.MapPost("clear", ClearAsync);
//注册终节点，sidecar会收集当前应用提供的所有订阅处理器的元数据信息
//当sidecar接收到发布消息后，会根据这组元数据选择匹配的订阅处理器
app.MapSubscribeHandler();

app.MapPost("/{method}", Calculate);
app.Run();

//订阅clearresult主题的方法
[Topic(pubsubName:"pubsub", name:"clearresult")]
static Task ClearAsync(IResultCache cache, [FromBody] string[] methods)
    => cache.ClearAsync(methods);

static async Task<IResult> Calculate(string method, [FromBody] Input input, IResultCache resultCache)
{
    var output = await resultCache.GetAsync(method, input);
    if (output == null)
    {
        var result = method.ToLower() switch
        {
            "add" => input.X + input.Y,
            "sub" => input.X - input.Y,
            "mul" => input.X * input.Y,
            "div" => input.X / input.Y,
            _ => throw new InvalidOperationException($"Invalid operation {method}")
        };
        output = new Output { Result = result };
        await resultCache.SaveAsync(method, input, output);
    }
    return Results.Json(output);
}

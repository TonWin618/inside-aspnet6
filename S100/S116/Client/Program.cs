using App;
using Grpc.Core;
using Grpc.Net.Client;
//针对gRPC服务地址创建一个GrpcChannel对象
using var channel = GrpcChannel.ForAddress("http://localhost:5000");
//定义proto文件后自动生成Calculator.CalculatorClient
var client = new Calculator.CalculatorClient(channel);
var inputMessage = new InputMessage { X = 1, Y = 0 };

await InvokeAsync(input => client.AddAsync(input), inputMessage, "+");
await InvokeAsync(input => client.SubstractAsync(input), inputMessage, "-");
await InvokeAsync(input => client.MultiplyAsync(input), inputMessage, "*");
await InvokeAsync(input => client.DivideAsync(input), inputMessage, "/");

//AsyncUnaryCall异步一元调用结果的类型
static async Task InvokeAsync(Func<InputMessage, AsyncUnaryCall<OutputMessage>> invoker, 
    InputMessage input, string @operator)
{
    var output = await invoker(input);
    if (output.Status == 0)
    {
        Console.WriteLine($"{input.X}{@operator}{input.Y}={output.Result}");
    }
    else
    {
        Console.WriteLine(output.Error);
    }
}
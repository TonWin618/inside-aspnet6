using App;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

//gRPC采用HTTP2传输协议，让Kestrel服务器监听的终结点默认采用HTTP2协议
builder.WebHost.ConfigureKestrel(Kestrel => Kestrel.ConfigureEndpointDefaults(endpoint => 
    endpoint.Protocols = HttpProtocols.Http2));

//gRPC相关服务注册
builder.Services.AddGrpc();

var app = builder.Build();

//生成路由终节点
app.MapGrpcService<CalculatorService>();

app.Run();

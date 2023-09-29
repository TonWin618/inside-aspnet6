using App;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IGreeter,Greeter>()
    //通过服务注册将配置节绑定到GreetingOptions对象上
    .Configure<GreetingOptions>(builder.Configuration.GetSection("greeting"));

var app = builder.Build();
app.UseMiddleware<GreetingMiddleware>();
app.Run();

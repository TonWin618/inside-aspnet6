using App;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IGreeter,Greeter>();
//按照Asp.Net Core约定定义的中间件，在注册中间件时就已经利用依赖注入容器将其创建
//所以其生命周期为单例，也不需要将其注册为服务

var app = builder.Build();
app.UseMiddleware<GreetingMiddleware>();
app.Run();

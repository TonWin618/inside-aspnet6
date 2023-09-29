using App;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IGreeter,Greeter>()
    .AddSingleton<GreetingMiddleware>();
//该中间件的实例由依赖注入容器实时提供，需要预先注册为服务
//去掉会提示No service for type 'App.GreetingMiddleware' has been registered.

var app = builder.Build();
app.UseMiddleware<GreetingMiddleware>();
app.Run();

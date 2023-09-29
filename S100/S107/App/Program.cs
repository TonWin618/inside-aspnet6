using App;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IGreeter,Greeter>()
    .AddSingleton<GreetingMiddleware>();//该中间件的构造函数需要依赖项，去掉会提示No service for type 'App.GreetingMiddleware' has been registered.

var app = builder.Build();
app.UseMiddleware<GreetingMiddleware>();
app.Run();

//RequestDelegate的本质就是Func<HttpContext, Task>类型的委托
RequestDelegate handler = context => context.Response.WriteAsync("Hello, World!");
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();
//没有指定路由路径，所有请求都通过这个处理器处理
app.Run(handler: handler);
app.Run();
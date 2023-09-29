var app = WebApplication.Create(args);
IApplicationBuilder appBuilder = app;
appBuilder
    .Use(middleware: HelloMiddleware)
    .Use(middleware: WorldMiddleware);

//只会输出“World!”
//appBuilder
//    .Use(middleware: WorldMiddleware)
//    .Use(middleware: HelloMiddleware);

app.Run();//注册处于管道末端的中间件

//中间件，可体现为一个Func<RequestDelegate,RequestDelegate>的委托对象
static RequestDelegate HelloMiddleware(RequestDelegate next) =>
async httpContext =>
{
    await httpContext.Response.WriteAsync("Hello, ");
    await next(httpContext);
};

static RequestDelegate WorldMiddleware(RequestDelegate next) =>
    httpContext => httpContext.Response.WriteAsync("World!");
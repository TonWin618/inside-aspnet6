var app = WebApplication.Create(args);
app
    .Use(middleware: HelloMiddleware)
    .Use(middleware: WorldMiddleware);
app.Run();

//Use方法内部会将Func<HttpContext, HttpRequest, Task>转化内Func<RequestDelegate, RequestDelegate>
static async Task HelloMiddleware(HttpContext httpContext, RequestDelegate next){
    await httpContext.Response.WriteAsync("Hello, ");
    await next(httpContext);
}

static Task WorldMiddleware(HttpContext httpContext, RequestDelegate next)
    => httpContext.Response.WriteAsync("World!");

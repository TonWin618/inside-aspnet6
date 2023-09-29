//快速创建 Web 应用程序，这种方式的配置选项相对较少
var app = WebApplication.Create(args);
app.Run(handler: HandleAsync);
app.Run();
//将请求处理器定义为一个静态方法
static Task HandleAsync(HttpContext httpContext)
    => httpContext.Response.WriteAsync("Hello, World!");
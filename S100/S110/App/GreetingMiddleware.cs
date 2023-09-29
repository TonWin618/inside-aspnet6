
namespace App;
public class GreetingMiddleware
{
    public GreetingMiddleware(RequestDelegate next){}

    //按照约定定义的中间件类型，依赖服务可以直接注入Invoke方法中
    //注意：构造函数注入和方法注入并不等效
    public async Task InvokeAsync(HttpContext context, IGreeter greeter) =>
        await context.Response.WriteAsync(greeter.Greet(DateTime.Now));
}
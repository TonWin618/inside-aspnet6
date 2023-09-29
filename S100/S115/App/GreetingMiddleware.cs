
namespace App;
public class GreetingMiddleware
{
    public GreetingMiddleware(RequestDelegate next){}
    public async Task InvokeAsync(HttpContext context, IGreeter greeter) =>
        await context.Response.WriteAsync(greeter.Greet(DateTime.Now));
}
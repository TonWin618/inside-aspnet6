
namespace App;
public class GreetingMiddleware : IMiddleware
{
    private readonly IGreeter _greeter;
    public GreetingMiddleware(IGreeter greeter)
    {
        _greeter = greeter;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next) =>
        await context.Response.WriteAsync(_greeter.Greet(DateTime.Now));
}
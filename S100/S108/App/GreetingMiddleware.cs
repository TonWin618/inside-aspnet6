
namespace App;
public class GreetingMiddleware
{
    private readonly IGreeter _greeter;
    public GreetingMiddleware(RequestDelegate next, IGreeter greeter)
    {
        _greeter = greeter;
    }

    public async Task InvokeAsync(HttpContext context) =>
        await context.Response.WriteAsync(_greeter.Greet(DateTime.Now));
}
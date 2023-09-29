using Microsoft.Extensions.Options;

namespace App;
public interface IGreeter
{
    string Greet(DateTimeOffset time);
}

public class Greeter : IGreeter
{
    private readonly GreetingOptions _options;
    private readonly ILogger _logger;
    //ILogger<Greeter>类型会让日志记录带上App.Greeter[0]前缀，将日志记录与特定的类相关联
    public Greeter(IOptions<GreetingOptions> options, ILogger<Greeter> logger)
    {
        _options = options.Value;
        _logger = logger;
    }
    public string Greet(DateTimeOffset time)
    {
        var message = time.Hour switch
        {
            var h when h >= 5 && h < 12 => _options.Morning,
            var h when h >= 12 && h < 17 => _options.Afternoon,
            _ => _options.Evening
        };
        //控制台是默认开启的日志输出渠道之一
        _logger.LogInformation(message: "{time} => {message}", time, message);
        return message;
    }
}
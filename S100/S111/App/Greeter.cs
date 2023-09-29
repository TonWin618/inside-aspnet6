using Microsoft.Extensions.Options;

namespace App;
public interface IGreeter
{
    string Greet(DateTimeOffset time);
}

public class Greeter: IGreeter
{
    private readonly GreetingOptions _options;
    public Greeter(IOptions<GreetingOptions> options)
    {
        //通过Value属性获得配置
        _options = options.Value;
    }
    public string Greet(DateTimeOffset time) => time.Hour switch
    {
        //使用配置
        var h when h >= 5 && h < 12 => _options.Morning,
        var h when h >= 12 && h < 17 => _options.Afternoon,
        _ => _options.Evening
    };
}
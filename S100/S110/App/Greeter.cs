namespace App;
public interface IGreeter
{
    string Greet(DateTimeOffset time);
}

public class Greeter: IGreeter
{
    private readonly IConfiguration _configuration;
    public Greeter(IConfiguration configuration)
    {
        //获得配置节
        _configuration = configuration.GetSection("Greeting");
    }
    public string Greet(DateTimeOffset time) => time.Hour switch
    {
        //不同的配置项值获取方式
        //索引
        var h when h >= 5 && h < 12 => _configuration["morning"],
        //方法
        var h when h >= 12 && h < 17 => _configuration.GetValue<string>("afternoon"),
        //配置节
        _ => _configuration.GetSection("evening").Value
    };
}
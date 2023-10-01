using Microsoft.Extensions.DependencyInjection;

//服务注册有效性校验关闭
BuildServiceProvider(false);
//服务注册有效性校验开启
BuildServiceProvider(true);

static void BuildServiceProvider(bool validateOnBuild)
{
    try
    {
        var options = new ServiceProviderOptions
        {
            ValidateOnBuild =validateOnBuild
        };
        new ServiceCollection()
            .AddSingleton<IFoobar,Foobar>()
            .BuildServiceProvider(options);
        Console.WriteLine($"Status: Suceess; ValidateOnBuild: {validateOnBuild}");
    }
    catch(Exception ex)
    {
        Console.WriteLine($"Status: Fail; ValidateOnBuild: {validateOnBuild}");
        Console.WriteLine($"Error: {ex.Message}");
    }
}

public interface IFoobar{}
public class Foobar : IFoobar
{
    private Foobar(){}
    //Foobar具有唯一私有构造函数，针对该务实现类的注册无效
    public static readonly Foobar Instance = new Foobar();
}
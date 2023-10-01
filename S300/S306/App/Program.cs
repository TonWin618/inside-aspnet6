using Microsoft.Extensions.DependencyInjection;

var root = new ServiceCollection()
    .AddSingleton<IFoo, Foo>()
    .AddScoped<IBar, Bar>()
    .BuildServiceProvider(true);//启用服务注册验证

var child = root.CreateScope().ServiceProvider;

ResolveService<IFoo>(root);//Error: Cannot consume scoped service 'IBar' from singleton 'IFoo'. Foo依赖Bar
ResolveService<IBar>(root);//Error: Cannot resolve scoped service 'IBar' from root provider.   根容器不能提供Scope服务
ResolveService<IFoo>(child);//Error: Cannot consume scoped service 'IBar' from singleton 'IFoo'. 单例服务在根容器上
ResolveService<IBar>(child);//Scope服务由子容器提供


void ResolveService<T>(IServiceProvider provider)
{
    var isRootContainer = (root == provider) ? "Yes" : "No";
    try
    {
        provider.GetService<T>();
        Console.WriteLine($"Status: Success; Service Type: {typeof(T).Name}; Root: {isRootContainer}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Status: Fail; Service Type: {typeof(T).Name}; Root: {isRootContainer}");
        Console.WriteLine($"Error: {ex.Message}");
    }
}

public interface IFoo { }
public interface IBar { }
public class Bar : IBar { }
public class Foo : IFoo
{
    public IBar Bar { get; }
    public Foo(IBar bar) => Bar = bar;
}
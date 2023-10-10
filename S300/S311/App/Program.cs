using Microsoft.Extensions.DependencyInjection;

//异步
await using var scope = new ServiceCollection()
    .AddScoped<Fooar>()
    .BuildServiceProvider()
    .CreateAsyncScope();//异步

scope.ServiceProvider.GetRequiredService<Fooar>();
//当以异步方式释放容器时，可以采用同步方式释放服务实例，反之则不成立
public class Fooar : IAsyncDisposable
{
    public ValueTask DisposeAsync() => default;
}
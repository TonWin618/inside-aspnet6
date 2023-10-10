using Microsoft.Extensions.DependencyInjection;

using var scope = new ServiceCollection()
    .AddScoped<Fooar>()
    .BuildServiceProvider()
    .CreateScope();

scope.ServiceProvider.GetRequiredService<Fooar>();
//当IServiceProvider对象的Dispose方法被执行时，释放的服务实例对应的类在实现IAsyncDisposable的同时也要实现IDisposable接口，否则报错：
//System.InvalidOperationException: 'Fooar' type only implements IAsyncDisposable. Use DisposeAsync to dispose the container.
public class Fooar : IAsyncDisposable
{
    public ValueTask DisposeAsync() => default;
}
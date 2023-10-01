using App;
using Microsoft.Extensions.DependencyInjection;

var root = new ServiceCollection()
    .AddTransient<IFoo, Foo>()
    .AddScoped<IBar>(_ => new Bar())
    .AddSingleton<IBaz, Baz>()
    .BuildServiceProvider();

var provider1 = root.CreateScope().ServiceProvider;
var provider2 = root.CreateScope().ServiceProvider;

GetServiceTwice<IFoo>(provider1);
GetServiceTwice<IBar>(provider1);
GetServiceTwice<IBaz>(provider1);
Console.WriteLine();
GetServiceTwice<IFoo>(provider2);
GetServiceTwice<IBar>(provider2);
GetServiceTwice<IBaz>(provider2);


static void GetServiceTwice<T>(IServiceProvider provider)
{
    provider.GetService<T>();
    provider.GetService<T>();
}

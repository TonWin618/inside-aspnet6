using App;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

var provider = new ServiceCollection()
    .AddTransient<IFoo, Foo>()
    .AddScoped<IBar>(_ => new Bar())
    .AddSingleton<IBaz, Baz>()
    .BuildServiceProvider();
Console.WriteLine(provider.GetService<IFoo>() is Foo);
Console.WriteLine(provider.GetService<IBar>() is Bar);
Console.WriteLine(provider.GetService<IBaz>() is Baz);

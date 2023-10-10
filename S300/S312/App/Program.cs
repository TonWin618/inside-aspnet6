using App;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<Foo>()
    .AddSingleton<Bar>()
    .BuildServiceProvider();

//var fooBar = ActivatorUtilities.CreateInstance<Foobar>(serviceProvider, "foobar");
var fooBar = ActivatorUtilities.CreateInstance(serviceProvider, typeof(Foobar), "foobar") as Foobar;
Console.WriteLine(fooBar?.Name);
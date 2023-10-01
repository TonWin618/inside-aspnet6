using App;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Linq;

var services = new ServiceCollection()
    .AddTransient<Base, Foo>()
    .AddTransient<Base, Bar>()
    .AddTransient<Base, Baz>()
    .BuildServiceProvider()
    .GetServices<Base>();
    
Console.WriteLine(services.OfType<Foo>().Any());
Console.WriteLine(services.OfType<Bar>().Any());
Console.WriteLine(services.OfType<Baz>().Any());
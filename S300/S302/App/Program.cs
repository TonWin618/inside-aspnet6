using App;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

var provider = new ServiceCollection()
    .AddTransient<IFoo, Foo>()
    .AddTransient<IBar, Bar>()
    .AddTransient(typeof(IFoobar<,>), typeof(Foobar<,>))
    .BuildServiceProvider();

var foobar = (Foobar<IFoo, IBar>?)provider.GetService<IFoobar<IFoo, IBar>>();
Console.WriteLine(foobar?.Foo is Foo);
Console.WriteLine(foobar?.Bar is Bar);
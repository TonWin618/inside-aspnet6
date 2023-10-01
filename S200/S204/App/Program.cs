using App;
var services = new Cat()
    .Register<Base,Foo>(Lifetime.Transient)
    .Register<Base,Bar>(Lifetime.Transient)
    .Register<Base,Baz>(Lifetime.Transient)
    .GetServices<Base>();

Console.WriteLine(services.OfType<Foo>().Any());
Console.WriteLine(services.OfType<Bar>().Any());
Console.WriteLine(services.OfType<Baz>().Any());
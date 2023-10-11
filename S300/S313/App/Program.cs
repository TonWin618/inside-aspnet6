using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var constructor = typeof(Foobarbaz).GetConstructors().Single();
var matcherType = typeof(ActivatorUtilities).GetNestedType("ConstructorMatcher",BindingFlags.NonPublic) ?? throw new InvalidOperationException("It fails to resove ConstructorMatcher type");
var matchMethod = matcherType.GetMethod("Match");

var foo = new Foo();
var bar = new Bar();
var baz = new Baz();
var qux = new Qux();

//-1
Console.WriteLine($"[Qux] = {Match(qux)}");
//0
Console.WriteLine($"[Foo] = {Match(foo)}");
//1
Console.WriteLine($"[Foo, Bar] = {Match(foo, bar)}");
//2
Console.WriteLine($"[Foo, Bar, Baz] = {Match(foo, bar, baz)}");
//0
Console.WriteLine($"[Bar, Baz] = {Match(bar, baz)}");
//0
Console.WriteLine($"[Foo, Baz] = {Match(foo, baz)}");

int? Match(params object[] args)
{
    var matcher = Activator.CreateInstance(matcherType,constructor);
    return (int?)matchMethod?.Invoke(matcher, new object[] {args});
}

public class Foo {}
public class Bar {}
public class Baz {}
public class Qux {}

public class Foobarbaz
{
    public Foobarbaz(Foo foo, Bar bar, Baz baz)
    {

    }
}

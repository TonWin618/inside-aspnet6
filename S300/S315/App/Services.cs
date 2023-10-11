using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public class Foo { }
    public class Bar { }
    public class Baz { }

    public class Foobar
    {
        public Foobar(Foo foo) => Console.WriteLine("Foobar(Foo foo)");
        //指定使用该构造函数
        [ActivatorUtilitiesConstructor]
        public Foobar(Foo foo, Bar bar) => Console.WriteLine("Foobar(Foo foo, Bar bar)");
    }
    public class BarBaz
    {
        public BarBaz(Bar bar, Baz baz) => Console.WriteLine("BarBaz(Bar bar, Baz baz)");
        [ActivatorUtilitiesConstructor]
        public BarBaz(Bar bar) => Console.WriteLine("BarBaz(Bar bar)");
    }

}
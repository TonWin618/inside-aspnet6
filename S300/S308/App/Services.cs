namespace App
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public interface IQux { }

    public class Foo : IFoo { }
    public class Bar : IBar { }
    public class Baz : IBaz { }
    public class Qux : IQux
    {
        public Qux(IFoo foo)
            => Console.WriteLine("Selected constructor: Qux(IFoo)");
        //最终被调用的是这个
        //选择参数最多的合法构造函数
        public Qux(IFoo foo, IBar bar)
            => Console.WriteLine("Selected constructor: Qux(IFoo, IBar)");
        //缺少参数，不合法
        public Qux(IFoo foo, IBar bar, IBaz baz)
            => Console.WriteLine("Selected constructor: Qux(IFoo, IBar, IBaz)");
    }


}
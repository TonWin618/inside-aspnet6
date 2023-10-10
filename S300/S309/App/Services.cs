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
        //抛出InvalidOperation异常，无法从两个构造函数中选择最优的来创建服务实例。
        public Qux(IFoo foo, IBar bar) { }
        public Qux(IBar bar, IBaz baz) { }
    }


}
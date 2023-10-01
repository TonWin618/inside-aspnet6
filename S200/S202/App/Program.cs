using System.Diagnostics;
using App;
var cat = new Cat()
    .Register<IFoo,Foo>(Lifetime.Transient) //Register<服务类型,实例类型>(生命周期)
    .Register<IBar,Bar>(Lifetime.Transient)
    .Register(typeof(IFoobar<,>),typeof(Foobar<,>),Lifetime.Transient);

var foobar = (Foobar<IFoo,IBar>?)cat.GetService<IFoobar<IFoo,IBar>>();//在需要提供时才创建Foobar<Foo,Bar>对象
Console.WriteLine(foobar?.Foo is Foo);
Console.WriteLine(foobar?.Bar is Bar);
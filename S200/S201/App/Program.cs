using App;
var root = new Cat()
    .Register<IFoo,Foo>(Lifetime.Transient) //Register<服务类型,实例类型>(生命周期)
    .Register<IBar>(_=>new Bar(),Lifetime.Self) //Register<服务类型>(实例创建函数, 生命周期)
    .Register<IBaz,Baz>(Lifetime.Self)
    .Register(typeof(Foo).Assembly);//提供程序集，在程序集中查找MapTo特性的类将其注册

var cat1 = root.CreateChild();
var cat2 = root.CreateChild();

void GetServices<TService>(Cat cat) where TService:class
{
    cat.GetService<TService>();
    cat.GetService<TService>();
}

GetServices<IFoo>(cat1);//会显示两次创建
GetServices<IBar>(cat1);
GetServices<IBaz>(cat1);
GetServices<IQux>(cat1);
Console.WriteLine();
GetServices<IFoo>(cat2);
GetServices<IBar>(cat2);
GetServices<IBaz>(cat2);
GetServices<IQux>(cat2);
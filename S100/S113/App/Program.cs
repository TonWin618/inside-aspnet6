using App;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IGreeter,Greeter>()
    .Configure<GreetingOptions>(builder.Configuration.GetSection("greeting"));

var app = builder.Build();

//注册一个指向路径`/greet`的终节点，终节点的处理器指向Greet方法的委托对象
//在浏览器中请求`http://localhost:5000/greet`路径可触发
app.MapGet("/greet", Greet);
app.Run();

static string Greet(IGreeter greeter) => greeter.Greet(DateTimeOffset.Now);
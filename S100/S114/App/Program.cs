using App;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IGreeter,Greeter>()
    .Configure<GreetingOptions>(builder.Configuration.GetSection("greeting"))
    //完成与Controller相关的服务注册
    .AddControllers();
var app = builder.Build();
//将定义在所有Controller类型中的Action方法映射为对应的终节点
app.MapControllers();
app.Run();

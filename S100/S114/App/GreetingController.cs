using Microsoft.AspNetCore.Mvc;

namespace App;

public class GreetingController
{
    //Action方法最终转化成一个或者多个终节点
    //通过FromServices特性可以在方法中注入服务
    [HttpGet("/greet")]
    public string Greet([FromServices] IGreeter greeter) =>
        greeter.Greet(DateTimeOffset.Now);
}
using Microsoft.AspNetCore.Mvc;

namespace App;

public class GreetingController: Controller
{
    //Action方法最终转化成一个或者多个终节点
    //通过FromServices特性可以在方法中注入服务
    [HttpGet("/greet")]
    public IActionResult Greet()
    {
        ViewBag.Time = DateTimeOffset.Now;
        //视图引擎会根据当前的Controller和Action名称定位目标视图的cshtml文件
        //按照默认视图定位规则，视图文件路径为`/Views/Greeting/Greet.cshtml`或者`/Views/Shared/Greet.cshtml`
        return View();
    }
}
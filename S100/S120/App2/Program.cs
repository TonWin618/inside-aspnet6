using Shared;
using Microsoft.AspNetCore.Mvc;
using App2;
using Dapr;

var builder = WebApplication.CreateBuilder(args);
//注册Accumulator Actor
builder.Services
    .AddActors(options => options.Actors.RegisterActor<Accumulator>());
var app = builder.Build();
//注册Actor终节点
app.MapActorsHandlers();
app.Run();
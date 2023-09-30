using Shared;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("{method}", Calculate);

app.Run();

static IResult Calculate(string method, [FromBody] Input input)
{
    var result = method.ToLower() switch
    {
        "add" => input.X + input.Y,
        "sub" => input.X - input.Y,
        "mul" => input.X * input.Y,
        "div" => input.X / input.Y,
        _ => throw new InvalidOperationException($"Invalid operation {method}")
    };
    return Results.Json(new Output { Result = result });
}

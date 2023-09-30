using Dapr.Client;
using Shared;

HttpClient client = DaprClient.CreateInvokeHttpClient(appId: "app2");
var input = new Input{ X = 2, Y = 1 };
await InvokeAsync("add");
await InvokeAsync("sub");
await InvokeAsync("mul");
await InvokeAsync("div");

async Task InvokeAsync(string method)
{
    var response = await client.PostAsync(method,JsonContent.Create(input));
    var output = await response.Content.ReadFromJsonAsync<Output>();
    Console.WriteLine(
        $"{input.X} {method} {input.Y} = {output.Result} ({output.Timestamp})"
    );
}
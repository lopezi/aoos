using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FlowbiteBlazorWasmStarter;
using Blazored.LocalStorage;
using ArweaveBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddArweaveBlazor();

await builder.Build().RunAsync();


public static class MemValues
{
    public static string Address { get; set; } = "j7XsUSfBWqkBX35uJdx2nDXOXcpLPLj8ltLnFvvMQHg";
    public static string Token { get; set; } = "DK6-PR-ZFsyrHCqjH7kxJxlGtOYFLG-RXiPyfYVFCjw";
}
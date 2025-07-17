using BlazorPortfolio;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

// Pour Radzen components
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Pour Radze
builder.Services.AddRadzenComponents();
// Pour les services de notification
builder.Services.AddScoped<NotificationService>();

await builder.Build().RunAsync();

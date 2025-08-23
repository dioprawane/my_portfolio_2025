using BlazorPortfolio;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorPortfolio.Services;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection; // <--- obligatoire pour AddHttpClient

// Pour Radzen components
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Base address de ton API Python (dev)
builder.Services.AddScoped(sp => new AnalyticsApiClient(
    new HttpClient
    {
        BaseAddress = new Uri("http://127.0.0.1:8000/") // ou depuis config
    }
));

// Pour Radze
builder.Services.AddRadzenComponents();
// Pour les services de notification
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<GitHubService>();
builder.Services.AddScoped<LocalCache>();


await builder.Build().RunAsync();

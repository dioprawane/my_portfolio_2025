using BlazorPortfolio;
using BlazorPortfolio.Models;
using BlazorPortfolio.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration; // <-- Ajoutez cette ligne
using Microsoft.Extensions.DependencyInjection; // <--- obligatoire pour AddHttpClient
// Pour Radzen components
using Radzen;
using System.Net.Http;
using static System.Net.WebRequestMethods;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Lecture de la configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Base address de ton API Python (dev)
builder.Services.AddScoped(sp => new AnalyticsApiClient(
    new HttpClient
    {
        //BaseAddress = new Uri("http://127.0.0.1:8000/") // ou depuis config
        BaseAddress = new Uri("https://back-portefolio.onrender.com/") // ou depuis config
    }
));

// Print de la base address
Console.WriteLine("BaseAddress: " + builder.Services.BuildServiceProvider().GetRequiredService<HttpClient>().BaseAddress);

// Pour Radze
builder.Services.AddRadzenComponents();
// Pour les services de notification
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<GitHubService>();
builder.Services.AddScoped<LocalCache>();
// ...
builder.Services.Configure<EmailJSSettings>(builder.Configuration.GetSection("EmailJSSettings"));
// ...


await builder.Build().RunAsync();

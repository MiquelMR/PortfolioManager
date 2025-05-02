using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PortfolioManagerWASM;
using PortfolioManagerWASM.Services;
using PortfolioManagerWASM.Services.IService;
using PortfolioManagerWASM.ViewModels;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Inject Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();

// Inject ViewModels
builder.Services.AddScoped<LoginViewModel>();
builder.Services.AddScoped<HomeViewModel>();
builder.Services.AddScoped<NavbarViewModel>();
builder.Services.AddScoped<UserProfileViewModel>();
builder.Services.AddScoped<AdminViewModel>();

// Local Storage
builder.Services.AddBlazoredLocalStorage();

// SyncFusion
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzgzMDkzOEAzMjM5MmUzMDJlMzAzYjMyMzkzYmtiM2ZVTVpqWDBxTVFyTkpJR1ljWG43MVZZcER4N3Z4NkhIeU1NM2Z0Kzg9");
builder.Services.AddSyncfusionBlazor();

// Authentication
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthStateProvider>());

var host = builder.Build();

await host.RunAsync();
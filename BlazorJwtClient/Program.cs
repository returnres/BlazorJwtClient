
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorJwtClient;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiUrl = builder.Configuration["ApiSettings:BaseUrl"];
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(uriString: apiUrl) });

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();


// Registrazione di TokenStorage come Scoped
builder.Services.AddScoped<TokenStorage>();
builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();

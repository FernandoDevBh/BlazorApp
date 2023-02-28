using Client;
using Client.Services;
using Client.Pages.Set.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Client.Components.Navigation.Menu.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client.Pages.Card;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseAPIUrl = builder.Configuration.GetValue<string>("BaseAPIUrl");

if(!string.IsNullOrEmpty(baseAPIUrl))
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAPIUrl) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddServices();
builder.Services.AddCardServices();
builder.Services.AddCategoryServices();
builder.Services.AddMenuServices();

await builder.Build().RunAsync();

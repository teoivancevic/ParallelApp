using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ParallelApp.Client;
using MudBlazor.Services;
using ParallelApp.Shared.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();

//public User appUser = await Http.GetFromJsonAsync<User>("api/user/getuserbyid/" + user_id.ToString());

await builder.Build().RunAsync();

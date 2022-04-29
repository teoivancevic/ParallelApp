using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ParallelApp.Client;
using MudBlazor.Services;
using ParallelApp.Shared.Models;
using MudBlazor;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("AAIEdu", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.DefaultScopes.Add("hrEduPersonAffiliation");
    options.ProviderOptions.DefaultScopes.Add("hrEduPersonGroupMember");
    options.ProviderOptions.DefaultScopes.Add("hrEduPersonPersistentID");
    options.ProviderOptions.DefaultScopes.Add("o");
    options.ProviderOptions.DefaultScopes.Add("sn");
    options.ProviderOptions.DefaultScopes.Add("givenName");
});


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<LogoutToken>();

builder.Services.AddMudServices();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});


//public User appUser = await Http.GetFromJsonAsync<User>("api/user/getuserbyid/" + user_id.ToString());

await builder.Build().RunAsync();


public class LogoutToken
{
    private string token = string.Empty;

    public string GetToken()
    {
        return token;
    }
    public void SetToken(string val)
    {
        token = val;
    }
}
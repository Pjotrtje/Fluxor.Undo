using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Fluxor;

namespace FluxorBlazorWeb.ReduxDevToolsTutorial.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("app");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddFluxor(o =>
        {
            o.ScanAssemblies(typeof(Program).Assembly);
#if DEBUG
            o.UseReduxDevTools(rdt =>
            {
                rdt.Name = "Fluxor ReduxDevTools sample";
                rdt.EnableStackTrace();
                rdt.UseSystemTextJson(_ =>
                    new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                    }
                );
            });
#endif
        });
        await builder.Build().RunAsync();
    }
}

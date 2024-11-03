using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace ConfigurazioniSvc.LoadBalancer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddReverseProxy()
            .LoadFromConfig(builder.Configuration.GetSection("LoadBalancer"));

        builder.Services.Configure<KestrelServerOptions>(builder.Configuration.GetSection("Kestrel"));
        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        var app = builder.Build();

        app.MapReverseProxy();
        app.Run();
    }
}
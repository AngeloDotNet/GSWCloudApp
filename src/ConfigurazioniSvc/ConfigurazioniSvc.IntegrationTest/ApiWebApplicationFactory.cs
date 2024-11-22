using ConfigurazioniSvc.DataAccessLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurazioniSvc.IntegrationTest;

public class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            var assembly = typeof(Program).Assembly.GetName().Name!.ToString();
            var databaseInMemory = string.Concat(assembly, "-InMemory-Test");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseInMemory);
            });
        });
    }
}
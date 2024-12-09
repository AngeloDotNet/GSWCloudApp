using GSWCloudApp.Common.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutenticazioneSvc.BusinessLayer.HostedService;

public class AuthStartupTask(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        await GenerateRolesAsync(roleManager);

        //TODO: Manca la generazione dell'utente amministratore
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static async Task GenerateRolesAsync(RoleManager<ApplicationRole> roleManager)
    {
        var roleNames = new string[] { RoleNames.Administrator, RoleNames.PowerUser, RoleNames.User };

        foreach (var roleName in roleNames)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                await roleManager.CreateAsync(new ApplicationRole(roleName));
            }
        }
    }
}
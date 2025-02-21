using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Identity;
using GSWCloudApp.Common.Identity.Entities.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutenticazioneSvc.BusinessLayer.HostedService;

public class AuthStartupTask(IServiceProvider serviceProvider, ILogger<AuthStartupTask> logger, IConfiguration configuration) : IHostedService
{
    //private readonly ILogger<AuthStartupTask> logger = logger;
    //private readonly IConfiguration configuration = configuration;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        await GenerateRolesAsync(roleManager, logger);

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        await GenerateAdminUserProfileAsync(userManager);
    }

    private async Task GenerateAdminUserProfileAsync(UserManager<ApplicationUser> userManager)
    {
        var adminUser = new ApplicationUser
        {
            UserName = "admin",
            Email = "admin@example.com",
            FirstName = "Admin",
            LastName = "User",
            EmailConfirmed = true,
            TwoFactorEnabled = false,
            LockoutEnabled = false
        };

        await CheckCreateUserAsync(adminUser, RoleNames.Administrator);

        async Task CheckCreateUserAsync(ApplicationUser user, params string[] roles)
        {
            var userPassword = MicroservicesExtensions.GetConfigurationAppAsync(configuration).Result.DefaultAdminPassword;
            var dbUser = await userManager.FindByEmailAsync(user.Email!);

            if (dbUser == null)
            {
                var result = await userManager.CreateAsync(user, userPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(user, roles);
                }
            }
        }
    }

    private static async Task GenerateRolesAsync(RoleManager<ApplicationRole> roleManager, ILogger<AuthStartupTask> logger)
    {
        var roleNames = new string[] { RoleNames.Administrator, RoleNames.PowerUser, RoleNames.User };

        foreach (var roleName in roleNames)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                await roleManager.CreateAsync(new ApplicationRole(roleName));
                logger.LogInformation($"Role {roleName} created.");
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}

using GSWCloudApp.Common.Identity;
using GSWCloudApp.Common.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutenticazioneSvc.BusinessLayer.HostedService;

public class AuthStartupTask(IServiceProvider serviceProvider, ILogger<AuthStartupTask> logger, IConfiguration configuration) : IHostedService
{
    private readonly ILogger<AuthStartupTask> _logger = logger;
    private readonly IConfiguration _configuration = configuration;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        await GenerateRolesAsync(roleManager, _logger);

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        await GenerateAdminUserProfileAsync(userManager, _logger);
    }

    private async Task GenerateAdminUserProfileAsync(UserManager<ApplicationUser> userManager, ILogger<AuthStartupTask> logger)
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
            var userPassword = _configuration.GetSection("DefaultAdminPassword").Value
                ?? throw new ArgumentNullException("DefaultAdminPassword not found in appsettings.json");

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

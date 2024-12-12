using System.Security.Cryptography;
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

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        await GenerateAdminUserProfileAsync(scope, userManager);
    }

    private static async Task GenerateAdminUserProfileAsync(IServiceScope scope, UserManager<ApplicationUser> userManager)
    {
        var administratorUser = new ApplicationUser
        {
            UserName = "admin",
            Email = "admin@example.com",
            FirstName = "Admin",
            LastName = "User",
            EmailConfirmed = true,
            TwoFactorEnabled = false,
            LockoutEnabled = false
        };

        //TODO: Aggiungere un log per loggare la password generata
        await CheckCreateUserAsync(administratorUser, GeneratePassword(15), RoleNames.Administrator);

        async Task CheckCreateUserAsync(ApplicationUser user, string password, params string[] roles)
        {
            var dbUser = await userManager.FindByEmailAsync(user.Email!);

            if (dbUser == null)
            {
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(user, roles);
                }
            }
        }
    }

    private static string GeneratePassword(int length)
    {
        var validCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!#$&".ToCharArray();

        if (length <= 0)
        {
            throw new ArgumentException("Password length must be greater than zero.", nameof(length));
        }

        var byteBuffer = new byte[length];

        RandomNumberGenerator.Fill(byteBuffer);

        var passwordChars = byteBuffer.Select(b => validCharacters[b % validCharacters.Length]).ToArray();

        return new string(passwordChars);
    }

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

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
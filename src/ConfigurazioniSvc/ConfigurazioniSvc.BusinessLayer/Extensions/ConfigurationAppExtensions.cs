using System.Text.Json;
using ConfigurazioniSvc.Shared;
using GSWCloudApp.Common.Identity.Options;
using GSWCloudApp.Common.Options;
using Microsoft.Extensions.Configuration;

namespace ConfigurazioniSvc.BusinessLayer.Extensions;

public static class ConfigurationAppExtensions
{
    public static void GenerateJSON(IConfiguration configuration, string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        var applicationOptions = configuration.GetSection("ApplicationOptions").Get<ApplicationOptions>() ?? new();
        var jwtSettings = configuration.GetSection("JwtOptions").Get<JwtOptions>() ?? new();
        var workerSettings = configuration.GetSection("WorkerSettings").Get<WorkerSettings>() ?? new();
        var pollyPolicyOptions = configuration.GetSection("PollyPolicyOptions").Get<PollyPolicyOptions>() ?? new();
        var redisOptions = configuration.GetSection("RedisOptions").Get<RedisOptions>() ?? new();

        var configurationApp = new ConfigurationApp()
        {
            ConnectionStrings = new ConnectionStrings()
            {
                SqlAutentica = configuration.GetConnectionString("SqlAutentica") ?? string.Empty,
                SqlConfigSmtp = configuration.GetConnectionString("SqlConfigSmtp") ?? string.Empty,
                SqlGestDocumenti = configuration.GetConnectionString("SqlGestDocumenti") ?? string.Empty,
                SqlGestLoghi = configuration.GetConnectionString("SqlGestLoghi") ?? string.Empty,
                SqlInvioEmail = configuration.GetConnectionString("SqlInvioEmail") ?? string.Empty
            },
            ApplicationOptions = new ApplicationOptions()
            {
                TabellaMigrazioni = applicationOptions.TabellaMigrazioni,
                SwaggerEnable = applicationOptions.SwaggerEnable,
                MaxRetryCount = applicationOptions.MaxRetryCount,
                MaxRetryDelaySeconds = applicationOptions.MaxRetryDelaySeconds
            },
            JwtOptions = new JwtOptions()
            {
                Audience = jwtSettings.Audience,
                Issuer = jwtSettings.Issuer,
                SecurityKey = jwtSettings.SecurityKey,
                AccessTokenExpirationMinutes = jwtSettings.AccessTokenExpirationMinutes,
                RefreshTokenExpirationMinutes = jwtSettings.RefreshTokenExpirationMinutes
            },
            WorkerSettings = new WorkerSettings()
            {
                WorkerDelayInSeconds = workerSettings.WorkerDelayInSeconds
            },
            PollyPolicyOptions = new PollyPolicyOptions()
            {
                RetryCount = pollyPolicyOptions.RetryCount,
                SleepDuration = pollyPolicyOptions.SleepDuration
            },
            RedisOptions = new RedisOptions()
            {
                Hostname = redisOptions.Hostname,
                InstanceName = redisOptions.InstanceName,
                AbsoluteExpireTime = redisOptions.AbsoluteExpireTime,
                SlidingExpireTime = redisOptions.SlidingExpireTime
            },
            DefaultAdminPassword = configuration.GetSection("DefaultAdminPassword").Value ?? string.Empty
        };

        File.WriteAllText(filePath, JsonSerializer.Serialize(configurationApp, jsonOptions));
    }

    private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
}

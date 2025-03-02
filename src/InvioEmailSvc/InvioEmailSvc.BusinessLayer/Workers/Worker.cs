using GSWCloudApp.Common;
using GSWCloudApp.Common.Exceptions;
using GSWCloudApp.Common.Extensions;
using InvioEmailSvc.BusinessLayer.Extensions;
using InvioEmailSvc.BusinessLayer.Services.Interfaces;
using InvioEmailSvc.DataAccessLayer;
using InvioEmailSvc.DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InvioEmailSvc.BusinessLayer.Workers;

public class Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IConfiguration configuration) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var settings = await MicroservicesExtensions.GetWorkerSettingsAsync(configuration);

        while (!cancellationToken.IsCancellationRequested)
        {
            await ProcessOutboxAsync(cancellationToken);

            await Task.Delay(TimeSpan.FromSeconds(settings.WorkerDelayInSeconds), cancellationToken); // Intervallo di polling
        }
    }

    private async Task ProcessOutboxAsync(CancellationToken cancellationToken)
    {
        var pollyOptions = await MicroservicesExtensions.GetPollyPolicyOptionsAsync(configuration);
        var retryPolicy = DependencyInjection.GetRetryPolicy(logger, pollyOptions);

        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var emailService = scope.ServiceProvider.GetRequiredService<IMailKitEmailSenderService>();
        var unsentEmail = new List<EmailMessage>();

        unsentEmail = await dbContext.GetListEmailMessageAsync(cancellationToken);

        if (unsentEmail.Count != 0)
        {
            foreach (var email in unsentEmail.Where(email => email != null))
            {
                try
                {
                    await retryPolicy.ExecuteAsync(async () =>
                    {
                        if (await emailService.SendEmailAsync(email, cancellationToken))
                        {
                            await dbContext.UpdateEmailAsync(email.Id, cancellationToken);
                        }
                        else
                        {
                            throw new SendEmailException();
                        }
                    });
                }
                catch (SendEmailException ex)
                {
                    logger.LogError(ex, "Error sending email with id {emailId}", email.Id);
                }
                catch (Exception ex)
                {
                    logger.LogError("Error sending email, exception thrown: {exception}", ex.Message);
                }
            }
        }
    }
}
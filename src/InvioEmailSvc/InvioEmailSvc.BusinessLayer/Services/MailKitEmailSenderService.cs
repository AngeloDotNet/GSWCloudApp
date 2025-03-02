using GSWCloudApp.Common.Constants;
using GSWCloudApp.Common.Exceptions;
using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Settings.SendEmail;
using InvioEmailSvc.BusinessLayer.Services.Interfaces;
using InvioEmailSvc.DataAccessLayer.Entities;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace InvioEmailSvc.BusinessLayer.Services;

public class MailKitEmailSenderService(ILogger<MailKitEmailSenderService> logger, IConfiguration configuration, HttpClient httpClient) : IMailKitEmailSenderService
{
    public async Task<bool> SendEmailAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        try
        {
            var settings = await MicroservicesExtensions.GetSettingsAsync<SmtpSettings>(httpClient, configuration, EndpointAPI.ConfigurazioniSmtp);
            var sender = await MicroservicesExtensions.GetSettingsAsync<SenderSettings>(httpClient, configuration, EndpointAPI.ConfigurazioniSender);

            var email = CreateEmailMessage(emailMessage, sender);
            using var client = new SmtpClient();

            await client.ConnectAsync(settings.Host, settings.Port, settings.Security, cancellationToken);

            if (!string.IsNullOrWhiteSpace(settings.Username) && !string.IsNullOrWhiteSpace(settings.Password))
            {
                await client.AuthenticateAsync(settings.Username, settings.Password, cancellationToken);
            }

            await client.SendAsync(email, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
        catch (EndpointUnreachableException ex)
        {
            logger.LogError(ex, "Error reaching the endpoint {Endpoint}", ex.Message);
            return false;
        }
        catch (InvalidResponseApiException ex)
        {
            logger.LogError(ex, "Error reading the response from the API");
            return false;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error sending email to {EmailTo}", emailMessage.To);
            return false;
        }

        return true;
    }

    private static MimeMessage CreateEmailMessage(EmailMessage message, SenderSettings sender)
    {
        var email = new MimeMessage
        {
            Subject = message.Subject,
            Body = new BodyBuilder { TextBody = message.TextContent }.ToMessageBody()
        };

        email.From.Add(new MailboxAddress(sender.Name ?? sender.Email, sender.Email));
        email.To.AddRange(message.To?.Select(a => new MailboxAddress(a, a)) ?? []);
        email.Cc.AddRange(message.Cc?.Select(a => new MailboxAddress(a, a)) ?? []);
        email.Bcc.AddRange(message.Bcc?.Select(a => new MailboxAddress(a, a)) ?? []);
        email.ReplyTo.AddRange(message.ReplyTo?.Select(a => new MailboxAddress(a, a)) ?? []);

        return email;
    }
}
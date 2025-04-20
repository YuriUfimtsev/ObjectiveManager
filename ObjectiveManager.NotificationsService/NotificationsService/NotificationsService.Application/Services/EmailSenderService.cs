using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using NotificationsService.Application.Configuration;
using NotificationsService.Application.Models;
using NotificationsService.Application.Services.Interfaces;

namespace NotificationsService.Application.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly EmailSenderConfiguration _configuration;
    private readonly ILogger<EmailSenderService> _logger;

    public EmailSenderService(IOptions<EmailSenderConfiguration> configuration,
        ILogger<EmailSenderService> logger)
    {
        _configuration = configuration.Value;
        _logger = logger;
    }

    public async Task SendEmailWithRetriesAsync(string destinationAddress, EmailMessageModel emailMessageModel,
        CancellationToken cancellationToken)
    {
        if (_configuration.ServiceAppCredentials == null || _configuration.RetrySettings == null)
        {
            throw new ApplicationException("Не указаны параметры конфигурации сервиса уведомлений");
        }

        var retryCount = _configuration.RetrySettings.Count;
        var message = GetMessage(destinationAddress, emailMessageModel);

        // Отправляем письмо с несколькими повторными попытками с разницей в определенное время
        while (!await SendEmailAsync(destinationAddress, emailMessageModel.Subject, message, cancellationToken)
               && retryCount > 0)
        {
            await Task.Delay(_configuration.RetrySettings.DelayInMinutes * 60 * 1000, cancellationToken);
            --retryCount;
        }
    }

    private async Task<bool> SendEmailAsync(string recipient, string subject, MimeMessage message,
        CancellationToken cancellationToken)
    {
        try
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(
                _configuration.ServiceAppCredentials!.SmtpServer,
                _configuration.ServiceAppCredentials.SmtpPort,
                _configuration.ServiceAppCredentials.UseSsl,
                cancellationToken
            );
            await client.AuthenticateAsync(
                _configuration.ServiceAppCredentials.ServiceLogin,
                _configuration.ServiceAppCredentials.ServicePassword,
                cancellationToken
            );
            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);

            _logger.LogInformation($"Письмо отправлено успешно. Получатель {recipient}, тема: {subject}");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"Письмо для {recipient} не отправлено. Причина: {e.Message}");
            return false;
        }
    }

    private MimeMessage GetMessage(string recipient, EmailMessageModel emailMessageModel)
    {
        var message = new MimeMessage();
        message.From.Add(
            new MailboxAddress(
                _configuration.ServiceAppCredentials!.ServiceEmailName,
                _configuration.ServiceAppCredentials.ServiceEmailAddress
            ));
        message.To.Add(
            new MailboxAddress(recipient, recipient));
        message.Subject = emailMessageModel.Subject;
        message.Body = new TextPart("html")
        {
            Text = emailMessageModel.Body
        };

        return message;
    }
}
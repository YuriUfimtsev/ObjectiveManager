using NotificationsService.Application.Models;

namespace NotificationsService.Application.Services.Interfaces;

public interface IEmailSenderService
{
    public Task SendEmailWithRetriesAsync(string destinationAddress, EmailMessageModel emailMessageModel,
        CancellationToken cancellationToken);
}
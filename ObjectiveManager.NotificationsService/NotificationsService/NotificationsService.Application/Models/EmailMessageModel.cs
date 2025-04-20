namespace NotificationsService.Application.Models;

public record EmailMessageModel(
    string Subject,
    string Body
);
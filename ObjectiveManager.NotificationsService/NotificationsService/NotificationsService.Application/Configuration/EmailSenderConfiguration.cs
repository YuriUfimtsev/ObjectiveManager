namespace NotificationsService.Application.Configuration;

public class EmailSenderConfiguration
{
    public ServiceAppCredentials? ServiceAppCredentials { get; set; }
    public RetrySettings? RetrySettings { get; set; }
}
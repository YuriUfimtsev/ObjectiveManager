namespace NotificationsService.Application.Configuration;

public class RetrySettings
{
    public int Count { get; set; }
    
    public int DelayInMinutes { get; set; }
}
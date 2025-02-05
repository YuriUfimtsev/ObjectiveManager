namespace NotificationsService.Application.Configuration;

public class ServiceAppCredentials
{
    public string? SmtpServer { get; set; }
    
    public int SmtpPort { get; set; }
    
    public bool UseSsl { get; set; }
    
    public string? ServiceEmailAddress { get; set; }
    
    public string? ServiceEmailName { get; set; }
    
    public string? ServiceLogin { get; set; }
    
    public string? ServicePassword { get; set; }
}
namespace Assessment14.Models;

public class NotificationMessage
{
    public string ToEmail { get; set; } = "";
    public string ToPhone { get; set; } = "";
    public string Subject { get; set; } = "";
    public string Body { get; set; } = "";
}
namespace Assessment14.Options;

public class EmailSettings
{
    public string FromEmail { get; set; } = "";
    public string SmtpHost { get; set; } = "";
    public int SmtpPort { get; set; }
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
}
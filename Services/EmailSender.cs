using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

// IEmailSender funktionalitet som ansluter och skickar email för bekräftelse med mera.
public class EmailSender : IEmailSender
{
    private readonly string _smtpServer;
    private readonly int _port;
    private readonly string _senderEmail;
    private readonly string _password;

    public EmailSender(string smtpServer, int port, string senderEmail, string password)
    {
        _smtpServer = smtpServer;
        _port = port;
        _senderEmail = senderEmail;
        _password = password;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var client = new SmtpClient(_smtpServer, _port)
        {
            Credentials = new NetworkCredential(_senderEmail, _password),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_senderEmail),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        mailMessage.To.Add(email);

        await client.SendMailAsync(mailMessage);
    }
}

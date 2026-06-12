using System.Net;
using System.Net.Mail;

namespace LjruiFoodWeb.Services;

public class EmailService : IEmailService
{
    private readonly string _email;
    private readonly string _password;

    public EmailService()
    {
        _email = "lyminhkhang45@gmail.com";
        _password = "zjmm axlj ommx ykny";
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(_email, _password)
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_email, "Ljrui Food"),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        mailMessage.To.Add(email);

        await client.SendMailAsync(mailMessage);
    }
}

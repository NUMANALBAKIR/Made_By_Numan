using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace Client.Areas.Identity.EmailSender;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string emailAddress, string subject, string htmlMessage)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(MailboxAddress.Parse("numan.al.developer@gmail.com"));
        emailMessage.To.Add(MailboxAddress.Parse(emailAddress));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

        // send
        using (var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate("numan.al.developer@gmail.com", "vjblnfgesrscnlvn");
            client.Send(emailMessage);
            client.Disconnect(true);
        }
        return Task.CompletedTask;
    }
}

﻿using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace Client.Areas.Identity.EmailSender;

public class EmailSender : IEmailSender
{
    public EmailSender(IConfiguration configuration)
    {
        _smtpEmail = configuration.GetValue<string>("SMTP:Email");
        _smtpPassword = configuration.GetValue<string>("SMTP:Password");
    }
    private string _smtpEmail;
    private string _smtpPassword;


    public Task SendEmailAsync(string emailAddress, string subject, string htmlMessage)
    {
        // using sendinblue
        try
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(_smtpEmail));
            emailMessage.To.Add(MailboxAddress.Parse(emailAddress));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            // send
            using (var client = new SmtpClient())
            {
                client.Connect("smtp-relay.sendinblue.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(_smtpEmail, _smtpPassword);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("-- ERROR: " + e);
        }
        finally
        {
        }

        return Task.CompletedTask;
    }
}

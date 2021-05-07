using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ChazuraProgram.MyEmail
{
    public class Mailer : IMailer
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly IWebHostEnvironment _env;
        public Mailer (IOptions<SmtpSettings> smtpSettings,IWebHostEnvironment env)
        {
            _smtpSettings = smtpSettings.Value;
            _env = env;
        }
            
        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                var messege = new MimeMessage();
                messege.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                messege.To.Add(new MailboxAddress(email));
                messege.Subject = subject;
                messege.Body = new TextPart("html")
                {
                    Text = body
                };
                using var client = new SmtpClient
                {
                    ServerCertificateValidationCallback = (s, c, h, e) => true
                };
                if (_env.IsDevelopment())
                {
                    await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, true);
                }
                else
                {
                    await client.ConnectAsync(_smtpSettings.Server);
                }
                await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                await client.SendAsync(messege);
                await client.DisconnectAsync(true);
            }
            catch (Exception e)
            {

                throw new InvalidOperationException(e.Message);
            }
            //await Task.CompletedTask;
        }
    }
}

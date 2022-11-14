using AspNetMVC_P324.Data;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace AspNetMVC_P324.Services
{
    public class MailManager : IMailServices
    {
        private readonly MailSettings _mailSettings;

        public MailManager(IOptions<MailSettings> mailsettings)
        {
           
            _mailSettings = mailsettings.Value;
        }
        public async Task SendEmailAsync(RequestEmail requestemail)
        {
            try
            {
                var email = new MimeMessage 
                { 
                    Sender = MailboxAddress.Parse(_mailSettings.Mail) 
                };

                email.To.Add(MailboxAddress.Parse(requestemail.ToEmail));

                email.Subject=requestemail.Subject;

                var builder = new BodyBuilder
                {
                    HtmlBody = requestemail.Body
                };
                email.Body = builder.ToMessageBody();

                using var smpt = new SmtpClient();
                smpt.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smpt.Authenticate(_mailSettings.Mail, _mailSettings.Password);

                await smpt.SendAsync(email);

                smpt.Disconnect(true);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}

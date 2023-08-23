using MimeKit;
using OnlineShopWebAPI.Interface;

namespace OnlineShopWebAPI.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly EmailServiceConfiguration _emailConfig;

        public SendEmailService(EmailServiceConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void BlockedEmailNotification(string emailTo)
        {
            var message = CreateEmailMessageBlocked(emailTo);
            Send(message);
        }

        public void VerifiedEmailNotification(string emailTo)
        {
            var message = CreateEmailMessageVerified(emailTo);
            Send(message);
        }
        private MimeMessage CreateEmailMessageVerified(string emailTo)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(string.Empty, _emailConfig.From));
            emailMessage.To.Add(new MailboxAddress(string.Empty, emailTo));
            emailMessage.Subject = "Account verification";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = "Your account has been verified!" };
            return emailMessage;
        }
        private MimeMessage CreateEmailMessageBlocked(string emailTo)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(string.Empty, _emailConfig.From));
            emailMessage.To.Add(new MailboxAddress(string.Empty, emailTo));
            emailMessage.Subject = "Account blocked";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = "Your account has been blocked!" };
            return emailMessage;
        }
        private void Send(MimeMessage mailMessage)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}

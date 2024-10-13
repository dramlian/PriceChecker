using System.Net;
using System.Net.Mail;
using PriceChecker.Models;

namespace PriceChecker.Helpers
{
    public class MailSender
    {
        IEnumerable<string> _recipients;
        SmtpClient _smtpClient;

        public MailSender(Config parsedXmlConfig)
        {
            _recipients= parsedXmlConfig.recipients;
            _smtpClient = new SmtpClient("in-v3.mailjet.com", 587)
            {
                Credentials = new NetworkCredential(parsedXmlConfig.networkUsername, parsedXmlConfig.networkPassword),
                EnableSsl = true
            };
        }

        public void SendMail(string subject, string body)
        {
            try
            {
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("jankovdamian@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                foreach (string recipient in _recipients) //addrange not avaiLable for some reason
                {
                    mailMessage.To.Add(recipient);
                }
                _smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }
    }
}

using System;
using System.Net;
using System.Net.Mail;

namespace FindingImmo.Core.Infrastructure.Mailing
{
    internal sealed class Mailer
    {
        public void Send(string title, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            var msg = new MailMessage()
            {
                From = new MailAddress(Configuration.Smtp.Sender),
                Subject = title,
                Body = message,
                IsBodyHtml = true
            };

            foreach (string recipient in Configuration.MailRecipients)
                msg.To.Add(recipient);

            using (SmtpClient client = new SmtpClient())
            {
                client.Send(msg);
            }
        }

        private SmtpClient BuildSmtpClient()
        {
            return new SmtpClient()
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                DeliveryFormat = SmtpDeliveryFormat.SevenBit,
                Host = Configuration.Smtp.Host,
                Port = Configuration.Smtp.Port,
                Credentials = new NetworkCredential(Configuration.Smtp.UserName, Configuration.Smtp.Password),
                EnableSsl = true,
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Publico.Models;

namespace Publico.Services {
    // Сервис отправки подтверждения почты
    public class EmailService {
        public static async Task SendToEmail(User user) {
            try {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("sierra_93@mail.ru"));
                emailMessage.To.Add(new MailboxAddress("sierra_93@mail.ru"));
                emailMessage.Subject = "Подтверждение почты";
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) {
                    Text = "Здравствуйте, " + user.Login + " для подтверждения почты перейдите <a href=\"https://localhost:44323/Home/SuccessEmail\"> по ссылке </a>"
                };
                using (var client = new SmtpClient()) {
                    await client.ConnectAsync("smtp.mail.ru", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync("sierra_93@mail.ru", "31293dd");
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex) {
                new Exception(ex.Message.ToString());
            }
        }
    }
}

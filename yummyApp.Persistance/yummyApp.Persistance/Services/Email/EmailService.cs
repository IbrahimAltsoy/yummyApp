using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;
using yummyApp.Application.Services.Email;

namespace yummyApp.Persistance.Services.Email
{
    public class EmailService : IEmailService
    {
        readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
                smtp.Port = 587;
                smtp.EnableSsl = true; // Güvenli bağlantıyı etkinleştirin
                smtp.Host = _configuration["Mail:Host"];

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(_configuration["Mail:Username"], "Yummy Application");
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = isBodyHtml;

                    foreach (var to in tos)
                    {
                        mail.To.Add(to);
                    }

                    try
                    {
                        await smtp.SendMailAsync(mail);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"SendMailAsync Hatası: {ex.ToString()}");
                        throw;
                    }
                }
            }
        }


        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {

            StringBuilder mail = new();
            mail.AppendLine("Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br>");
            mail.AppendLine("<strong><a target=\"_blank\" href=\"" + _configuration["AngularClientUrl"] + "/updatepassword/" + userId + "/" + resetToken + "\">Yeni şifre talebi için tıklayınız...</a></strong><br><br>");

            mail.AppendLine("<span style=\"font-size:12px;\">NOT : Eğer ki bu talep tarafınızca gerçekleştirilmemişse lütfen bu maili ciddiye almayınız.</span><br>");
            mail.AppendLine("Saygılarımızla...<br><br><br>yummyApp Company");
            //url = $"<strong><a target=\"_blank\" href=\"{AngularClientUrl}/login/{userId}/{resetToken}\">Yeni şifre talebi için tıklayınız...</a></strong><br><br>";
            await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString());


        }
    }
}

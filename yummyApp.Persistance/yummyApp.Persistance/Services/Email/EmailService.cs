using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;
using yummyApp.Application.Services.Email;
using System.Security.Cryptography;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.RegularExpressions;

namespace yummyApp.Persistance.Services.Email
{
    public class EmailService : IEmailService
    {
        readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> CreateEmailActivationKey()
        {
            byte[] bytes = RandomNumberGenerator.GetBytes(64);
            string key = Convert.ToBase64String(bytes)
                .Replace("+", "-") 
                .Replace("/", "_")  
                .TrimEnd('='); 
            return Task.FromResult(key);
        }

        public async Task SendActivationEmailAsync(string email, string activationCode)
        {
            var encodedActivationCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(activationCode));
            string activationLink = _configuration["ApplicationSettings:AdminApplication"]!+ $"/api/Auth/VerifyEmail?Email={email}&ActivationCode={encodedActivationCode}";
            await SendMailAsync(
                email,
                "Aktivasyon Kodu",
                $"Kaydınızı doğrulamak için aşağıdaki bağlantıya tıklayınız: <a href='{activationLink}'>Aktivasyon Linki</a>");
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            // E-posta adreslerini kontrol et
            foreach (var to in tos)
            {
                if (!IsValidEmail(to))
                {
                    throw new Exception($"Geçersiz e-posta adresi: {to}");
                }
            }

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = _configuration["Mail:Host"]!;

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(_configuration["Mail:Username"]!, "Yummy Application");
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
                        throw new Exception("E-posta gönderilirken bir hata oluştu.", ex);
                    }
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {               
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }


        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {

            StringBuilder mail = new();            
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(resetToken));
            mail.AppendLine("Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br>");
            mail.AppendLine("<strong><a target=\"_blank\" href=\"" + _configuration["ApplicationSettings:AdminApplication"]! + "/Auth/UpdatePassword/" + userId + "/" + encodedToken + "\">Yeni şifre talebi için tıklayınız...</a></strong><br><br>");

            mail.AppendLine("<span style=\"font-size:12px;\">NOT : Eğer ki bu talep tarafınızca gerçekleştirilmemişse lütfen bu maili ciddiye almayınız.</span><br>");
            mail.AppendLine("Saygılarımızla...<br><br><br>yummyApp Company");
            await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString());


        }
        
    }
}

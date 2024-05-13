using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yummyApp.Application.Services.Email
{
    public interface IEmailService
    {
        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true);

        Task SendPasswordResetMailAsync(string to, string userId, string resetToken);
    }
}

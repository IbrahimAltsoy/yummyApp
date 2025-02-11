using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yummyApp.Application.Features.Users.Constants
{
    public abstract class AuthMessages
    {
        public const string UserDontExists = "Kullanıcı bilgilerinizi kontrol ederek tekrar deneyin.";
        public const string UserMailAlreadyExists = "Kullanıcı mevcut, lütfen şifrenizi girerek veya şifre yenileyerek giriş yapınız.";
        public const string UserEmailHasNotBeenVerified = "Eposta adresiniz doğrulanmamış. Lütfen epostanıza gelen bağlantıdan doğrulama yapınız.";
        public const string UserPhoneNumberAlreadyRegistered = "Bu telefon numarası zaten kullanılıyor.";
    }
}

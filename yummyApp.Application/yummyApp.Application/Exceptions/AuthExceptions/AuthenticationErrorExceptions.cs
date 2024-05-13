namespace yummyApp.Application.Exceptions.AuthExceptions
{
    public class AuthenticationErrorExceptions : Exception
    {
        public AuthenticationErrorExceptions() : base("Kimlik doğrulama hatası oluştu. Tekrardan Giriş yapınız.")
        {
        }

        public AuthenticationErrorExceptions(string? message) : base(message)
        {
        }

        public AuthenticationErrorExceptions(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

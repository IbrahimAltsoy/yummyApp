namespace yummyApp.Application.Exceptions.AuthExceptions
{
    public class UserEmailVerifyCheckException : Exception
    {
        public UserEmailVerifyCheckException() : base("Eposta adresiniz doğrulanmamış. Lütfen epostanıza gelen bağlantıdan doğrulama yapınız.") { }

        public UserEmailVerifyCheckException(string? message) : base(message)
        {
        }

        public UserEmailVerifyCheckException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

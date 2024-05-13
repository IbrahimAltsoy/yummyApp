namespace yummyApp.Application.Exceptions.AuthExceptions
{
    public class PasswordChangeFailledException : Exception
    {
        public PasswordChangeFailledException()
        {
        }

        public PasswordChangeFailledException(string? message) : base("Şifre güncellenirken hata oluştu.")
        {
        }

        public PasswordChangeFailledException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

namespace yummyApp.Application.Exceptions.AuthExceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException()
        {
            
        }
        public UserAlreadyExistsException(string? message) : base("Mail zaten mevcut şifremi unuttum bölümüne gidiniz!")
        {
        }

        public UserAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

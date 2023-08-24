namespace ETicaretAPI.Application.Exceptions
{

    public class NotFoundUserException : Exception
    {
        public NotFoundUserException() : base("Kullanıcı adı veya email hatalı  ")
        {
        }

        public NotFoundUserException(string? message) : base(message)
        {
        }

        public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

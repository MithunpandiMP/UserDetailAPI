namespace UserDetailAPI.CustomMiddleware
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message): base(message) { }
    }
}

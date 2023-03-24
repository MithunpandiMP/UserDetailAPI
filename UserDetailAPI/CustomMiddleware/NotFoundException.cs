namespace UserDetailAPI.CustomMiddleware
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message): base(message) { }
    }
}

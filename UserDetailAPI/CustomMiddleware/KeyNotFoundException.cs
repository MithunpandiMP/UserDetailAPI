namespace UserDetailAPI.CustomMiddleware
{
    public class KeyNotFoundException : Exception
    {
       public KeyNotFoundException(string message) : base(message) { }
    }
}

namespace BookPhone
{
    public interface ITokenMiddleware
    {
            Task Invoke(HttpContext context);
    }
}

namespace JwtWebApiTutorial.Exceptions
{
    public class UnauthorizedException : HttpResponseException
    {
        public UnauthorizedException()
            : base(401, "Unauthorized", "Incorrect username or password.")
        {
        }
    }
}

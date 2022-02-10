namespace JwtWebApiTutorial.Exceptions
{
    public class NotFoundException : HttpResponseException
    {
        public NotFoundException()
            : base(404, "Not Found", "User not found.")
        {
        }
    }
}

namespace JwtWebApiTutorial.Exceptions
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException(int status, string title, string message)
        {
            Title = title;
            Status = status;
            ErrorMessage = message;
        }

        public string Title { get; set; }

        public int Status { get; set; }

        public string ErrorMessage { get; set; }
    }
}

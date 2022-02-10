namespace JwtWebApiTutorial.Responses.Auth
{
    public class PostRefreshResponse
    {
        public string message { get; set; } = string.Empty;
        public int status { get; set; }
        public string access_token { get; set; } = string.Empty;
    }
}

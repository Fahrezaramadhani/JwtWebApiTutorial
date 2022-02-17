namespace JwtWebApiTutorial.Responses.Auth
{
    public class PostRefreshResponse
    {
        public string Message { get; set; } = string.Empty;
        public int Status { get; set; }
        public string AccessToken { get; set; } = string.Empty;
    }
}

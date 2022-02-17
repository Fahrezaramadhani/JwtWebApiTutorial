namespace JwtWebApiTutorial.Requests.Auth
{
    public class PostRefreshRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}

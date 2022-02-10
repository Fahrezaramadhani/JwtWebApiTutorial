namespace JwtWebApiTutorial.Requests.Auth
{
    public class PostRefreshRequest
    {
        public string refresh_token { get; set; } = string.Empty;
    }
}

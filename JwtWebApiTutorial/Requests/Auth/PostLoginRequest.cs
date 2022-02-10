namespace JwtWebApiTutorial.Requests.Auth
{
    public class PostLoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

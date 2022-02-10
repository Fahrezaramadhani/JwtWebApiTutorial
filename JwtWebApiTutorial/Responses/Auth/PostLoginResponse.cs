using JwtWebApiTutorial.Constants;

namespace JwtWebApiTutorial.Responses.Auth
{
    public class PostLoginResponse
    {
        public string message { get; set; } = string.Empty;
        public int status { get; set; }
        public UserProfile user { get; set; } 
        public string access_token { get; set; } = string.Empty;
        public string refresh_token { get; set; } = string.Empty;
    }
}

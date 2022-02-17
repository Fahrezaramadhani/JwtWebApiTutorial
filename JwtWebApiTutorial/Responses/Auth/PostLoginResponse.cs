using JwtWebApiTutorial.Constants;

namespace JwtWebApiTutorial.Responses.Auth
{
    public class PostLoginResponse
    {
        public string Message { get; set; } = string.Empty;
        public int Status { get; set; }
        public UserProfile Data { get; set; } 
    }
}

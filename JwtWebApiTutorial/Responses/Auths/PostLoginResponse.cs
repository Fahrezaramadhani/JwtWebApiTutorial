
namespace JwtWebApiTutorial.Responses.Auths
{
    public class PostLoginResponse
    {
        public int Id { get; set; }
        public int SuperiorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string PhotoName { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}

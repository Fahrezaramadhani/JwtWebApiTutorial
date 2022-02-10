namespace JwtWebApiTutorial.Configurations
{
    public class JWTConfiguration
    {
        public string SecretKey { get; set; }

        public string RefreshSecretKey { get; set; }

        public int ExpirationTime { get; set; }

        public int RefreshExpirationTime { get; set; }
    }
}

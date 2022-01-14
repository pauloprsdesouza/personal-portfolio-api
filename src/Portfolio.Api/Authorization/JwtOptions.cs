namespace Portfolio.Api.Authorization
{
    public class JwtOptions
    {
        public const string Issuer = "http://api.paulosouza.com";

        public const string Audience = "http://client.paulosouza.com";

        public string Secret { get; set; }
    }
}

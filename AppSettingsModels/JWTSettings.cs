namespace InMemoryDBSpecificationRepositoryUOWProject.AppSettingsModels
{
    public class JWTSettings
    {
        public string SigningKey { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string RSAKeyPath { get; set; } = null!;
    }
}

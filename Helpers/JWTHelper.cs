using System.Security.Cryptography;

namespace InMemoryDBSpecificationRepositoryUOWProject.Helpers
{
    public static class JWTHelper
    {
        public static RSA LoadRSAKeys(string rsaKeyPath)
        {
            var rsa = RSA.Create();
            if (!File.Exists(rsaKeyPath))
            {
                throw new FileNotFoundException($"RSA key file not found at path: {rsaKeyPath}");
            }
            var pemContent = File.ReadAllText(rsaKeyPath);
            rsa.ImportFromPem(pemContent.ToCharArray());
            return rsa;
        }
    }
}

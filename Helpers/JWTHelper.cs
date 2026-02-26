using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using InMemoryDBSpecificationRepositoryUOWProject.AppSettingsModels;

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

    public class ClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _environment;
        private readonly JWTSettings _jwtSettings;
        private readonly Dictionary<string, string> _claims;

        public ClaimService(IHttpContextAccessor httpContextAccessor, JWTSettings jwtSettings, IWebHostEnvironment environment)
        {
            _httpContextAccessor = httpContextAccessor;
            _environment = environment;
            _jwtSettings = jwtSettings;
            _claims = new Dictionary<string, string>();

            HttpContext? httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                string token = httpContext.Request.Headers["Authorization"].ToString();
                if (token != "")
                {
                    token = token.Replace("Bearer ", "");
                    SetClaimsFromToken(token);
                }
            }
        }

        public string this[string key]
        {
            get
            {
                if (_claims == null) return "";
                if (_claims.ContainsKey(key)) return _claims[key];
                return "";
            }
        }

        private void SetClaimsFromToken(string token)
        {
            string path = Path.Combine(_environment.ContentRootPath, _jwtSettings.RSAKeyPath);
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new RsaSecurityKey(JWTHelper.LoadRSAKeys(path))
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var claims = jwtToken.Claims.ToList();
            foreach (var item in claims)
            {
                _claims.Add(item.Type, item.Value);
            }
        }
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MovieApp.Database;
using MovieApp.Models;

namespace MovieApp.Methods
{
    public class JWTMethod : IJWTMethod
    {
        private readonly IDatabaseCollections _databaseCollections;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JWTMethod(IDatabaseCollections databaseCollections, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _databaseCollections = databaseCollections;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public string CreateToken(string UserEmail)
        {
            var AboutUser = _databaseCollections.UserDetails().Find(x => x.EmailAddress == UserEmail).SingleOrDefault();
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, UserEmail),
                new Claim(ClaimTypes.Role, AboutUser.IsAdmin? "true" : "false")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWTTokentext:Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddSeconds(100000),
                signingCredentials: cred
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        public async Task<SignUpModel> ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWTTokentext:Token").Value!));

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = key
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                string email = principal.FindFirst(ClaimTypes.Name)?.Value!;
                var user = _databaseCollections.UserDetails().Find(x => x.EmailAddress == email).SingleOrDefault();

                return await Task.FromResult(user);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
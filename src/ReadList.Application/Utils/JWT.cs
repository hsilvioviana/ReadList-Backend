using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReadList.Application.ViewModels;

namespace ReadList.Application.Utils
{
    public class JWT
    {
        private string _secretKey;

        public JWT(IConfiguration configuration)
        {
            _secretKey = configuration["JWT:SecretKey"];
        }
        
        public string GenerateToken(UserViewModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("username", user.Username),
                    new Claim("email", user.Email)
                }),
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

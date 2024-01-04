using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using posts_cs.model;

namespace posts_cs.Controllers;

public interface IAuthService
{
    Task<string> AuthenticateAsync(string username, string password);
}

public class AuthService : IAuthService
{
    private static readonly byte[] SecretKey = Encoding.UTF8.GetBytes("tutajTwojSekretnyKlucz1234567890123456");

    private readonly List<User> _users = new List<User>
    {
        new User { Username = "user", Password = "user" },
    };
    

    public async Task<string> AuthenticateAsync(string username, string password)
    {
        var user = _users.SingleOrDefault(u => u.Username == username && u.Password == password);

        if (user == null)
            return null;

        var token = GenerateToken(user);
        return token;
    }

    private string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
            }),
            Expires = DateTime.UtcNow.AddHours(1), // Token ważny przez 1 godzinę
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecretKey), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}

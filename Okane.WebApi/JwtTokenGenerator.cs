using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Okane.Application;
using Okane.Domain;

namespace Okane.WebApi;

public class JwtTokenGenerator : ITokenGenerator
{
    private const string Key = "super-secret-key-this-must-be-in-a-file";

    public string Generate(User user)
    {
        var claims = CreateClaims(user);
        var credentials = SignCredentials();
        var token = CreateJwtToken(claims, credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static Claim[] CreateClaims(User user) =>
        new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

    private static JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials) =>
        new(
            issuer: "https://okane.com", // Replace with your issuer
            audience: "public", // Replace with your audience
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1), // Token expiration time
            signingCredentials: credentials
        );

    private SigningCredentials SignCredentials()
    {
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        return credentials;
    }
    
    public static SymmetricSecurityKey SymmetricSecurityKey() => 
        new(Encoding.ASCII.GetBytes(Key));
}
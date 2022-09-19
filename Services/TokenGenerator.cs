using System.Security.Claims;
using System.Text;
using BookLibraryApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BookLibraryApi.Generatetoken;

public class TokenGenerator
{
    private string _patern = "akndgakl98qauhf9qhwhasufh9ush0SHF08ASHFAUHSF8";
    public string GetToken(UserEntity user)
    {


        var Claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
            new Claim(ClaimTypes.Name , user.UserName),
            new Claim(ClaimTypes.Email , user.EmailAddress),
            new Claim(ClaimTypes.Role , user.role)
        };

        var SecurityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_patern)
        );

        var signCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);

        var descriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(Claims),
            Audience = "SampleJwtAudience",
            Issuer = "SampleJwtIssuer",
            IssuedAt = DateTime.Now,
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = signCredentials,
            CompressionAlgorithm = CompressionAlgorithms.Deflate
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(descriptor);

        return tokenHandler.WriteToken(securityToken);

    }
}
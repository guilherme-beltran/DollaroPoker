using Backoffice.Domain.Interfaces.Services;
using Backoffice.Domain.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backoffice.Infra.Services;

internal sealed class TokenService : ITokenService
{
    private readonly IConfiguration __configuration;

    public TokenService(IConfiguration configuration) => __configuration = configuration;

    public string GenerateToken(LoginResponse response)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(__configuration["Secrets:JwtPrivateKey"]);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GerarClaims(response),
            Expires = DateTime.Now.AddHours(4),
            SigningCredentials = credentials
        };
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GerarClaims(LoginResponse response)
    {
        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim("Id", response.Id.ToString()));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, response.Username));

        return claimsIdentity;
    }
}

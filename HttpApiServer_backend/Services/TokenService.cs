using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HttpModels;
using Microsoft.IdentityModel.Tokens;

namespace HttpApiServer_backend;

public class TokenService : ITokenService
{
    private readonly JwtConfig _jwtConfig;
    private readonly IClock _clock;
    public TokenService(JwtConfig jwtConfig, IClock clock)
    {
        _jwtConfig = jwtConfig;
        _clock = clock;
    }
    public string GenerateToken(Account account)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, account.Email)
            }),
            Expires = _clock.UtcNow.Add(_jwtConfig.LifeTime),
            Audience = _jwtConfig.Audience,
            Issuer = _jwtConfig.Issuer,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_jwtConfig.SigningKeyBytes),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
 
}
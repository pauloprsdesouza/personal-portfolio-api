using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Domain.Users;

namespace Portfolio.Api.Authorization
{
    public class ApiToken
    {
        private readonly JwtOptions _jwtOptions;

        public User User { get; set; }

        public ApiToken(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public override string ToString()
        {
            var token = new JwtSecurityToken(
                issuer: JwtOptions.Issuer,
                audience: JwtOptions.Audience,
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Secret)),
                    algorithm: SecurityAlgorithms.HmacSha256
                ),
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name, User.Id.ToString())
                }
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using Microsoft.IdentityModel.Tokens;

namespace QuoteManager.Filters
{
    public class AuthenticationProcess
    {
        private const string secretKey = "c2Rqa2Zoc2RpdWZoZW5rbGZuaXN1ZGhmYWlvbmsrKzIzNDIzYXNkZHNua2puYXNuMg==";

        public static string GenerateToken(string username, int expiration=20)
        {
            var symmetricKey = Convert.FromBase64String(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,username) 
                }),
                
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(symmetricKey), 
                    SecurityAlgorithms.HmacSha256Signature,
                    SecurityAlgorithms.Sha256Digest
                ),

                Expires = now.AddMinutes(Convert.ToInt32(expiration))

            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);
            return token;

        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(secretKey);

                var validationParameter = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)

                };

                var principal = tokenHandler.ValidateToken(token, validationParameter, out _);

                return principal;
            }

            catch(Exception)
            {
                return null;
            }
        }

    }
}
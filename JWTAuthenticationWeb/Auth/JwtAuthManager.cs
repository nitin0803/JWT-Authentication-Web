using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;

namespace JWTAuthentication.WebApi.Auth
{
    public class JwtAuthManager
    {
        public const string SecretKey = "JIOBLi6eVjBpvGtWBgJzjWd2QH0sOn5tI8rIFXSHKijXWEt/3J2jFYL79DQ1vKu+EtTYgYkwTluFRDdtF41yAQ==";

        public static string GenerateJWTToken(string userName, int expireInMinutes = 30)
        {
            var symmetricKey = Convert.FromBase64String(SecretKey);
            var symmetricSecurityKey = new SymmetricSecurityKey(symmetricKey);


            var now = DateTime.UtcNow;

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, userName)
                }),

                Expires = now.AddMinutes(Convert.ToInt32(expireInMinutes)),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(securityTokenDescriptor);
            var tokenString = tokenHandler.WriteToken(securityToken);

            return tokenString;
        }


        public static ClaimsPrincipal GetPrincipal(string tokenString)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(tokenString);

            if (jwtToken == null)
                return null;

            var symmetricKey = Convert.FromBase64String(SecretKey);
            var symmetricSecurityKey = new SymmetricSecurityKey(symmetricKey);

            var validationParameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = symmetricSecurityKey
            };

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(tokenString, validationParameters, out securityToken);
            return principal;
        }
    }
}
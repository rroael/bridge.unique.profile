using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Bridge.Unique.Profile.System.Models;
using Bridge.Unique.Profile.System.Settings;
using Microsoft.IdentityModel.Tokens;

namespace Bridge.Unique.Profile.System.Helpers
{
    public class JwtHelper
    {
        public JwtHelper(Jwt jwt)
        {
            SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt.Key));
            TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SymmetricSecurityKey,
                ValidAudience = jwt.Audience,
                ValidIssuer = jwt.Issuer
            };
            JwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SigningCredentials = new SigningCredentials(SymmetricSecurityKey, jwt.SecurityAlgorithm);
            Audience = jwt.Audience;
            Issuer = jwt.Issuer;
        }

        public JwtSecurityTokenHandler JwtSecurityTokenHandler { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public SymmetricSecurityKey SymmetricSecurityKey { get; set; }
        public TokenValidationParameters TokenValidationParameters { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }

        public string GetSecurityToken(SecurityTokenDescriptor securityTokenDescriptor)
        {
            securityTokenDescriptor.SigningCredentials = SigningCredentials;
            var securityToken = JwtSecurityTokenHandler.CreateJwtSecurityToken(securityTokenDescriptor);
            return JwtSecurityTokenHandler.WriteToken(securityToken);
        }

        public AuthUser ValidateToken(string token)
        {
            var result = new AuthUser();

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var claimsPrincipal =
                    tokenHandler.ValidateToken(token, TokenValidationParameters, out var validatedToken);
                if (validatedToken != null)
                {
                    var claimsList = claimsPrincipal.Claims.ToList();
                    var id = claimsList.Where(s => s.Type == "userId").FirstOrDefault()?.Value;
                    var applicationId = claimsList.Where(s => s.Type == "applicationId").FirstOrDefault()?.Value;
                    var clientId = claimsList.Where(s => s.Type == "clientId").FirstOrDefault()?.Value;
                    var profileId = claimsList.Where(s => s.Type == "profileId").FirstOrDefault()?.Value;
                    if (!string.IsNullOrWhiteSpace(id)) result.Id = Convert.ToInt32(id);
                    if (!string.IsNullOrWhiteSpace(applicationId))
                        result.ApplicationId = Convert.ToInt32(applicationId);
                    if (!string.IsNullOrWhiteSpace(clientId)) result.ClientId = Convert.ToInt32(clientId);
                    if (!string.IsNullOrWhiteSpace(profileId)) result.ProfileId = Convert.ToInt32(profileId);
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }
    }
}
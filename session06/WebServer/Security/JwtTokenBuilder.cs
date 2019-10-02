using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;

namespace WebServer.Security
{
    public sealed class JwtTokenBuilder
    {
        private SecurityKey _securityKey;
        private string _subject;
        private string _issuer;
        private string _audience;
        private static readonly Dictionary<string, string> _claims = new Dictionary<string, string>();
        public const int EXPIRE_MINUTES = 24 * 60 * 30;

        public const string CONFIGURATION_AUTHENTICATION_AUDIENCE_KEY = "CONFIGURATION_AUTHENTICATION_AUDIENCE_KEY";
        public const string CONFIGURATION_AUTHENTICATION_ISSUER_KEY = "CONFIGURATION_AUTHENTICATION_ISSUER_KEY";
        public const string CONFIGURATION_AUTHENTICATION_SHARED_SECRET_KEY = "CONFIGURATION_AUTHENTICATION_SHARED_SECRET_KEY";
        public const string CONFIGURATION_AUTHENTICATION_SUBJECT_KEY = "CONFIGURATION_AUTHENTICATION_SUBJECT_KEY";


        public const string BEARER_TOKEN_SCHEME = JwtBearerDefaults.AuthenticationScheme;

        public JwtTokenBuilder AddSecurityKey(SecurityKey securityKey)
        {
            _securityKey = securityKey;
            return this;
        }

        public static IEnumerable<Claim> GetClaims(string name)
        {
            var basicClaims = new Dictionary<string, string>();
            basicClaims.Add(ClaimTypes.Name, name);

            return _claims.Union(basicClaims)
                        .Select(item => new Claim(item.Key, item.Value))
                        .ToArray();
        }

        public JwtTokenBuilder AddSubject(string subject)
        {
            _subject = subject;
            return this;
        }

        internal static JwtSecurityToken GetSecuredToken(IConfiguration configuration)
        {
            var token = new JwtTokenBuilder()
                .AddSecurityKey(GetSecurityKey(configuration[CONFIGURATION_AUTHENTICATION_SHARED_SECRET_KEY]))
                                .AddSubject(configuration[CONFIGURATION_AUTHENTICATION_SUBJECT_KEY])
                                .AddIssuer(configuration[CONFIGURATION_AUTHENTICATION_ISSUER_KEY])
                                .AddAudience(configuration[CONFIGURATION_AUTHENTICATION_AUDIENCE_KEY])
                                .Build();

            return token;
        }

        
        internal static string WriteToken(JwtSecurityToken token)
        {
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        public JwtTokenBuilder AddIssuer(string issuer)
        {
            _issuer = issuer;
            return this;
        }

        public JwtTokenBuilder AddAudience(string audience)
        {
            _audience = audience;
            return this;
        }

        public JwtTokenBuilder AddClaim(string type, string value)
        {
            _claims.Add(type, value);
            return this;
        }

        public JwtTokenBuilder AddClaims(Dictionary<string, string> claims)
        {
            _claims.Union(claims);
            return this;
        }

        public JwtSecurityToken Build()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, this._subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
                .Union(_claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                            issuer: _issuer,
                            audience: _audience,
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(EXPIRE_MINUTES),
                            signingCredentials: new SigningCredentials(
                                                        _securityKey,
                                                        SecurityAlgorithms.HmacSha256));
            return token;
        }

        public static bool ValidateToken(string token, string authorizationKey)
        {
            SecurityToken validatedToken;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            if (handler.CanReadToken(token))
            {
                return false;
            }
            var options = new TokenValidationParameters
            {
                IssuerSigningKey = GetSecurityKey(authorizationKey),
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuer = false
            };

            try
            {
                var user = handler.ValidateToken(token.Replace($"{BEARER_TOKEN_SCHEME} ", string.Empty), options, out validatedToken);

                return user != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Bearer token \"{0}\". Exception: {1}", token, ex);
                return false;
            }
        }

        public static SymmetricSecurityKey GetSecurityKey(string secret)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            return key;
        }

        #region Private Methods

        private void EnsureArguments()
        {
            if (this._securityKey == null)
                throw new ArgumentNullException($"{nameof(JwtTokenBuilder)}:Security Key");

            if (string.IsNullOrEmpty(this._subject))
                throw new ArgumentNullException($"{nameof(JwtTokenBuilder)}:Subject");

            if (string.IsNullOrEmpty(this._issuer))
                throw new ArgumentNullException($"{nameof(JwtTokenBuilder)}:Issuer");

            if (string.IsNullOrEmpty(this._audience))
                throw new ArgumentNullException($"{nameof(JwtTokenBuilder)}:Audience");
        }

        #endregion
    }
}
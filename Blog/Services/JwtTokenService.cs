using System.IdentityModel.Tokens.Jwt; //Provides classes and functionalities to Create,validate, and handle JSON Web Tokens(JWTs). 
using System.Security.Claims; //Contains classes that implement claims-based identity in .NET, including classes that represent claims, claims-based identities, and claims-based principals
using System.Text; //classes for character encoding, conversions, and string manipulation
using Blog.Models; //Models directory
using Blog.Services;
using Microsoft.Extensions.Configuration; //Allows apps to store and retrieve configuration data from various sources such as files, environment variables, command-line arguements,user secrets
using Microsoft.IdentityModel.Tokens; //contains base classes such as securityToken, securityTokenHandler, and SecurityKeyIdentifierClause 

namespace Blog.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;

        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }

        public string CreateToken(AppUser user)
        {
            var claims = new[] { //claims are like meta data to the token, who the user is and what their role is. Its included in the token's payload.
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)); //grabs secret key from appsettings.json and turns into bytes for hash-based signing(HMAC)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);//Use HMAC SHA-256 to sign the JWT using this key

            var token = new JwtSecurityToken( //creates the token
                issuer: _config["Jwt:Issuer"], //sets Issuer
                audience: _config["Jwt:Audience"],//sets audience
                claims: claims, //adds claims from above
                expires: DateTime.UtcNow.AddHours(2), //Sets expiry to 2 hours
                signingCredentials: creds //adds signing credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token); //Serializes it into a compact JWT string like eyJhbGciOiJIUzI1NiIsInR5cCI6...
        }
    }
}
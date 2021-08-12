using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }
        //[HttpGet]
        //public IActionResult GetToken(string name, string pwd)
        //{
        //    if (name == "fati" && pwd == "33333")
        //    {
        //        var now = DateTime.UtcNow;

        //        var claims = new Claim[]
        //        {
        //    new Claim(JwtRegisteredClaimNames.Sub, name),
        //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //    new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
        //        };

        //        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Y2F0Y2hlciUyMHdvbmclMjBsb3ZlJTIwLm5ldA=="));
        //        var tokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = signingKey,
        //            ValidateIssuer = true,
        //            ValidIssuer = "localhost",
        //            ValidateAudience = true,
        //            ValidAudience = "Fatih Torman",
        //            ValidateLifetime = true,
        //            ClockSkew = TimeSpan.Zero,
        //            RequireExpirationTime = true,

        //        };

        //        var jwt = new JwtSecurityToken(
        //            issuer: "localhost",
        //            audience: "Fatih Torman",
        //            claims: claims,
        //            notBefore: now,
        //            expires: now.Add(TimeSpan.FromMinutes(10000)),
        //            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
        //        );
        //        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        //        var responseJson = new
        //        {
        //            access_token = encodedJwt,
        //            expires_in = (int)TimeSpan.FromMinutes(10000).TotalSeconds
        //        };

        //        return Ok(responseJson);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
        [HttpGet]
        public string GetToken(string name, string pwd)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Iss"],
              _config["Jwt:Aud"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public class Audience
        {
            public string Secret { get; set; }
            public string Iss { get; set; }
            public string Aud { get; set; }
        }
    }

}

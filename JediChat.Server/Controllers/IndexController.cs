using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using JediChat.Server.Configuration;
using JediChat.Server.Extensions;
using JediChat.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace JediChat.Server.Controllers
{
    public class IndexController : ControllerBase
    {
        private readonly JwtIssuerOptions _jwtIssuerOptions;

        public IndexController(IOptions<JwtIssuerOptions> jwtIssuerOptions)
        {
            _jwtIssuerOptions = jwtIssuerOptions.Value;
        }

        [HttpGet("/user")]
        public IActionResult Index()
        {            
            var info = new UserInfoViewModel
            {
                Id = User.GetUserId(),
                Name = User.GetUserName()
            };
            return Ok(info);
        }

        [HttpPost("/generate-token")]
        public async Task<IActionResult> Token([FromBody]JwtTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var identity = GenerateClaimsIdentity(request.Name, request.Name);
            var jwt = new JwtTokenResponse
            {
                Id = identity.Claims.Single(c=>c.Type == Constants.UserIdClaimTypeName).Value,
                AccessToken = await GenerateEncodedToken(request.Name, identity),
                ExpiresIn = (long) _jwtIssuerOptions.ValidFor.TotalSeconds
            };

            return Ok(jwt);
        }

        private ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(Constants.UserIdClaimTypeName, id),
                new Claim(Constants.UserNameClaimTypeName, userName)
            });
        }

        private async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtIssuerOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtIssuerOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),                
                identity.FindFirst(Constants.UserIdClaimTypeName),
                identity.FindFirst(Constants.UserNameClaimTypeName)
            };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtIssuerOptions.Issuer,
                audience: _jwtIssuerOptions.Audience,
                claims: claims,
                notBefore: _jwtIssuerOptions.NotBefore,
                expires: _jwtIssuerOptions.Expiration,
                signingCredentials: _jwtIssuerOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() -
                                 new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }
}
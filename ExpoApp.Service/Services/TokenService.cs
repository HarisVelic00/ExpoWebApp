using ExpoApp.Service.Interfaces;
using ExpoApp.Service.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<IdentityUser> UserManager;
        private readonly IConfiguration Configuration;

        public TokenService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            UserManager = userManager;
            Configuration = configuration;
        }

        public async Task<TokenResponse> BuildToken(IdentityUser user)
        {
            var claims = new List<Claim>()
            {
                //new Claim("Id", user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var userClaims = await UserManager.GetClaimsAsync(user);
            var userRolesAsClaims = await UserRoles(user);

            claims.AddRange(userClaims);
            claims.AddRange(userRolesAsClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiriation = DateTime.UtcNow.AddHours(5);

            var token = new JwtSecurityToken(
                issuer: null, audience: null, claims: claims, expires: expiriation, signingCredentials: creds);

            TokenResponse authResponse = new TokenResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiriation = expiriation
            };

            return authResponse;

        }

        private async Task<List<Claim>> UserRoles(IdentityUser user)
        {
            var userRoles = await UserManager.GetRolesAsync(user);

            List<Claim> rolesToClaims = new List<Claim>();

            foreach (var role in userRoles)
            {
                rolesToClaims.Add(new Claim("role", role));
            }

            return rolesToClaims;

        }
    }
}


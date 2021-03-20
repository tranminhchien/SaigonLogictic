using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Logictics.Service.ViewModel;
using Logictics.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Logictics.Web.Controllers
{
    public class BaseController : Controller
    {
        public async Task CreateAuthenticationTicket(UserViewModel user)
        {
            var key = Encoding.ASCII.GetBytes(Sitekeys.Token);
            var JWToken = new JwtSecurityToken(
                issuer: Sitekeys.WebSiteDomain,
                audience: Sitekeys.WebSiteDomain,
                claims: GetUserClaims(user),
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddDays(366)).DateTime,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );
            var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
            HttpContext.Session.SetString("JWToken", token);
            HttpContext.Session.SetString("UserId", user.Id);
        }


        private IEnumerable<Claim> GetUserClaims(UserViewModel user)
        {
            List<Claim> claims = new List<Claim>();
            Claim _claim;
            _claim = new Claim(ClaimTypes.Name, user.UserName);
            claims.Add(_claim);

            _claim = new Claim("Role", Role.Admin);
            claims.Add(_claim);

            return claims.AsEnumerable<Claim>();
        }
       
    }
}

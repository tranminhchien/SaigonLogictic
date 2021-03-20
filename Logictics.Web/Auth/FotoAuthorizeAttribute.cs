using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Logictics.Web.Auth
{

    public class LogicticsAuthorizeAttribute : TypeFilterAttribute
    {
        public LogicticsAuthorizeAttribute(params string[] claim) : base(typeof(LogicticsAuthorizeFilter))
        {
            Arguments = new object[] { claim };
        }
    }

    public class LogicticsAuthorizeFilter : IAuthorizationFilter
    {
        readonly string[] _claim;

        public LogicticsAuthorizeFilter(params string[] claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;

            if (IsAuthenticated)
            {
                bool flagClaim = false;
                foreach (var item in _claim)
                {
                    if (context.HttpContext.User.HasClaim("Role", item))
                    {
                        flagClaim = true;
                    }
                }
                if (!flagClaim)
                {
                    context.Result = new RedirectResult("~/Auth/AccessDenied");
                }
            }
            else
            {
                context.Result = new RedirectResult("~/Auth/Login");
            }
            return;
        }

    }
}

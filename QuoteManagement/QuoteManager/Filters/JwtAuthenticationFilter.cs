using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Mvc.Filters;
using IAuthenticationFilter = System.Web.Http.Filters.IAuthenticationFilter;

namespace QuoteManager.Filters
{
    public class JwtAuthenticationFilter:Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; }
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null || authorization.Scheme != "Bearer")
                return;

            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new FailedAuthenticationResult("Missing Token",request);
                return;

            }

            var token = authorization.Parameter;
            var principal = await AuthenticateJwtToken(token);

            if (principal == null)
                context.ErrorResult=new FailedAuthenticationResult("Invalid Token",request);
            else
            {
                context.Principal = principal;
            }
            


        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        private static bool ValidateToken(string token, out string username)
        {
            username = null;

            var simplePrincipal = AuthenticationProcess.GetPrincipal(token);
            var identity = simplePrincipal?.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim?.Value;

            if (String.IsNullOrEmpty(username))
                return false;



            return true;
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token)
        {
            if(ValidateToken(token, out var username))
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,username)
                };

                var identity = new ClaimsIdentity(claims,"Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);

                return Task.FromResult(user);


            }

            return Task.FromResult<IPrincipal>(null);
        }




    }
}
using JWTAuthentication.WebApi.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace JWTAuthentication.WebApi.Filters
{
    public class JwtAuthenticationFilter : Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; }
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            if (authorization?.Scheme != "Bearer" || string.IsNullOrEmpty(authorization?.Parameter))
            {
                context.ErrorResult = new AuthFailureResult("JWT token is missing!!", request);
                return;
            }

            var tokenString = authorization.Parameter;
            var principal = await AuthJwtToken(tokenString);

            if (principal == null)
            {
                context.ErrorResult = new AuthFailureResult("Invalid JWT Token", request);
            }
            else
            {
                context.Principal = principal;
            }
        }

        private Task<IPrincipal> AuthJwtToken(string tokenString)
        {
            string username;
            if (ValidateTokenHasUsername(tokenString, out username))
            {

                var claimsList = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username)
                    };

                var identity = new ClaimsIdentity(claims: claimsList, authenticationType: "jwt");

                IPrincipal user = new ClaimsPrincipal(identity);
                return Task.FromResult(user);
            }

            return Task.FromResult<IPrincipal>(null);
        }


        private static bool ValidateTokenHasUsername(string tokenString, out string username)
        {
            username = null;
            var principal = JwtAuthManager.GetPrincipal(tokenString);
            if (principal == null)
                return false;

            var identity = principal.Identity as ClaimsIdentity;
            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim?.Value;
            if (string.IsNullOrEmpty(username))
                return false;

            // You can implement more validation to check whether username exists in your DB or not or something else. 
            return true;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter = null;

            if (!string.IsNullOrEmpty(Realm))
                parameter = "realm=\"" + Realm + "\"";

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var scheme = "Bearer";
            var challenge = new AuthenticationHeaderValue(scheme, parameter);

            context.Result = new Auth.UnauthorizedResult(challenge, context.Result);
        }

    }
}
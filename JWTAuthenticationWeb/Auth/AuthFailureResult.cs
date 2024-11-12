using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace JWTAuthentication.WebApi.Auth
{
    public class AuthFailureResult : IHttpActionResult
    {
        public string ReasonPhrase { get; set; }
        public HttpRequestMessage Request { get; set; }

        public AuthFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            this.ReasonPhrase = reasonPhrase;
            this.Request = request;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            return new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage = this.Request,
                ReasonPhrase = this.ReasonPhrase
            };
        }
    }
}
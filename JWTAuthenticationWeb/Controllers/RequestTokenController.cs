using JWTAuthentication.WebApi.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace JWTAuthentication.WebApi.Controllers
{
    public class RequestTokenController : ApiController
    {
        public IHttpActionResult Get(string username, string password)
        {
            if (this.CheckCredentials(username, password))
            {
                return Ok(JwtAuthManager.GenerateJWTToken(username));
            }
            else
            {
                return NotFound();
            }
        }

        public bool CheckCredentials(string username, string password)
        {
            if (username == "codeadda" && password == "abc123")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

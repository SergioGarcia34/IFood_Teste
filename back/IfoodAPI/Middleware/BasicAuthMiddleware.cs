using IfoodAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IfoodAPI.Middleware
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;

        private ValidacaoServices _validacaoExterna;

        public BasicAuthMiddleware(RequestDelegate next, ValidacaoServices validacaoExterna)
        {
            _validacaoExterna = validacaoExterna;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Value == "/api/login")
            {

                string authHeader = httpContext.Request.Headers["Authorization"];
                if (authHeader != null)
                {
                    string auth = authHeader.Split(new char[] { ' ' })[1];
                    Encoding encoding = Encoding.GetEncoding("UTF-8");
                    var usernameAndPassword = encoding.GetString(Convert.FromBase64String(auth));
                    string username = usernameAndPassword.Split(new char[] { ':' })[0];
                    string password = usernameAndPassword.Split(new char[] { ':' })[1];

                    httpContext.Response.StatusCode = await _validacaoExterna.ValidarLogin(username, password);
                    
                    if (httpContext.Response.StatusCode == 200)
                    {
                        await _next(httpContext);

                    }
                    else
                    {
                        return ;
                    }

                }
                else
                {
                    httpContext.Response.StatusCode = 401;
                    return;
                }
            }
            else
            {
                await _next(httpContext);
            }
        }
        
    }

    public static class BasicAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseBasicAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BasicAuthMiddleware>();
        }
    }
}

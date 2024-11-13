using Microsoft.AspNetCore.Builder;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyArchitectV2Lab1.JWTMiddleware1
{
    public static class JWTMiddlewareManager
    {
        public static IApplicationBuilder UseJWTAuthorizedMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtService>();
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ValidacaoDeSenha.Tests
{
    public class EnderecoMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IPAddress fakeIPAddress = IPAddress.Parse("172.190.1.10");

        public EnderecoMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Connection.RemoteIpAddress = fakeIPAddress;

            await this.next(httpContext);
        }
    }
}

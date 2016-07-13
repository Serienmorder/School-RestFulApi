using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using IdentityServer3.AccessTokenValidation;
using System.Web.Http;
using System.Security.Cryptography.X509Certificates;

[assembly: OwinStartup(typeof(Api.Startup))]

namespace Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                //Authority = "http://localhost:13103"
                Authority = "http://dotnet1798.cloudapp.net:8080"
            });
         
        }
    }
}

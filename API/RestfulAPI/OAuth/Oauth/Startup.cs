using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityServer3.Core.Configuration;
using IdentityServer3.EntityFramework;
using Microsoft.Owin;
using Owin;
using IdentityServer3.EntityFramework;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using Oauth.Models;

[assembly: OwinStartup(typeof(Oauth.Startup))]

namespace Oauth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var entityFrameworkOptions = new EntityFrameworkServiceOptions
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["oauthConnection"].ConnectionString
            };
            var certificate = Convert.FromBase64String(ConfigurationManager.AppSettings["SigningCertificate"]);
            var inMemoryManager = new InMemoryManager();

         //   SetupClients(inMemoryManager.GetClients(), entityFrameworkOptions);
         //   SetupScopes(inMemoryManager.GetScopes(), entityFrameworkOptions);
            oauthConnection db = new oauthConnection();

            var blah = db;
            var factory = new IdentityServerServiceFactory()
                .UseInMemoryUsers(inMemoryManager.GetUsers())
                .UseInMemoryScopes(inMemoryManager.GetScopes())
                .UseInMemoryClients(inMemoryManager.GetClients());
            //factory.RegisterConfigurationServices(entityFrameworkOptions);
            //factory.RegisterOperationalServices(entityFrameworkOptions);
            factory.UserService = new Registration<IUserService>(typeof(TasqBookUserService));
            factory.Register(new Registration<oauthConnection>(db));
            new TokenCleanup(entityFrameworkOptions, 1).Start();
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            var options = new IdentityServerOptions
            {
                SigningCertificate = new X509Certificate2(certificate,ConfigurationManager.AppSettings["SigningCertificatePassword"]),
                RequireSsl = false,
                Factory = factory
            };
            app.UseIdentityServer(options);
        }
        public void SetupClients(IEnumerable<Client> clients,
                            EntityFrameworkServiceOptions options)
        {
            using (var context =
                new ClientConfigurationDbContext(options.ConnectionString,
                                                options.Schema))
            {
                if (context.Clients.Any()) return;

                foreach (var client in clients)
                {
                    context.Clients.Add(client.ToEntity());
                }

                context.SaveChanges();
            }
        }
        public void SetupScopes(IEnumerable<Scope> scopes,
                                 EntityFrameworkServiceOptions options)
        {
            using (var context =
                new ScopeConfigurationDbContext(options.ConnectionString,
                                                options.Schema))
            {
                if (context.Scopes.Any()) return;

                foreach (var scope in scopes)
                {
                    context.Scopes.Add(scope.ToEntity());
                }

                context.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using System.Threading.Tasks;
using Oauth.Helpers;
using Oauth.Models;
using System.Data.Entity;


namespace Oauth
{
    public class TasqBookUserService : UserServiceBase
    {
        private readonly oauthConnection db;
        public TasqBookUserService(oauthConnection db)
        {
            this.db = db;
        }
        public override async Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            //var user = await db.idents.FindAsync(context.UserName);
            var user = await db.idents.Where(x => x.username == context.UserName).ToListAsync();
            var hash = HashHelper.Sha512(context.UserName, context.Password);

            if (user != null && user.FirstOrDefault().password == hash)
            {
                context.AuthenticateResult = new AuthenticateResult(context.UserName, context.UserName);
            }
            else
            {
                context.AuthenticateResult = new AuthenticateResult("Incorrect Credentials");
                return;
            }
            //var user = await userRepository.GetAsync();
        }

    }
}
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Clinica.API.Startup))]
namespace Clinica.API
{
    using Clinica.API.Infrastucture.Resolvers;
    using Clinica.API.Providers;
    using Clinica.Services.Services.Services.Interfaces;
    using Microsoft.Owin.Cors;
    using Microsoft.Owin.Security.OAuth;
    using Owin;
    using System;
    using System.Web.Http;

    public class Startup
    {
        //public readonly IUsuariosService myUsuariosService;

        //public Startup(IUsuariosService usuariosService)
        //{
        //    this.myUsuariosService = usuariosService;
        //}

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
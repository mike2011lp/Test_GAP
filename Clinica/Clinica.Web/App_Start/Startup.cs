using Microsoft.Owin;

[assembly: OwinStartup(typeof(Clinica.Web.App_Start.Startup))]
namespace Clinica.Web.App_Start
{
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using Owin;
    using System;
    using System.Security.Claims;
    using System.Web.Helpers;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Usuarios"),
            });

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}
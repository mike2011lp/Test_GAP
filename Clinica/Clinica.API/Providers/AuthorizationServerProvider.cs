namespace Clinica.API.Providers
{
    using AutoMapper;
    using Clinica.DataAccess.Context;
    using Clinica.DataAccess.Entities;
    using Clinica.Recursos.ResourceFiles;
    using Clinica.Services.Mapping;
    using Clinica.Services.Models;
    using Clinica.Services.Repositories;
    using Clinica.Services.Services.Services.Implementation;
    using Clinica.Services.Services.Services.Interfaces;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security.OAuth;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>
    /// Server authorization provider
    /// </summary>
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        #region Fields
        private IUsuariosService myUsuarioService;

        private DataContext myContext;

        private UserManager<Usuario, string> myUserManager;

        private UserStore<Usuario> myStore
        {
            get
            {
                return new UserStore<Usuario>(this.myContext);
            }
        }

        private IMapper myMapper;

        private IIdentityRepository myRepository;

        private UserValidator<Usuario> myValidator;

        private IPasswordHasher myHasher;

        #endregion

        #region Construction
        public AuthorizationServerProvider()
        {
            this.myContext = new DataContext();
            this.myUserManager = new UserManager<Usuario, string>(this.myStore);
            this.myMapper = new AutoMapperBase().Mapper;
            this.myValidator = new UserValidator<Usuario>(this.myUserManager);
            this.myRepository = new IdentityRepository(this.myUserManager);
            this.myHasher = new PasswordHasher();

            this.myUsuarioService = new UsuariosService(this.myRepository, this.myMapper, this.myValidator, this.myHasher);
        }
        #endregion

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            UsuarioModel user = await this.myUsuarioService.FindUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", Messages.MSG_AUTH_INVALID_CREDENTIALS);
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

            if (!string.IsNullOrEmpty(user.Email))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            }

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

            context.Validated(identity);
        }
    }
}
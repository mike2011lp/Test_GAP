namespace Clinica.API
{
    using AutoMapper;
    using Clinica.API.Controllers;
    using Clinica.API.Infrastucture.Resolvers;
    using Clinica.DataAccess.Context;
    using Clinica.DataAccess.Entities;
    using Clinica.Services.Handlers;
    using Clinica.Services.Mapping;
    using Clinica.Services.Repositories;
    using Clinica.Services.Services.Implementation;
    using Clinica.Services.Services.Interfaces;
    using Clinica.Services.Services.Services.Implementation;
    using Clinica.Services.Services.Services.Interfaces;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Web.Http;
    using Unity;
    using Unity.Lifetime;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Unity DI
            var container = new UnityContainer();

            //Custom instances
            var mapper = new AutoMapperBase().Mapper;
            var userManager = new UserManager<Usuario>(new UserStore<Usuario>(new DataContext()));
            var userValidator = new UserValidator<Usuario>(userManager);
            var identityRepository = new IdentityRepository(userManager);
            var hasher = new PasswordHasher();

            //Register types
            container.RegisterType<IErrorHandler, ErrorHandler>(new HierarchicalLifetimeManager());
            container.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            container.RegisterType(typeof(IBaseService<>), typeof(BaseService<>));
            container.RegisterType(typeof(IIdentityValidator<>), typeof(UserValidator<>));
            container.RegisterType(typeof(IUserStore<>), typeof(UserStore<>));
            container.RegisterType<IIdentityRepository, IdentityRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPasswordHasher, PasswordHasher>(new HierarchicalLifetimeManager());
            container.RegisterType<IUsuariosService, UsuariosService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUbicacionService, UbicacionService>(new HierarchicalLifetimeManager());
            container.RegisterType<IParametroService, ParametroService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICitaService, CitaService>(new HierarchicalLifetimeManager());

            //Register instances
            container.RegisterInstance<IMapper>(mapper);
            container.RegisterInstance<IdentityRepository>(identityRepository);
            container.RegisterInstance<UserValidator<Usuario>>(new UserValidator<Usuario>(userManager));
            container.RegisterInstance<UsuariosService>(new UsuariosService(identityRepository, mapper, userValidator, hasher));

            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

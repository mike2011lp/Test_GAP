namespace Clinica.API
{
    using AutoMapper;
    using Clinica.API.Controllers;
    using Clinica.API.Infrastucture.Resolvers;
    using Clinica.Services.Handlers;
    using Clinica.Services.Mapping;
    using Clinica.Services.Repositories;
    using Clinica.Services.Services.Implementation;
    using Clinica.Services.Services.Interfaces;
    using Clinica.Services.Services.Services.Implementation;
    using Clinica.Services.Services.Services.Interfaces;
    using System.Web.Http;
    using Unity;
    using Unity.Lifetime;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Unity DI
            var container = new UnityContainer();
            container.RegisterType<IErrorHandler, ErrorHandler>(new HierarchicalLifetimeManager());
            container.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            container.RegisterType(typeof(IBaseService<>), typeof(BaseService<>));
            container.RegisterInstance<IMapper>(new AutoMapperBase().Mapper);

            container.RegisterType<IUsuariosService, UsuariosService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUbicacionService, UbicacionService>(new HierarchicalLifetimeManager());
            container.RegisterType<IParametroService, ParametroService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICitaService, CitaService>(new HierarchicalLifetimeManager());
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

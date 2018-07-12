//using AutoMapper;
//using Clinica.DataAccess.Entities;
//using Clinica.Services.Handlers;
//using Clinica.Services.Mapping;
//using Clinica.Services.Repositories;
//using Clinica.Services.Services.Implementation;
//using Clinica.Services.Services.Interfaces;
//using Clinica.Services.Services.Services.Implementation;
//using Clinica.Services.Services.Services.Interfaces;
//using System;

//using Unity;
//using Unity.Injection;
//using Unity.Lifetime;

//namespace Clinica.API
//{
//    /// <summary>
//    /// Specifies the Unity configuration for the main container.
//    /// </summary>
//    public static class UnityConfig
//    {
//        #region Unity Container
//        private static Lazy<IUnityContainer> container =
//          new Lazy<IUnityContainer>(() =>
//          {
//              var container = new UnityContainer();
//              RegisterTypes(container);
//              return container;
//          });

//        /// <summary>
//        /// Configured Unity Container.
//        /// </summary>
//        public static IUnityContainer Container => container.Value;
//        #endregion

//        /// <summary>
//        /// Registers the type mappings with the Unity container.
//        /// </summary>
//        /// <param name="container">The unity container to configure.</param>
//        /// <remarks>
//        /// There is no need to register concrete types such as controllers or
//        /// API controllers (unless you want to change the defaults), as Unity
//        /// allows resolving a concrete type even if it was not previously
//        /// registered.
//        /// </remarks>
//        public static void RegisterTypes(IUnityContainer container)
//        {
//            // NOTE: To load from web.config uncomment the line below.
//            // Make sure to add a Unity.Configuration to the using statements.
//            // container.LoadConfiguration();

//            /*container.RegisterType<IErrorHandler>(new HierarchicalLifetimeManager(),
//                new InjectionFactory(c => new ErrorHandler()));

//            container.RegisterType<IParametroService>(new HierarchicalLifetimeManager(),
//                new InjectionFactory(c => new ParametroService()));*/

//            container.RegisterType<IErrorHandler, ErrorHandler>(new HierarchicalLifetimeManager());
//            container.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>));
//            container.RegisterType(typeof(IBaseService<>), typeof(BaseService<>));
//            container.RegisterInstance<IMapper>(new AutoMapperBase().Mapper);
//            //service, IMapper Mapper

//            container.RegisterType<IUsuariosService, UsuariosService>(new HierarchicalLifetimeManager());
//            container.RegisterType<IUbicacionService, UbicacionService>(new HierarchicalLifetimeManager());
//            container.RegisterType<IParametroService, ParametroService>(new HierarchicalLifetimeManager());
//            container.RegisterType<ICitaService, CitaService>(new HierarchicalLifetimeManager());

//            // TODO: Register your type's mappings here.
//            // container.RegisterType<IProductRepository, ProductRepository>();
//        }
//    }
//}
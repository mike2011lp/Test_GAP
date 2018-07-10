namespace Clinica.DataAccess.Context
{
    using Clinica.Constantes;
    using Clinica.DataAccess.Entities;
    using Clinica.DataAccess.Mappers;
    using Clinica.DataAccess.Migrations;
    using System.Data.Entity;

    /// <summary>
    /// Contexto de base de datos para la aplicación
    /// </summary>
    public class DataContext : DbContext
    {
        #region Construction
        public DataContext() : base(DataAccessConstants.CONN_STR_NAME)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            //Configuratión de migración
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, DataContextMigrationConfig>());
        }
        #endregion

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Cita> Citas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UsuarioMapper());
            modelBuilder.Configurations.Add(new RolMapper());
            modelBuilder.Configurations.Add(new ParametroMapper());
            modelBuilder.Configurations.Add(new UbicacionMapper());
            modelBuilder.Configurations.Add(new CitaMapper());

            base.OnModelCreating(modelBuilder);
        }
    }
}

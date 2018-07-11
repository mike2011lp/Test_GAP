namespace Clinica.DataAccess.Context
{
    using Clinica.Constantes;
    using Clinica.DataAccess.Entities;
    using Clinica.DataAccess.Mappers;
    using Clinica.DataAccess.Migrations;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Contexto de base de datos para la aplicación
    /// </summary>
    public class DataContext : IdentityDbContext<Usuario, Rol, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        #region Construction
        public DataContext() : base(DataAccessConstants.CONN_STR_NAME)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            //Configuratión de migración
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
        }
        #endregion

        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Cita> Citas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UsuarioMapper());
            modelBuilder.Configurations.Add(new ParametroMapper());
            modelBuilder.Configurations.Add(new UbicacionMapper());
            modelBuilder.Configurations.Add(new CitaMapper());

            modelBuilder.Entity<Usuario>();
            modelBuilder.Entity<Rol>();
            modelBuilder.Entity<Parametro>();
            modelBuilder.Entity<Ubicacion>();
            modelBuilder.Entity<Cita>();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                    );
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                    );
            }
        }
    }
}

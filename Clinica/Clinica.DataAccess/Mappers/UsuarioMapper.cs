namespace Clinica.DataAccess.Mappers
{
    using Clinica.DataAccess.Entities;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Mapper para la entidad Usuario
    /// </summary>
    class UsuarioMapper : EntityTypeConfiguration<Usuario>
    {
        #region Constants
        const string NOM_TBL_USUARIO = "Usuarios";
        #endregion

        #region Construction
        public UsuarioMapper()
        {
            //Especificacion sobre nombre de tabla
            this.ToTable(NOM_TBL_USUARIO);

            //Llave primaria
            this.HasKey(u => u.Id);

            //Definicion sobre campos requeridos
            this.Property(u => u.NumeroIdentificacion).IsRequired();
            this.Property(u => u.Nombres).IsRequired();
            this.Property(u => u.Apellidos).IsRequired();
            this.Property(u => u.Contrasena).IsRequired();

            //Campos opcionales
            this.Property(u => u.Telefono).IsOptional();
            this.Property(u => u.FechaNacimiento).IsOptional();
            this.Property(u => u.FechaCreacion).IsOptional();

            //Definición de largo para aquellos campos que lo requieren
            this.Property(u => u.NumeroIdentificacion).HasMaxLength(25);
            this.Property(u => u.Nombres).HasMaxLength(100);
            this.Property(u => u.Apellidos).HasMaxLength(100);
            this.Property(u => u.Contrasena).HasMaxLength(50);
            this.Property(u => u.Telefono).HasMaxLength(50);


            //Definición de llaves foraneas
            this.HasRequired(u => u.TipoIdentificacion).WithMany().Map(i => i.MapKey("TipoIdentificacion"));
            this.HasRequired(u => u.TipoUsuario).WithMany().Map(t => t.MapKey("TipoUsuario"));
            this.HasRequired(u => u.Estado).WithMany().Map(t => t.MapKey("Estado"));

            //Otras relaciones
            this.Property(u => u.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
        #endregion
    }
}

namespace Clinica.DataAccess.Mappers
{
    using Clinica.DataAccess.Entities;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Mapper para la entidad rol
    /// </summary>
    class RolMapper : EntityTypeConfiguration<Rol>
    {
        #region Constants
        const string NOM_TBL_ROL = "Roles";
        #endregion

        #region Construction
        public RolMapper()
        {
            //Especificacion sobre nombre de tabla
            this.ToTable(NOM_TBL_ROL);

            //Llave primaria
            this.HasKey(r => r.Codigo);

            //Definicion sobre campos requeridos
            this.Property(r => r.Description).IsRequired();

            //Definición de largo para aquellos campos que lo requieren
            this.Property(r => r.Codigo).HasMaxLength(15);
            this.Property(r => r.Description).HasMaxLength(50);

            //Otras relaciones
            this.Property(r => r.Codigo).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
        #endregion
    }
}

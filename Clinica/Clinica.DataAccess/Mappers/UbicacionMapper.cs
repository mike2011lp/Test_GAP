namespace Clinica.DataAccess.Mappers
{
    using Clinica.DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Mapper para la entidad ubicacion
    /// </summary>
    class UbicacionMapper: EntityTypeConfiguration<Ubicacion>
    {
        #region Constants
        const string NOM_TBL_UBICACION = "Ubicaciones";
        #endregion

        #region Construction
        public UbicacionMapper()
        {
            //Especificacion sobre nombre de tabla
            this.ToTable(NOM_TBL_UBICACION);

            //Llave primaria
            this.HasKey(u => u.Codigo);

            //Definicion sobre campos requeridos
            this.Property(u => u.Descripcion).IsRequired();

            //Campos opcionales
            this.Property(u => u.Direccion).IsOptional();

            //Definición de largo para aquellos campos que lo requieren
            this.Property(u => u.Descripcion).HasMaxLength(100);
            this.Property(u => u.Direccion).HasMaxLength(100);
        }
        #endregion
    }
}

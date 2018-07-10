namespace Clinica.DataAccess.Mappers
{
    using Clinica.DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Mapper para la entidad Parametro
    /// </summary>
    class ParametroMapper : EntityTypeConfiguration<Parametro>
    {
        #region Constants
        const string NOM_TBL_PARAM = "Parametros";
        #endregion

        #region Construction
        public ParametroMapper()
        {
            //Especificacion sobre nombre de tabla
            this.ToTable(NOM_TBL_PARAM);

            //Llave primaria
            this.HasKey(p => new { p.Categoria, p.Codigo } );

            //Definicion sobre campos requeridos
            this.Property(p => p.Categoria).IsRequired();
            this.Property(p => p.Codigo).IsRequired();
            this.Property(p => p.ValorPrincipal).IsRequired();

            //Campos opcionales
            this.Property(p => p.ValorSecundario).IsOptional();

            //Definición de largo para aquellos campos que lo requieren
            this.Property(p => p.Categoria).HasMaxLength(20);
            this.Property(p => p.Codigo).HasMaxLength(20);
            this.Property(p => p.ValorPrincipal).HasMaxLength(100);
            this.Property(p => p.ValorSecundario).HasMaxLength(100);
        }
        #endregion
    }
}

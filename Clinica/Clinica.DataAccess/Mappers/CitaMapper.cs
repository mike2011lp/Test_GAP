namespace Clinica.DataAccess.Mappers
{
    using Clinica.DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Mapper para la entidad Cita
    /// </summary>
    class CitaMapper : EntityTypeConfiguration<Cita>
    {
        #region Constants
        const string NOM_TBL_CITA = "Citas";
        #endregion

        #region Construction
        public CitaMapper()
        {
            //Especificacion sobre nombre de tabla
            this.ToTable(NOM_TBL_CITA);

            //Llave primaria
            this.HasKey(c => c.Id);

            //Definición de llaves foraneas
            this.HasRequired(c => c.Paciente).WithMany().WillCascadeOnDelete(false);
            this.HasRequired(c => c.Medico).WithMany().WillCascadeOnDelete(false);
            this.HasRequired(c => c.TipoCita).WithMany().WillCascadeOnDelete(false);
            this.HasRequired(c => c.Sitio).WithMany().WillCascadeOnDelete(false);
        }
        #endregion
    }
}

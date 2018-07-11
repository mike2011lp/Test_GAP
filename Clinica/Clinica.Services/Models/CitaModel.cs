namespace Clinica.Services.Models
{
    /// <summary>
    /// Modelo para la entidad citas
    /// </summary>
    public class CitaModel
    {
        #region Construction
        public CitaModel()
        {
            this.Paciente = new UsuarioModel();
            this.Medico = new UsuarioModel();
            this.TipoCita = new ParametroModel();
            this.Sitio = new UbicacionModel();
        }
        #endregion

        #region Properties
        public int Id { get; set; }

        public UsuarioModel Paciente { get; set; }

        public UsuarioModel Medico { get; set; }

        public ParametroModel TipoCita { get; set; }

        public UbicacionModel Sitio { get; set; }
        #endregion
    }
}

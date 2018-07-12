namespace Clinica.Services.Models
{
    using System;

    /// <summary>
    /// Modelo para la entidad citas
    /// </summary>
    public class CitaModel
    {
        #region Construction
        public CitaModel()
        {
            this.Paciente = new UsuarioModel();
            this.TipoCita = new ParametroModel();
            this.Sitio = new UbicacionModel();
        }
        #endregion

        #region Properties
        public int Id { get; set; }

        public string Paciente_Id { get; set; }

        public UsuarioModel Paciente { get; set; }

        public int TipoCita_Id { get; set; }

        public ParametroModel TipoCita { get; set; }

        public int Sitio_Id { get; set; }

        public UbicacionModel Sitio { get; set; }

        public DateTime FechaCita { get; set; }
        #endregion
    }
}

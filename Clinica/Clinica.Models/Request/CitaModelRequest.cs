namespace Clinica.Models.Request
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CitaModelRequest
    {
        [Required]
        public string Paciente { get; set; }

        [Required]
        public string Sitio { get; set; }

        [Required]
        public string CodigoCita { get; set; }

        [Required]
        public string ValorCita { get; set; }

        [Required]
        public DateTime FechaCita { get; set; }
    }
}

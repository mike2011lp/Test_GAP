using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la cita para determinado usuario
    /// </summary>
    public class Cita : BaseEntity
    {
        #region Construction
        public Cita()
        {
            this.Paciente = new Usuario();
            this.TipoCita = new Parametro();
            this.Sitio = new Ubicacion();
        }
        #endregion

        #region Properties
        public string Paciente_Id { get; set; }

        [ForeignKey("Paciente_Id")]
        public Usuario Paciente { get; set; }

        public int TipoCita_Id { get; set; }

        [ForeignKey("TipoCita_Id")]
        public Parametro TipoCita { get; set; }

        public int Sitio_Id { get; set; }

        [ForeignKey("Sitio_Id")]
        public Ubicacion Sitio { get; set; }

        public DateTime FechaCita { get; set; }
        #endregion
    }
}

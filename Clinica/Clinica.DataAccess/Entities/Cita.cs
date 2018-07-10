using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la cita para determinado usuario
    /// </summary>
    public class Cita
    {
        #region Construction
        public Cita()
        {
            this.Paciente = new Usuario();
            this.Medico = new Usuario();
            this.TipoCita = new Parametro();
            this.Sitio = new Ubicacion();
        }
        #endregion

        #region Properties
        public int Id { get; set; }

        public Usuario Paciente { get; set; }

        public Usuario Medico { get; set; }

        public Parametro TipoCita { get; set; }

        public Ubicacion Sitio { get; set; }
        #endregion
    }
}

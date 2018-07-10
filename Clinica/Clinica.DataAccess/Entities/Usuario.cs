using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.DataAccess.Entities
{
    /// <summary>
    /// Entidad Usuario
    /// </summary>
    public class Usuario
    {
        #region Construction
        public Usuario()
        {
            this.TipoIdentificacion = new Parametro();
            this.TipoUsuario = new Parametro();
            this.Roles = new List<Rol>();
            this.Estado = new Parametro();
        }
        #endregion

        #region Properties
        public int Id { get; set; }

        public Parametro TipoIdentificacion { get; set; }

        public string NumeroIdentificacion { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Contrasena { get; set; }

        public string Telefono { get; set; }

        public Parametro Estado { get; set; }

        public Parametro TipoUsuario { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public virtual ICollection<Rol> Roles { get; set; }
        #endregion
    }
}

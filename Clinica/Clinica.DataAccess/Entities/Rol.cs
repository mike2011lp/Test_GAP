using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la definición de un rol
    /// </summary>
    public class Rol
    {
        #region Construction
        public Rol()
        {
            this.Usuarios = new List<Usuario>();
        }
        #endregion

        #region Properties
        public string Codigo { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.DataAccess.Entities
{
    /// <summary>
    /// Ubicación donde el usuario será atendido
    /// </summary>
    public class Ubicacion
    {
        #region Construction
        public Ubicacion() { }
        #endregion

        #region Properties
        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public string Direccion { get; set; }
        #endregion
    }
}

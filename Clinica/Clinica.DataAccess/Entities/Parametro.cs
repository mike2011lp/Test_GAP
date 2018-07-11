using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa un parámetro de la aplicación
    /// </summary>
    public class Parametro : BaseEntity
    {
        #region Construction
        public Parametro() { }
        #endregion

        #region Properties
        public string Categoria { get; set; }

        public string Codigo { get; set; }

        public string ValorPrincipal { get; set; }

        public string ValorSecundario { get; set; }
        #endregion
    }
}

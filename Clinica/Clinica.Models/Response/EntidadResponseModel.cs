using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Response
{
    /// <summary>
    /// Modelo de respuesta para una entidad
    /// </summary>
    public class EntidadResponseModel
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public string Direccion { get; set; }
    }
}

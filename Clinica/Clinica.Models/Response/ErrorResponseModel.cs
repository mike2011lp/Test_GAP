using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Response
{
    /// <summary>
    /// Modelo para recibir respuestas de error desde el servicio
    /// </summary>
    public class ErrorResponseModel
    {
        public string error { get; set; }

        public string error_description { get; set; }
    }
}

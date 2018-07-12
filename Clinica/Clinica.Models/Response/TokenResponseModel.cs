using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Response
{
    /// <summary>
    /// Modelo con el token de respuesta para el proceso de autenticación
    /// </summary>
    public class TokenResponseModel
    {
        public string expires_in { get; set; }

        public string access_token { get; set; }
    }
}

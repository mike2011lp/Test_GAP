using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Services.Enums
{
    /// <summary>
    /// Enumeración para dar manejo a los distintos casos de error en base de datos
    /// </summary>
    public enum MensajesErrorEnum
    {
        EntidadNula = 1,
        Validacion = 2,
        UsuarioNoExiste = 3,
        CredencialesInvalidas = 4,
        NoAutorizaCreacion = 5,
        NoAutorizaBorrado = 6,
        NoAutorizaActualizacion = 7,
        InfoInvalida = 8,
        NoAutorizaToken = 9
    }
}

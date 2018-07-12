using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Constantes
{
    /// <summary>
    /// Constantes sobre parámetros del sistema
    /// </summary>
    public static class SystemConstants
    {
        #region Roles
        public const string ROL_ADMIN = "Administrador";
        public const string ROL_PACIENTE = "Paciente";
        public const string ROL_MEDICO = "Medico";
        #endregion

        #region Limites
        public const int LIMIT_EXCEPTION_MSG_DISPLAY_LENGTH = 512;
        #endregion
    }
}

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

        #region Códigos Parametros
        public const string PARAM_CAT_TIPO_ID = "TIPO_ID";
        public const string PARAM_CAT_TIPO_USER = "TIPO_USUARIO";
        public const string PARAM_CAT_ESTADO_USUARIO = "ESTADO_USUARIO";
        public const string PARAM_CAT_ESTADO_CITA = "ESTADO_CITA";
        public const string PARAM_CAT_ESPEC = "ESPECIALIDAD";
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Constantes
{
    /// <summary>
    /// Constantes para proyecto web
    /// </summary>
    public static class WebConstants
    {
        #region Config Keys
        public const string CFG_KEY_API_BASE_URI = "ApiBaseUri";
        #endregion

        #region TempData Keys
        public const string TMP_MESSAGE = "T_MSG";
        #endregion

        #region Url structure
        public const string URL_TOKEN_STRUCT = "{0}/token";
        #endregion

        #region Headers Definitions
        public const string HEADER_AUTH_GRANT = "grant_type";
        public const string HEADER_AUTH_USER = "username";
        public const string HEADER_AUTH_PASS = "password";
        #endregion

        #region Header values
        public const string HEADER_AUTH_VAL_GRANT = "password";
        #endregion
    }
}

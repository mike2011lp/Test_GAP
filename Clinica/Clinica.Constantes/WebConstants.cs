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
        public const string URL_API_GET = "{0}/api/{1}";
        public const string URL_API_GET_BY_IDENTIFIER = "{0}/api/{1}/{2}";
        #endregion

        #region Headers Definitions
        public const string HEADER_AUTH_GRANT = "grant_type";
        public const string HEADER_AUTH_USER = "username";
        public const string HEADER_AUTH_PASS = "password";
        public const string HEADER_AUTH_AUTHORIZATION = "Authorization";
        #endregion

        #region Header values
        public const string HEADER_AUTH_VAL_GRANT = "password";
        #endregion

        #region Claims
        public const string CLAIM_TYPE_ACCESS_TOKEN = "AcessToken";
        #endregion
    }
}

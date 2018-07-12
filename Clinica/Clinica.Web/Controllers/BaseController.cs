namespace Clinica.Web.Controllers
{
    using Clinica.Comun.Exceptions;
    using Clinica.Constantes;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using Clinica.Extensiones.Main;
    using System.Net.Http;
    using Newtonsoft.Json;
    using Clinica.Models.Response;
    using System.Web;

    /// <summary>
    /// Base controller for application
    /// </summary>
    public class BaseController : Controller
    {
        #region Properties
        public string ApiBaseUri
        {
            get
            {
                return ConfigurationManager.AppSettings[WebConstants.CFG_KEY_API_BASE_URI];
            }
        }

        /// <summary>
        /// Identify if a user is or not authenticated
        /// </summary>
        protected bool IsUserAuthenticated
        {
            get
            {
                var context = HttpContext.GetOwinContext();

                return context != null &&
                    context.Request.User != null &&
                    context.Request.User.Identity != null &&
                    context.Request.User.Identity.IsAuthenticated;
            }
        }
        #endregion

        #region Exception Handling
        /// <summary>
        /// Proceso para manejar excepciones en solicitudes AJAX
        /// </summary>
        /// <param name="ex"></param>
        protected ActionResult HandleException(Exception ex)
        {
            //Setear el código del estado
            Response.StatusCode = ex is BusinessException  ?
                (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.InternalServerError;

            //Setear la descripción del estado
            Response.StatusDescription = this.GetExceptionMessage(ex).LimitLength(SystemConstants.LIMIT_EXCEPTION_MSG_DISPLAY_LENGTH);

            //Return response if neccesary
            return new HttpStatusCodeResult(Response.StatusCode, Response.StatusDescription);
        }

        /// <summary>
        /// Obtener mensaje de excepción con el formato correcto
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected string GetExceptionMessage(Exception ex)
        {
            string message = string.Empty;

            if (ex is BusinessException)
            {
                message = ex.Message;
            }
            else if (ex is AggregateException)
            {
                List<string> lstErrorMessages = new List<string>();

                // This is where you can choose which exceptions to handle.
                foreach (var iex in ((AggregateException)ex).InnerExceptions)
                {
                    lstErrorMessages.Add(iex.Message);
                }

                message = string.Join(CommonConstants.STR_DOUBLE_PIPE, lstErrorMessages);
            }
            else if (ex is Exception)
            {
                message = ex.Message;
            }

            return Regex.Replace(message, "<.*?>", string.Empty).Replace("\r\n", "\n").Replace("\n", CommonConstants.STR_WHITESPACE); ;
        }
        #endregion

        #region Api response analizing
        /// <summary>
        /// Verificar la respuesta del API
        /// </summary>
        /// <param name="response"></param>
        /// <param name="content"></param>
        protected void VerifyApiResponse(HttpResponseMessage response, string content)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseModel>(content);

                throw new BusinessException(errorResponse.error_description);
            }
        }
        #endregion
    }
}
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
    using System.Security.Claims;
    using System.Linq;
    using Clinica.Web.Models.Citas;
    using Clinica.Models.Request;
    using System.Text;
    using System.Threading.Tasks;

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
        /// Bandera para identificar si un usuario está o no autenticado
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

        /// <summary>
        /// Obtener token de autenticación
        /// </summary>
        protected string BearerToken
        {
            get
            {
                var context = HttpContext.GetOwinContext();

                if (this.IsUserAuthenticated)
                {
                    var identity = (ClaimsIdentity)context.Request.User.Identity;
                    var claim = identity.Claims.FirstOrDefault(c => c.Type.Equals(WebConstants.CLAIM_TYPE_ACCESS_TOKEN));
                    return claim != null ? claim.Value : null;
                }
                return null;
            }
        }

        /// <summary>
        /// Obtener nombre de usuario autenticado
        /// </summary>
        protected string UserName
        {
            get
            {
                var context = HttpContext.GetOwinContext();

                if (this.IsUserAuthenticated)
                {
                    var identity = (ClaimsIdentity)context.Request.User.Identity;
                    var claim = identity.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier));
                    return claim != null ? claim.Value : null;
                }
                return null;
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

        #region Api consuming
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
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                var errorResponse = JsonConvert.DeserializeObject<ExceptionResponseModel>(content);

                throw new BusinessException(errorResponse.ExceptionMessage);
            }
        }

        /// <summary>
        /// Construir Url para petición get por identificador
        /// </summary>
        /// <param name="recurso"></param>
        /// <param name="identifier"></param>
        protected string ConstruirGetUrl(string recurso,  object identifier = null)
        {
            if (identifier == null)
            {
                return string.Format(WebConstants.URL_API_GET, this.ApiBaseUri, recurso);
            }
            else
            {
                return string.Format(WebConstants.URL_API_GET_BY_IDENTIFIER, this.ApiBaseUri, recurso, identifier);
            }
        }

        /// <summary>
        /// Construir Url para petición post por operacion
        /// </summary>
        /// <param name="recurso"></param>
        /// <param name="operacion"></param>
        protected string ConstruirPostUrl(string recurso, string operacion)
        {
            return string.Format(WebConstants.URL_API_POST_OPERATION, this.ApiBaseUri, recurso, operacion);
        }
        #endregion

        #region Default API Requests
        protected List<EntidadResponseModel> ObtenerEntidades()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                //Setear el header de autorizacion
                httpClient.DefaultRequestHeaders.Add(WebConstants.HEADER_AUTH_AUTHORIZATION, this.BearerToken);

                //Obtener URL que consume el API
                var entidadesUrl = this.ConstruirGetUrl(WebConstants.API_RESOURCE_ENTIDADES);

                //Efectuar el request al API
                HttpResponseMessage result = httpClient.GetAsync(entidadesUrl).Result;

                //Leer el contenido de la respuesta
                string resultContent = result.Content.ReadAsStringAsync().Result;

                //Verificar respuesta del API
                this.VerifyApiResponse(result, resultContent);

                //Parsear elementos
                return JsonConvert.DeserializeObject<List<EntidadResponseModel>>(resultContent);
            }
        }

        protected List<ParametroResponseModel> ObtenerParametroPorCategoria(string categoria)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                //Setear el header de autorizacion
                httpClient.DefaultRequestHeaders.Add(WebConstants.HEADER_AUTH_AUTHORIZATION, this.BearerToken);

                //Obtener URL que consume el API
                var parametrosUrl = this.ConstruirGetUrl(WebConstants.API_RESOURCE_PARAMETROS, categoria);

                //Efectuar el request al API
                HttpResponseMessage result = httpClient.GetAsync(parametrosUrl).Result;

                //Leer el contenido de la respuesta
                string resultContent = result.Content.ReadAsStringAsync().Result;

                //Verificar respuesta del API
                this.VerifyApiResponse(result, resultContent);

                //Parsear elementos
                return JsonConvert.DeserializeObject<List<ParametroResponseModel>>(resultContent);
            }
        }

        protected UsuarioResponseModel ObtenerUsuarioPorUserName(string userName)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                //Setear el header de autorizacion
                httpClient.DefaultRequestHeaders.Add(WebConstants.HEADER_AUTH_AUTHORIZATION, this.BearerToken);

                //Obtener URL que consume el API
                var usuariosUrl = this.ConstruirGetUrl(WebConstants.API_RESOURCE_USUARIOS, this.UserName);

                //Efectuar el request al API
                HttpResponseMessage result = httpClient.GetAsync(usuariosUrl).Result;

                //Leer el contenido de la respuesta
                string resultContent = result.Content.ReadAsStringAsync().Result;

                //Verificar respuesta del API
                this.VerifyApiResponse(result, resultContent);

                //Parsear elementos
                return JsonConvert.DeserializeObject<UsuarioResponseModel>(resultContent);
            }
        }

        protected async Task<bool> SolicitarCita(SolicitarCitaModel model)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                //Setear el header de autorizacion
                httpClient.DefaultRequestHeaders.Add(WebConstants.HEADER_AUTH_AUTHORIZATION, this.BearerToken);

                //Obtener URL que consume el API
                var crearCitaUrl = this.ConstruirPostUrl(WebConstants.API_RESOURCE_CITAS, WebConstants.API_OPERATION_CREATE);

                //Generar date de cita a partir del modelo
                var hora = DateTime.Now.Date.AddHours(7).AddMinutes(30 * Convert.ToInt32(model.HoraCita)).ToString(CommonConstants.TIME_FORMAT_24_HOURS);

                //Construir fecha cita
                var fechaCita = StringExtensions.ParseDateAndTime(model.FechaCita, hora, CommonConstants.DATE_FORMAT_YEAR_FULL, CommonConstants.TIME_FORMAT_24_HOURS);

                //Obtener parametro de especialidad
                var tipoCitaParam = this.ObtenerParametroPorCategoria(SystemConstants.PARAM_CAT_ESPEC).FirstOrDefault(c => c.ValorPrincipal.Equals(model.Servicio));

                //Crear objeto request
                var citaRequest = new CitaModelRequest
                {
                    Paciente = this.UserName,
                    CodigoCita = tipoCitaParam.Codigo,
                    ValorCita = tipoCitaParam.ValorPrincipal,
                    FechaCita = fechaCita,
                    Sitio = model.Entidad,
                };

                //Parsear el contenido
                StringContent content = new StringContent(JsonConvert.SerializeObject(citaRequest), Encoding.UTF8, "application/json");

                //Efectuar el request al API
                HttpResponseMessage result = await httpClient.PostAsync(crearCitaUrl, content);

                //Leer el contenido de la respuesta
                string resultContent = result.Content.ReadAsStringAsync().Result;

                //Verificar respuesta del API
                this.VerifyApiResponse(result, resultContent);

                //Parsear elementos
                return true;
            }
        }

        #endregion
    }
}
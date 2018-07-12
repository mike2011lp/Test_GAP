namespace Clinica.Web.Controllers
{
    using Clinica.Constantes;
    using Clinica.Models.Response;
    using Clinica.Web.Models.Citas;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Mvc;
    using System.Linq;

    /// <summary>
    /// Controlador para acciones relacionadas con citas
    /// </summary>
    public class CitasController : BaseController
    {
        #region Action Methods
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var model = new SolicitarCitaModel();

            using (HttpClient httpClient = new HttpClient())
            {
                //Setear el header de autorizacion
                httpClient.DefaultRequestHeaders.Add(WebConstants.HEADER_AUTH_AUTHORIZATION, this.BearerToken);

                //Obtener servicios por código desde el API
                var parametrosUrl = string.Format(WebConstants.URL_API_GET_BY_IDENTIFIER, this.ApiBaseUri, "parametros", SystemConstants.PARAM_CAT_ESPEC);

                //Efectuar el request al API
                HttpResponseMessage result = httpClient.GetAsync(parametrosUrl).Result;

                //Leer el contenido de la respuesta
                string resultContent = result.Content.ReadAsStringAsync().Result;

                //Verificar respuesta del API
                this.VerifyApiResponse(result, resultContent);

                //Parsear elementos
                var parametros = JsonConvert.DeserializeObject<List<ParametroResponseModel>>(resultContent);

                //Setear elementos en el modelo
                model.Servicios = parametros.Select(p => new KeyValuePair<string, string>(p.ValorPrincipal, p.ValorSecundario)).ToList();
            }

            return View(model);
        }
        #endregion
    }
}
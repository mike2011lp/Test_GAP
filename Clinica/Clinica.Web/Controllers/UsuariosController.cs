namespace Clinica.Web.Controllers
{
    using Microsoft.Owin.Security;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Web.Mvc;
    using System.Web;
    using Clinica.Web.Models.Usuario;
    using Clinica.Constantes;
    using Newtonsoft.Json;
    using Clinica.Models.Response;

    /// <summary>
    /// Controlador para operaciones con usuarios
    /// </summary>
    public class UsuariosController : BaseController
    {
        /// <summary>
        /// Acción Principal de la vista
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            LoginModel loginModel = new LoginModel();

            #region User Login Verification
            //If user is OWIN authenticated and authenticated on auth service redirect to home page
            if (this.IsUserAuthenticated)
            {
                return RedirectToAction("Index", "Citas");
            }
            #endregion

            return View(loginModel);
        }

        #region Action Methods
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                return this.Autenticar(model);
            }
            catch(Exception ex)
            {
                return this.HandleException(ex);
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Autenticar el usuario actual en la aplicación
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected JsonResult Autenticar(LoginModel model)
        {
            //Url para obtener el token que servirá para la autenticacion
            var tokenUrl = string.Format(WebConstants.URL_TOKEN_STRUCT, this.ApiBaseUri);

            using (HttpClient httpClient = new HttpClient())
            {
                //Construir el contenido que se enviará en el request
                HttpContent content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>(WebConstants.HEADER_AUTH_GRANT, WebConstants.HEADER_AUTH_VAL_GRANT),
                    new KeyValuePair<string, string>(WebConstants.HEADER_AUTH_USER , model.NombreUsuario),
                    new KeyValuePair<string, string>(WebConstants.HEADER_AUTH_PASS , model.Contrasena)
                });

                //Efectuar el request al API
                HttpResponseMessage result = httpClient.PostAsync(tokenUrl, content).Result;

                //Leer el contenido de la respuesta
                string resultContent = result.Content.ReadAsStringAsync().Result;

                //Verificar respuesta del API
                this.VerifyApiResponse(result, resultContent);

                //Obtener el token deserializado
                var token = JsonConvert.DeserializeObject<TokenResponseModel>(resultContent);

                AuthenticationProperties options = new AuthenticationProperties();

                options.AllowRefresh = true;
                options.IsPersistent = true;
                options.ExpiresUtc = DateTime.UtcNow.AddSeconds(int.Parse(token.expires_in));

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, model.NombreUsuario),
                    new Claim("AccessToken", string.Format("Bearer {0}", token.access_token)),
                };

                var identity = new ClaimsIdentity(claims, "ApplicationCookie");

                Request.GetOwinContext().Authentication.SignIn(options, identity);
            }

            //When succeding send link for redirecting to home view
            return Json(Url.Action("Index", "Citas"));
        }
        #endregion
    }
}
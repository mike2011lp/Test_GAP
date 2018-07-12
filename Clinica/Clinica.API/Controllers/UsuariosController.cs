namespace Clinica.API.Controllers
{
    using Clinica.Services.Handlers;
    using Clinica.Services.Models;
    using Clinica.Services.Services.Services.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Linq;
    using System.Net.Http;
    using Clinica.Services.Enums;
    using Microsoft.AspNet.Identity;

    [RoutePrefix("api/usuarios")]
    public class UsuariosController : ApiController
    {
        #region Fields
        private readonly IUsuariosService myUsuariosService;

        private readonly IErrorHandler myErrorHandler;
        #endregion

        #region Construction
        public UsuariosController(IUsuariosService usuariosService, IErrorHandler errorHandler)
        {
            this.myUsuariosService = usuariosService;
            this.myErrorHandler = errorHandler;
        }
        #endregion

        #region Api Methods
        [Route("")]
        [Authorize]
        [HttpGet]
        public List<UsuarioModel> Get()
        {
            return this.myUsuariosService.GetAll().ToList();
        }

        [Route("{userName}")]
        [Authorize]
        [HttpGet]
        public UsuarioModel GetByUserName(string userName)
        {
            return this.myUsuariosService.GetByUserName(userName);
        }

        [Route("create")]
        [Authorize]
        [HttpPost]
        public async Task<UsuarioModel> Create([FromBody]UsuarioModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpRequestException(string.Format(
                    this.myErrorHandler.GetMessage(MensajesErrorEnum.Validacion),
                    ModelState.Values.First().Errors.First().ErrorMessage));
            }
            var user = new UsuarioModel
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await this.myUsuariosService.Create(user, model.PasswordHash);

            if (result.Succeeded)
            {
                return this.myUsuariosService.GetByUserName(model.UserName);
            }
            throw new HttpRequestException(this.myErrorHandler.GetMessage(MensajesErrorEnum.NoAutorizaCreacion));
        }

        [Route("delete")]
        [Authorize]
        [HttpPost]
        public async Task<UsuarioModel> Delete([FromBody]UsuarioModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpRequestException(string.Format(
                    this.myErrorHandler.GetMessage(MensajesErrorEnum.Validacion),
                    ModelState.Values.First().Errors.First().ErrorMessage));
            }
            var user = this.myUsuariosService.GetByUserName(model.UserName);
            if (user == null)
                throw new HttpRequestException(this.myErrorHandler.GetMessage(MensajesErrorEnum.UsuarioNoExiste));


            var result = await this.myUsuariosService.Delete(user);
            if (result.Succeeded)
            {
                return user;
            }

            throw new HttpRequestException(this.myErrorHandler.GetMessage(MensajesErrorEnum.NoAutorizaBorrado));
        }

        [Authorize]
        [HttpPost]
        public async Task<UsuarioModel> Edit([FromBody] UsuarioModel request)
        {
            var user = this.myUsuariosService.GetByUserName(request.UserName);

            if (user == null) throw new HttpRequestException(this.myErrorHandler.GetMessage(MensajesErrorEnum.UsuarioNoExiste));

            var valid = await this.myUsuariosService.Validate(user);

            if (!valid.Succeeded)
            {
                this.myErrorHandler.ErrorIdentityResult(valid);
            }

            if (!string.IsNullOrEmpty(request.PasswordHash))
            {
                user.PasswordHash = this.myUsuariosService.HashPassword(request.PasswordHash);
            }

            if (valid != null && ((!valid.Succeeded || request.PasswordHash == string.Empty || !valid.Succeeded)))
            {
                throw new HttpRequestException(this.myErrorHandler.GetMessage(MensajesErrorEnum.InfoInvalida));
            }

            var result = await this.myUsuariosService.Update(user);

            if (result.Succeeded)
            {
                return user;
            }

            throw new HttpRequestException(this.myErrorHandler.GetMessage(MensajesErrorEnum.NoAutorizaActualizacion));
        }
        #endregion
    }
}
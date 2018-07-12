namespace Clinica.API.Controllers
{
    using Clinica.Services.Enums;
    using Clinica.Services.Handlers;
    using Clinica.Services.Models;
    using Clinica.Services.Services.Services.Interfaces;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Linq;

    /// <summary>
    /// Controlador de acceso para parametros
    /// </summary>
    [RoutePrefix("api/parametros")]
    public class ParametrosController : ApiController
    {
        #region Fields
        private readonly IParametroService myService;

        private readonly IErrorHandler myErrorHandler;
        #endregion

        #region Construction
        public ParametrosController(IParametroService service, IErrorHandler errorHandler)
        {
            this.myService = service;
            this.myErrorHandler = errorHandler;
        }
        #endregion

        #region Api Methods
        [Authorize]
        [Route("")]
        public async Task<IHttpActionResult> GetAsync()
        {
            return Ok(await this.myService.Get());
        }

        [Authorize]
        [Route("{categoria}")]
        public IHttpActionResult GetByCategoria(string categoria)
        {
            return Ok(this.myService.Where(p => p.Categoria.Equals(categoria)));
        }

        [Route("create")]
        [Authorize]
        [HttpPost]
        public ParametroModel Create([FromBody]ParametroModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpRequestException(string.Format(
                    this.myErrorHandler.GetMessage(MensajesErrorEnum.Validacion),
                    ModelState.Values.First().Errors.First().ErrorMessage));
            }
            var parametro = new ParametroModel
            {
                Categoria = model.Categoria,
                Codigo = model.Codigo,
                ValorPrincipal = model.ValorPrincipal,
                ValorSecundario = model.ValorSecundario
            };

            var result = this.myService.Add(parametro);

            if (result.CompareTo(0) > 0)
            {
                return this.myService.Where(p => p.Codigo.Equals(model.Codigo)).FirstOrDefault();
            }
            else
            {
                throw new HttpRequestException(this.myErrorHandler.GetMessage(MensajesErrorEnum.NoAutorizaCreacion));
            }
        }

        [Route("delete")]
        [Authorize]
        [HttpPost]
        public int Delete([FromBody]ParametroModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpRequestException(string.Format(
                    this.myErrorHandler.GetMessage(MensajesErrorEnum.Validacion),
                    ModelState.Values.First().Errors.First().ErrorMessage));
            }

            var parametro = this.myService.GetById(model.Id);

            if (parametro == null)
            {
                throw new HttpRequestException(this.myErrorHandler.GetMessage(MensajesErrorEnum.ParametroNoExiste));
            }

            var result =  this.myService.Remove(model.Id);

            if (result.CompareTo(0) > 0)
            {
                return result;
            }
            else
            {
                throw new HttpRequestException(this.myErrorHandler.GetMessage(MensajesErrorEnum.NoAutorizaBorrado));
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ParametroModel> Edit([FromBody] ParametroModel request)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpRequestException(string.Format(
                    this.myErrorHandler.GetMessage(MensajesErrorEnum.Validacion),
                    ModelState.Values.First().Errors.First().ErrorMessage));
            }

            var parametro = await this.myService.GetById(request.Id);

            if (parametro == null)
            {
                throw new HttpRequestException(this.myErrorHandler.GetMessage(MensajesErrorEnum.ParametroNoExiste));
            }

            var result = await this.myService.Update(request);

            if (result.CompareTo(0) > 0)
            {
                return await this.myService.GetById(request.Id);
            }
            else
            {
                throw new HttpRequestException(this.myErrorHandler.GetMessage(MensajesErrorEnum.NoAutorizaActualizacion));
            }
        }
        #endregion
    }
}

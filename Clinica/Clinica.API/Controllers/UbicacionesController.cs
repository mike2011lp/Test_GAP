namespace Clinica.API.Controllers
{
    using Clinica.Services.Enums;
    using Clinica.Services.Handlers;
    using Clinica.Services.Models;
    using Clinica.Services.Services.Services.Interfaces;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix("api/ubicaciones")]
    public class UbicacionesController : ApiController
    {
        #region Fields
        private readonly IUbicacionService myService;

        private readonly IErrorHandler myErrorHandler;
        #endregion

        #region Construction
        public UbicacionesController(IUbicacionService service, IErrorHandler errorHandler)
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
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            return Ok(this.myService.Where(p => p.Id.CompareTo(id) == 0));
        }

        [Route("create")]
        [Authorize]
        [HttpPost]
        public UbicacionModel Create([FromBody]UbicacionModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpRequestException(string.Format(
                    this.myErrorHandler.GetMessage(MensajesErrorEnum.Validacion),
                    ModelState.Values.First().Errors.First().ErrorMessage));
            }
            var ubicacion = new UbicacionModel
            {
                Descripcion = model.Descripcion,
                Direccion = model.Direccion
            };

            var result = this.myService.Add(ubicacion);

            if (result.CompareTo(0) > 0)
            {
                return this.myService.Where(p => p.Descripcion.Equals(model.Descripcion)).FirstOrDefault();
            }
            else
            {
                throw new HttpRequestException(this.myErrorHandler.GetMessage(MensajesErrorEnum.NoAutorizaCreacion));
            }
        }

        [Route("delete")]
        [Authorize]
        [HttpPost]
        public int Delete([FromBody]UbicacionModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpRequestException(string.Format(
                    this.myErrorHandler.GetMessage(MensajesErrorEnum.Validacion),
                    ModelState.Values.First().Errors.First().ErrorMessage));
            }

            var ubicacion = this.myService.GetById(model.Id);

            if (ubicacion == null)
            {
                throw new HttpRequestException(this.myErrorHandler.GetMessage(MensajesErrorEnum.ParametroNoExiste));
            }

            var result = this.myService.Remove(model.Id);

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
        public async Task<UbicacionModel> Edit([FromBody] UbicacionModel request)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpRequestException(string.Format(
                    this.myErrorHandler.GetMessage(MensajesErrorEnum.Validacion),
                    ModelState.Values.First().Errors.First().ErrorMessage));
            }

            var ubicacion = await this.myService.GetById(request.Id);

            if (ubicacion == null)
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

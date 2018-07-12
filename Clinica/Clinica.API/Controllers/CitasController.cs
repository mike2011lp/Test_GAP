namespace Clinica.API.Controllers
{
    using Clinica.Constantes;
    using Clinica.Models.Request;
    using Clinica.Services.Enums;
    using Clinica.Services.Handlers;
    using Clinica.Services.Models;
    using Clinica.Services.Services.Services.Interfaces;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix("api/citas")]
    public class CitasController : ApiController
    {
        #region Fields
        private readonly ICitaService myService;

        private readonly IParametroService myParametroService;

        private readonly IUsuariosService myUsuariosService;

        private readonly IUbicacionService myUbicacionService;

        private readonly IErrorHandler myErrorHandler;
        #endregion

        #region Construction
        public CitasController(ICitaService service, IParametroService parametroService, IUsuariosService usuariosService, IUbicacionService ubicacionService, IErrorHandler errorHandler)
        {
            this.myService = service;
            this.myParametroService = parametroService;
            this.myUsuariosService = usuariosService;
            this.myUbicacionService = ubicacionService;
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

        [Route("create")]
        [Authorize]
        [HttpPost]
        public async Task<CitaModel> Create([FromBody]CitaModelRequest model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpRequestException(string.Format(
                    this.myErrorHandler.GetMessage(MensajesErrorEnum.Validacion),
                    ModelState.Values.First().Errors.First().ErrorMessage));
            }

            var paciente = this.myUsuariosService.GetByUserName(model.Paciente);
            var sitio = await this.myUbicacionService.GetById(Convert.ToInt32(model.Sitio));
            var tipoCita = this.myParametroService.Where(p => p.Codigo.Equals(model.CodigoCita) &&
                                p.Categoria.Equals(SystemConstants.PARAM_CAT_ESPEC)).FirstOrDefault();

            //Verificar que no exista cita en el pasado
            if (model.FechaCita.CompareTo(DateTime.Now) <= 0)
            {
                throw new HttpRequestException(this.myErrorHandler.GetMessage(MensajesErrorEnum.CitaFechaPasado));
            }

            //Verificar que no existan citas para el mismo dia para el usuario actual
            var fechaLimite = model.FechaCita.Date.AddDays(1);

            var citaHoy = this.myService.Where(p => p.Paciente.UserName.Equals(model.Paciente) && p.FechaCita.CompareTo(model.FechaCita.Date) >= 0 &&
                p.FechaCita.CompareTo(fechaLimite) <= 0).FirstOrDefault();

            if (citaHoy != null)
            {
                throw new HttpRequestException(this.myErrorHandler.GetMessage(MensajesErrorEnum.CitaMismoDia));
            }
            else
            {
                var cita = new CitaModel
                {
                    Paciente_Id = paciente.Id,
                    Paciente = null,
                    Sitio_Id = sitio.Id,
                    Sitio = null,
                    TipoCita_Id = tipoCita.Id,
                    TipoCita = null,
                    FechaCita = model.FechaCita
                };

                var result = this.myService.Add(cita);

                if (result.CompareTo(0) > 0)
                {
                    return this.myService.Where(p => p.Paciente.UserName.Equals(model.Paciente) && p.FechaCita.CompareTo(model.FechaCita) == 0).FirstOrDefault();
                }
                else
                {
                    throw new HttpRequestException(this.myErrorHandler.GetMessage(MensajesErrorEnum.NoAutorizaCreacion));
                }
            }
        }
        #endregion
    }
}

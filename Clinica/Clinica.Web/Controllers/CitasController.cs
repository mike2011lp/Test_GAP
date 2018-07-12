namespace Clinica.Web.Controllers
{
    using Clinica.Constantes;
    using Clinica.Web.Models.Citas;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Linq;
    using System;
    using System.Threading.Tasks;
    using System.Net;
    using Clinica.Recursos.ResourceFiles;

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

            //Obtener usuario
            var usuario = this.ObtenerUsuarioPorUserName(this.UserName);

            //Setear datos de usuario en modelo
            model.AsignarUsuarioResponseModel(usuario);

            //Obtener parámetros => Tipos de especialidad
            var especialidades = this.ObtenerParametroPorCategoria(SystemConstants.PARAM_CAT_ESPEC);

            //Setear elementos en el modelo
            model.Servicios = especialidades.Select(p => new KeyValuePair<string, string>(p.ValorPrincipal, p.ValorSecundario)).ToList();

            //Obtener parámetros => Tipos de identificacion
            var tiposIdent = this.ObtenerParametroPorCategoria(SystemConstants.PARAM_CAT_TIPO_ID);

            //Setear elementos en el modelo
            model.TiposDocumento = tiposIdent.Select(p => new KeyValuePair<string, string>(p.ValorPrincipal, p.ValorSecundario)).ToList();

            //Obtener ubicaciones (entidades)
            var entidades = this.ObtenerEntidades();

            //Setear elementos en el modelo
            model.Entidades = entidades.Select(p => new KeyValuePair<string, string>(p.Id.ToString(), p.Descripcion)).ToList();

            //Definir listado de horas
            model.Horas = this.ObtenerListaHoras(30);

            //Retornar vista con modelo
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Solicitar(SolicitarCitaModel model)
        {
            try
            {
                await this.SolicitarCita(model);

                return new HttpStatusCodeResult((int)HttpStatusCode.OK, Messages.MSG_CITA_CREACION_EXITO);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex);
            }

        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Generar lista de horas
        /// </summary>
        /// <param name="minInterval">Minute interval</param>
        /// <returns></returns>
        private List<KeyValuePair<int, string>> ObtenerListaHoras(int minInterval)
        {
            //Contador de iteraciones
            var itCounter = 0;
            var minutesToAdd = 0;

            //Generar lista de tiempo
            var lstTime = new List<KeyValuePair<int, string>>();

            //Obtener fecha inicial y final para efectuar intervalos
            var baseDate = DateTime.Now.Date.AddHours(7);
            var incDate = DateTime.Now.Date.AddHours(7);
            var limit = DateTime.Now.Date.AddHours(19);

            while (incDate.CompareTo(limit) < 0)
            {
                //Get minutes to add
                minutesToAdd = minInterval * itCounter;

                //Add minutes to incDate
                incDate = baseDate.AddMinutes(minutesToAdd);

                //Add incDate in proper format to list
                lstTime.Add(new KeyValuePair<int, string>(itCounter, incDate.ToString(CommonConstants.TIME_FORMAT_24_HOURS)));

                //Increment one iteration
                itCounter++;
            }

            return lstTime;
        }
        #endregion
    }
}
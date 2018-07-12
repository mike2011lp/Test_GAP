namespace Clinica.Web.Models.Citas
{
    using Clinica.Models.Response;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Modelo para solicitar citas
    /// </summary>
    public class SolicitarCitaModel
    {
        public string TipoDocumento { get; set; }

        public List<KeyValuePair<string, string>> TiposDocumento { get; set; }

        public string NumeroDocumento { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Rol { get; set; }

        public string Servicio { get; set; }

        public List<KeyValuePair<string, string>> Servicios { get; set; }

        public string Entidad { get; set; }

        public List<KeyValuePair<string, string>> Entidades { get; set; }

        public string FechaCita { get; set; }

        public string HoraCita { get; set; }

        public List<KeyValuePair<int, string>> Horas { get; set; }

        public void AsignarUsuarioResponseModel(UsuarioResponseModel model)
        {
            this.TipoDocumento = model.TipoIdentificacion.ValorPrincipal;
            this.NumeroDocumento = model.NumeroIdentificacion;
            this.Nombres = model.Nombres;
            this.Apellidos = model.Apellidos;
        }
    }
}
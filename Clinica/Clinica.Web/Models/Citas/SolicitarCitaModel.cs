using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinica.Web.Models.Citas
{
    public class SolicitarCitaModel
    {
        public string TipoDocumento { get; set; }

        public string NumeroDocumento { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Servicio { get; set; }

        public List<KeyValuePair<string, string>> Servicios { get; set; }

        public string Entidad { get; set; }

        public List<KeyValuePair<string, string>> Entidades { get; set; }

        public DateTime HoraCita { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Response
{
    public class ParametroResponseModel
    {
        public string Categoria { get; set; }

        public string Codigo { get; set; }

        public string ValorPrincipal { get; set; }

        public string ValorSecundario { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Clinica.Web.Models.Usuario
{
    /// <summary>
    /// Modelo para el proceso de login
    /// </summary>
    public class LoginModel
    {
        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        public string Contrasena { get; set; }
    }
}
﻿namespace Clinica.DataAccess.Entities
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Entidad Usuario
    /// </summary>
    public class Usuario : IdentityUser
    {
        #region Construction
        public Usuario()
        {
            this.TipoIdentificacion = new Parametro();
            this.TipoUsuario = new Parametro();
            this.Estado = new Parametro();
        }
        #endregion

        #region Properties
        public Parametro TipoIdentificacion { get; set; }

        public string NumeroIdentificacion { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Telefono { get; set; }

        public Parametro Estado { get; set; }

        public Parametro TipoUsuario { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public DateTime? FechaCreacion { get; set; }
        #endregion
    }
}

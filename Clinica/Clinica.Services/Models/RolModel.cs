namespace Clinica.Services.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Modelo que aplica para la entidad Rol
    /// </summary>
    public class RolModel
    {
        #region Construction
        public RolModel()
        {
            this.Usuarios = new List<UsuarioModel>();
        }
        #endregion

        #region Properties
        public string Codigo { get; set; }

        public string Description { get; set; }

        public virtual ICollection<UsuarioModel> Usuarios { get; set; }
        #endregion
    }
}

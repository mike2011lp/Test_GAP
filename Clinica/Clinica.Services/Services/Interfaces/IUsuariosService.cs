namespace Clinica.Services.Services.Services.Interfaces
{
    using Clinica.DataAccess.Entities;
    using Clinica.Services.Models;
    using Microsoft.AspNet.Identity;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Interfaz servicio: Usuarios
    /// </summary>
    public interface IUsuariosService
    {
        IQueryable<UsuarioModel> GetAll();

        UsuarioModel GetById(string id);

        UsuarioModel GetByEmail(string email);

        UsuarioModel GetByUserId(string idUsuario);

        UsuarioModel GetByUserName(string userName);

        Task<UsuarioModel> FindUser(string userName, string contrasena);

        IQueryable<UsuarioModel> GetByType(string codigo);

        IQueryable<UsuarioModel> GetByStatus(string codigo);

        Task<IdentityResult> Create(UsuarioModel usuario, string contrasena);

        Task<IdentityResult> Delete(UsuarioModel usuario);

        Task<IdentityResult> Update(UsuarioModel usuario);

        Task<IdentityResult> Validate(UsuarioModel usuario);

        string HashPassword(string contrasena);
    }
}

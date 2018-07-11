namespace Clinica.Services.Repositories
{
    using Clinica.DataAccess.Entities;
    using Microsoft.AspNet.Identity;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IIdentityRepository
    {
        IQueryable<Usuario> GetAll();

        Usuario GetByEmail(string email);

        Task<IdentityResult> Create(Usuario usuario, string contrasena);

        Task<IdentityResult> Delete(Usuario usuario);

        Task<IdentityResult> Update(Usuario usuario);

        UserManager<Usuario> GetUserManager();
    }
}

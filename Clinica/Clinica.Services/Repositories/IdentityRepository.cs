namespace Clinica.Services.Repositories
{
    using Clinica.DataAccess.Entities;
    using Microsoft.AspNet.Identity;
    using System.Linq;
    using System.Threading.Tasks;

    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<Usuario> myUserManager;

        public IdentityRepository(UserManager<Usuario> userManager)
        {
            myUserManager = userManager;
        }

        public IQueryable<Usuario> GetAll() => myUserManager.Users;

        public Usuario GetByEmail(string email) => this.GetAll().First(u => u.Email == email);

        public Task<IdentityResult> Create(Usuario usuario, string password)
        {
            return myUserManager.CreateAsync(usuario, password);
        }

        public async Task<IdentityResult> Delete(Usuario usuario)
        {
            return await myUserManager.DeleteAsync(usuario);
        }

        public async Task<IdentityResult> Update(Usuario usuario)
        {
            return await myUserManager.UpdateAsync(usuario);
        }

        public UserManager<Usuario> GetUserManager()
        {
            return myUserManager;
        }
    }
}

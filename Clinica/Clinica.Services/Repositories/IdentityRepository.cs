namespace Clinica.Services.Repositories
{
    using Clinica.DataAccess.Entities;
    using Microsoft.AspNet.Identity;
    using System.Linq;
    using System.Threading.Tasks;

    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<Usuario, string> myUserManager;

        public IdentityRepository(UserManager<Usuario, string> userManager)
        {
            myUserManager = userManager;
        }

        public IQueryable<Usuario> GetAll() => myUserManager.Users;

        public Usuario GetByEmail(string email) => this.GetAll().First(u => u.Email == email);

        public async Task<Usuario> FindUser(string userName, string password)
        {
            Usuario user = await this.myUserManager.FindAsync(userName, password);

            return user;
        }

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

        public UserManager<Usuario, string> GetUserManager()
        {
            return myUserManager;
        }
    }
}

namespace Clinica.Services.Services.Services.Implementation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Clinica.DataAccess.Entities;
    using Clinica.Services.Models;
    using Clinica.Services.Repositories;
    using Clinica.Services.Services.Services.Interfaces;
    using Microsoft.AspNet.Identity;
    using Clinica.Extensiones.Main;

    /// <summary>
    /// Implementacion de la interfaz IUsuariosRepository
    /// </summary>
    public class UsuariosRepository : IUsuariosRepository
    {
        #region Fields
        private readonly IIdentityRepository myIdentity;
        private readonly IMapper myMapper;
        private readonly IIdentityValidator<Usuario> myValidator;
        private readonly IPasswordHasher myPasswordHasher;
        //private readonly SignInManager<Usuario> myManager;
        #endregion

        #region Construction
        public UsuariosRepository(IIdentityRepository identity, IMapper mapper, IIdentityValidator<Usuario> validator, IPasswordHasher hasher)
        {
            this.myIdentity = identity;
            this.myMapper = mapper;
            this.myValidator = validator;
            this.myPasswordHasher = hasher;
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Obtener todos los usuarios
        /// </summary>
        /// <returns></returns>
        public IQueryable<UsuarioModel> GetAll()
        {
            var result = new List<UsuarioModel>();
            this.myIdentity.GetAll().ToList().ForEach(u =>
            {
                result.Add(this.myMapper.Map<Usuario, UsuarioModel>(u));
            });

            return result.AsQueryable();
        }

        /// <summary>
        /// Obtener usuario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UsuarioModel GetById(string id)
        {
            var user = this.myIdentity.GetAll().FirstOrDefault(u => u.Id.Equals(id));
            return user != null ? myMapper.Map<Usuario, UsuarioModel>(user) : null;
        }

        /// <summary>
        /// Obtener usuario por numero de identificacion
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public UsuarioModel GetByUserId(string idUsuario)
        {
            var user = this.myIdentity.GetAll().FirstOrDefault(u => u.NumeroIdentificacion.Equals(idUsuario));
            return user != null ? myMapper.Map<Usuario, UsuarioModel>(user) : null;
        }

        /// <summary>
        /// Obtener usuario por correo electronico
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UsuarioModel GetByEmail(string email)
        {
            return myMapper.Map<Usuario, UsuarioModel>(this.myIdentity.GetByEmail(email));
        }

        /// <summary>
        /// Obtener usuarios por estado
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IQueryable<UsuarioModel> GetByStatus(string codigo)
        {
            var result = new List<UsuarioModel>();
            this.myIdentity.GetAll().Where(u => u.Estado.Equals(codigo)).ToList().ForEach(u =>
            {
                result.Add(this.myMapper.Map<Usuario, UsuarioModel>(u));
            });

            return result.AsQueryable();
        }

        /// <summary>
        /// Obtener usuarios por tipo
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IQueryable<UsuarioModel> GetByType(string codigo)
        {
            var result = new List<UsuarioModel>();
            this.myIdentity.GetAll().Where(u => u.TipoUsuario.Codigo.Equals(codigo)).ToList().ForEach(u =>
            {
                result.Add(this.myMapper.Map<Usuario, UsuarioModel>(u));
            });

            return result.AsQueryable();
        }

        /// <summary>
        /// Creacion de usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="contrasena"></param>
        /// <returns></returns>
        public async Task<IdentityResult> Create(UsuarioModel usuario, string contrasena)
        {
            Usuario user = this.myMapper.Map<UsuarioModel, Usuario>(usuario);
            var hashedPass = this.HashPassword(contrasena);

            return await this.myIdentity.Create(user, hashedPass);
        }

        /// <summary>
        /// Borrado de usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<IdentityResult> Delete(UsuarioModel usuario)
        {
            return await this.myIdentity.Delete(this.myMapper.Map<UsuarioModel, Usuario>(usuario));
        }

        /// <summary>
        /// Actualización de usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<IdentityResult> Update(UsuarioModel usuario)
        {
            return await this.myIdentity.Update(this.myMapper.Map<UsuarioModel, Usuario>(usuario));
        }

        /// <summary>
        /// Validación de usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<IdentityResult> Validate(UsuarioModel usuario)
        {
            var user = this.myMapper.Map<UsuarioModel, Usuario>(usuario);
            return await this.myValidator.ValidateAsync(user);
        }

        /// <summary>
        /// Hasheo de contraseña de usuario
        /// </summary>
        /// <param name="contrasena"></param>
        /// <returns></returns>
        public string HashPassword(string contrasena)
        {
            return this.myPasswordHasher.HashPassword(contrasena);
        }
        #endregion
    }
}

namespace Clinica.Services.Services.Implementation
{
    using Clinica.DataAccess.Entities;
    using Clinica.Services.Repositories;
    using Clinica.Services.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementación de interfaz para servicio base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        #region Fields
        private readonly IBaseRepository<T> myRepository;
        #endregion

        #region Construction
        public BaseService(IBaseRepository<T> repository)
        {
            this.myRepository = repository;
        }

        #endregion

        #region Implementation
        public async Task<IEnumerable<T>> Get()
        {
            return await this.myRepository.GetAll();
        }

        public async Task<T> GetById(int id)
        {
            return await this.myRepository.GetById(id);
        }

        public int Add(T entrada)
        {
            return this.myRepository.Insert(entrada);
        }

        public async Task<int> Update(T entrada)
        {
            var target = this.myRepository.GetById(entrada.Id).Result;
            var exists = target != null;

            if (exists)
            {
                return await this.myRepository.Update(entrada);
            }

            return 0;
        }

        public int Remove(int id)
        {
            var target = this.myRepository.GetById(id).Result;
            return this.myRepository.Delete(target);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> expresion)
        {
            return this.myRepository.Where(expresion);
        }
        #endregion
    }
}

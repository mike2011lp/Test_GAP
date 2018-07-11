namespace Clinica.Services.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Interfaz base para patrón repositorio
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(object Id);

        IEnumerable<T> Where(Expression<Func<T, bool>> expression);

        T Insert(T entity);

        T Update(T entity);

        void Delete(T entity);

        void Save();
    }
}

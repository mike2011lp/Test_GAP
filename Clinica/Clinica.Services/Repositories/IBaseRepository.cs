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

        Task<T> GetById(int Id);

        IEnumerable<T> Where(Expression<Func<T, bool>> expression);

        int Insert(T entity);

        Task<int> Update(T entity);

        int Delete(T entity);
    }
}

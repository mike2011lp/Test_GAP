namespace Clinica.Services.Services.Interfaces
{
    using Clinica.DataAccess.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Interfaz Base de servicios
    /// </summary>
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> Get();

        Task<T> GetById(int id);

        IEnumerable<T> Where(Expression<Func<T, bool>> expresion);

        int Add(T entrada);

        Task<int> Update(T entrada);

        int Remove(int id);
    }
}

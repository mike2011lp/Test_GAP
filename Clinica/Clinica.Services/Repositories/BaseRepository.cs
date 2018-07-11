namespace Clinica.Services.Repositories
{
    using Clinica.DataAccess.Context;
    using Clinica.Services.Handlers;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of Base Repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Fields
        private readonly DataContext myContext;

        private readonly DbSet<T> myEntities;

        private readonly IErrorHandler myErrorHandler;
        #endregion

        #region Construction
        public BaseRepository(DataContext dataContext)
        {
            this.myContext = dataContext;
        }
        #endregion

        #region Interface Implementation
        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(object Id)
        {
            throw new NotImplementedException();
        }

        public T Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

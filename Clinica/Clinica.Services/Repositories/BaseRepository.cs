namespace Clinica.Services.Repositories
{
    using Clinica.DataAccess.Context;
    using Clinica.DataAccess.Entities;
    using Clinica.Recursos.ResourceFiles;
    using Clinica.Services.Enums;
    using Clinica.Services.Handlers;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Linq;

    /// <summary>
    /// Implementación de interfaz base para repositorio
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        #region Fields
        private readonly DataContext myContext;

        private readonly DbSet<T> myEntities;

        private readonly IErrorHandler myErrorHandler;
        #endregion

        #region Construction
        public BaseRepository(DataContext dataContext, IErrorHandler errorHandler)
        {
            this.myContext = dataContext;
            this.myEntities = dataContext.Set<T>();
            this.myErrorHandler = errorHandler;
        }
        #endregion

        #region Interface Implementation
        public async Task<IEnumerable<T>> GetAll()
        {
            return await this.myEntities.ToListAsync();
        }

        public async Task<T> GetById(int Id)
        {
            return await this.myEntities.SingleOrDefaultAsync(s => s.Id.CompareTo(Id) == 0);
        }

        public int Insert(T entity)
        {
            //Si la entidad es nula, invocar al manejador de errores y lanzar excepción
            if (entity == null)
            {
                throw new ArgumentNullException(string.Format(this.myErrorHandler.GetMessage(MensajesErrorEnum.EntidadNula), entity.GetType().Name, Messages.MSG_DB_ERR_INPUT_NULL));
            }

            this.myContext.Entry(entity).State = EntityState.Added;

            //Añadir la entidad
            this.myEntities.Add(entity);

            //Retornar el número de registros afectados
            return this.myContext.SaveChanges();
        }

        public async Task<int> Update(T entity)
        {
            //Si la entidad es nula, invocar al manejador de errores y lanzar excepción
            if (entity == null)
            {
                throw new ArgumentNullException(string.Format(this.myErrorHandler.GetMessage(MensajesErrorEnum.EntidadNula), entity.GetType().Name, Messages.MSG_DB_ERR_INPUT_NULL));
            }

            var oldEntity = await this.myEntities.FindAsync(entity.Id);
            this.myContext.Entry(oldEntity).CurrentValues.SetValues(entity);
            return this.myContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            //Si la entidad es nula, invocar al manejador de errores y lanzar excepción
            if (entity == null)
            {
                throw new ArgumentNullException(string.Format(this.myErrorHandler.GetMessage(MensajesErrorEnum.EntidadNula), entity.GetType().Name, Messages.MSG_DB_ERR_INPUT_NULL));
            }

            this.myEntities.Remove(entity);
            return this.myContext.SaveChanges();
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> expression)
        {
            return this.myEntities.Where(expression);
        }
        #endregion
    }
}

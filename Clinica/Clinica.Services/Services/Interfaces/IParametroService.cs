namespace Clinica.Services.Services.Services.Interfaces
{
    using Clinica.DataAccess.Entities;
    using Clinica.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Interfaz de servicio para parámetros
    /// </summary>
    public interface IParametroService
    {
        int Add(ParametroModel entrada);

        Task<int> Update(ParametroModel entrada);

        Task<IEnumerable<ParametroModel>> Get();

        Task<ParametroModel> GetById(int id);

        int Remove(int id);

        IEnumerable<ParametroModel> Where(Expression<Func<Parametro, bool>> exp);
    }
}

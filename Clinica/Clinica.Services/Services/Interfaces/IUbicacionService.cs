namespace Clinica.Services.Services.Services.Interfaces
{
    using Clinica.DataAccess.Entities;
    using Clinica.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Interfaz de servicio para ubicación
    /// </summary>
    public interface IUbicacionService
    {
        int Add(UbicacionModel entrada);

        Task<int> Update(UbicacionModel entrada);

        Task<IEnumerable<UbicacionModel>> Get();

        Task<UbicacionModel> GetById(int id);

        int Remove(int id);

        IEnumerable<UbicacionModel> Where(Expression<Func<Ubicacion, bool>> exp);
    }
}

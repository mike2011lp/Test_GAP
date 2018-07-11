using Clinica.DataAccess.Entities;
using Clinica.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clinica.Services.Services.Services.Interfaces
{
    /// <summary>
    /// Interfaz de servicio para citas
    /// </summary>
    public interface ICitaService
    {
        int Add(CitaModel entrada);

        Task<int> Update(CitaModel entrada);

        Task<IEnumerable<CitaModel>> Get();

        Task<CitaModel> GetById(int id);

        int Remove(int id);

        IEnumerable<CitaModel> Where(Expression<Func<Cita, bool>> exp);
    }
}

namespace Clinica.Services.Services.Services.Implementation
{
    using AutoMapper;
    using Clinica.DataAccess.Entities;
    using Clinica.Services.Models;
    using Clinica.Services.Services.Interfaces;
    using Clinica.Services.Services.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementación de servicio para cita
    /// </summary>
    public class CitaService : ICitaService
    {
        #region Fields
        private readonly IBaseService<Cita> myService;
        private readonly IMapper myMapper;
        #endregion

        #region Construction
        public CitaService(IBaseService<Cita> service, IMapper mapper)
        {
            this.myService = service;
            this.myMapper = mapper;
        }

        #endregion

        #region Implementation
        public async Task<IEnumerable<CitaModel>> Get()
        {
            var result = await this.myService.Get();
            return result.Select(t => this.myMapper.Map<Cita, CitaModel>(t));
        }

        public async Task<CitaModel> GetById(int id)
        {
            var item = await this.myService.GetById(id);

            return this.myMapper.Map<Cita, CitaModel>(item);
        }

        public int Add(CitaModel entrada)
        {
            return this.myService.Add(this.myMapper.Map<CitaModel, Cita>(entrada));
        }

        public async Task<int> Update(CitaModel entrada)
        {
            return await this.myService.Update(this.myMapper.Map<CitaModel, Cita>(entrada));
        }

        public int Remove(int id)
        {
            return this.myService.Remove(id);
        }

        public IEnumerable<CitaModel> Where(Expression<Func<Cita, bool>> expresion)
        {
            var result = this.myService.Where(expresion).ToList();
            return this.myMapper.Map<List<Cita>, List<CitaModel>>(result).AsEnumerable();
        }
        #endregion
    }
}

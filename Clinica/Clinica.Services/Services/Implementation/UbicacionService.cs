namespace Clinica.Services.Services.Services.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using AutoMapper;
    using Clinica.DataAccess.Entities;
    using Clinica.Services.Models;
    using Clinica.Services.Services.Interfaces;
    using Clinica.Services.Services.Services.Interfaces;
    using System.Linq;

    /// <summary>
    /// Implementacion de servicio para ubicación
    /// </summary>
    public class UbicacionService : IUbicacionService
    {
        #region Fields
        private readonly IBaseService<Ubicacion> myService;
        private readonly IMapper myMapper;
        #endregion

        #region Construction
        public UbicacionService(IBaseService<Ubicacion> service, IMapper mapper)
        {
            this.myService = service;
            this.myMapper = mapper;
        }

        #endregion

        #region Implementation
        public async Task<IEnumerable<UbicacionModel>> Get()
        {
            var result = await this.myService.Get();
            return result.Select(t => this.myMapper.Map<Ubicacion, UbicacionModel>(t));
        }

        public async Task<UbicacionModel> GetById(int id)
        {
            var item = await this.myService.GetById(id);

            return this.myMapper.Map<Ubicacion, UbicacionModel>(item);
        }

        public int Add(UbicacionModel entrada)
        {
            return this.myService.Add(this.myMapper.Map<UbicacionModel, Ubicacion>(entrada));
        }

        public async Task<int> Update(UbicacionModel entrada)
        {
            return await this.myService.Update(this.myMapper.Map<UbicacionModel, Ubicacion>(entrada));
        }

        public int Remove(int id)
        {
            return this.myService.Remove(id);
        }

        public IEnumerable<UbicacionModel> Where(Expression<Func<Ubicacion, bool>> expresion)
        {
            var result = this.myService.Where(expresion).ToList();
            return this.myMapper.Map<List<Ubicacion>, List<UbicacionModel>>(result).AsEnumerable();
        }
        #endregion
    }
}

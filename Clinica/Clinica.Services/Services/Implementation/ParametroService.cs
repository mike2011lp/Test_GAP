namespace Clinica.Services.Services.Services.Implementation
{
    using AutoMapper;
    using Clinica.DataAccess.Entities;
    using Clinica.Services.Models;
    using Clinica.Services.Services.Interfaces;
    using Clinica.Services.Services.Services.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Linq.Expressions;
    using System;

    /// <summary>
    /// Implementación de servicio para parámetros
    /// </summary>
    public class ParametroService : IParametroService
    {
        #region Fields
        private readonly IBaseService<Parametro> myService;
        private readonly IMapper myMapper;
        #endregion

        #region Construction
        public ParametroService(IBaseService<Parametro> service, IMapper mapper)
        {
            this.myService = service;
            this.myMapper = mapper;
        }

        #endregion

        #region Implementation
        public async Task<IEnumerable<ParametroModel>> Get()
        {
            var result = await this.myService.Get();
            return result.Select(t => this.myMapper.Map<Parametro, ParametroModel>(t));
        }

        public async Task<ParametroModel> GetById(int id)
        {
            var item = await this.myService.GetById(id);

            return this.myMapper.Map<Parametro, ParametroModel>(item);
        }

        public int Add(ParametroModel entrada)
        {
            return this.myService.Add(this.myMapper.Map<ParametroModel, Parametro>(entrada));
        }

        public async Task<int> Update(ParametroModel entrada)
        {
            return await this.myService.Update(this.myMapper.Map<ParametroModel, Parametro>(entrada));
        }

        public int Remove(int id)
        {
            return this.myService.Remove(id);
        }

        public IEnumerable<ParametroModel> Where(Expression<Func<Parametro, bool>> expresion)
        {
            var result = this.myService.Where(expresion).ToList();
            return this.myMapper.Map<List<Parametro>, List<ParametroModel>>(result).AsEnumerable();
        }
        #endregion
    }
}

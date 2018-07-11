using AutoMapper;
using Clinica.DataAccess.Entities;
using Clinica.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Services.Mapping
{
    public class AutoMapperBase
    {
        protected readonly IMapper myMapper;

        public AutoMapperBase()
        {
            var config = new MapperConfiguration(x =>
            {
                x.CreateMap<UsuarioModel, Usuario>();
            });

            myMapper = config.CreateMapper();
        }

        public IMapper Mapper
        {
            get
            {
                return this.myMapper;
            }
        }
    }
}

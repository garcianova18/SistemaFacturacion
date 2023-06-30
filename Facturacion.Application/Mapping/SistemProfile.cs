using AutoMapper;
using Facturacion.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturacion.Domain.DTOs;

namespace Facturacion.Application.Mapping
{
    public class SistemProfile : AutoMapper.Profile
    {
        public SistemProfile()
        {
            CreateMap<Cliente, ClientDTO>().ReverseMap();
            CreateMap<Cliente, ClientCreateDTO>().ReverseMap();
            CreateMap<Cliente, ClientUpdateDTO>().ReverseMap();

        }
    }
}

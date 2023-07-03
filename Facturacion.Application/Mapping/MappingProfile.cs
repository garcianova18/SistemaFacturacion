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
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<Client, ClientCreateDTO>().ReverseMap();
            CreateMap<Client, ClientUpdateDTO>().ReverseMap();

            CreateMap<Invoice, InvoiceCreateDTO>().ReverseMap();
            CreateMap<Invoice, InvoiceDTO>().ReverseMap();
            CreateMap<Invoice, InvoiceWhitDetailsDTO>().ReverseMap();
            CreateMap<Invoice, InvoiceUpdateDTO>().ReverseMap();

            CreateMap<InvoiceDetail, invoiceDetailsDTO>().ReverseMap();
            CreateMap<InvoiceDetail, InvoiceDetailsCreateDTO>().ReverseMap();
            CreateMap<InvoiceDetail, InvoiceDetailsUpdateDTO>().ReverseMap();


            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
            CreateMap<Category, CategoryUpdateDTO>().ReverseMap();


            CreateMap<User, UserDTO>().ReverseMap();

        }
    }
}

using AutoMapper;
using E_Commerce_Product_Management.Dto;
using E_Commerce_Product_Management.Models;

namespace E_Commerce_Product_Management.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductWriteDto>();
            CreateMap<ProductWriteDto, Product>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryWriteDto>();
            CreateMap<CategoryWriteDto, Category>();

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderWriteDto>();
            CreateMap<OrderWriteDto, Order>();
        }
    }
}

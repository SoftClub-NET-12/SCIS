using AutoMapper;
using Domain.DTOs.CategoryDTO;
using Domain.DTOs.LocationDTO;
using Domain.DTOs.PriceHistoryDTO;
using Domain.DTOs.ProductDTO;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Location, AddLocationDto>().ReverseMap();
        CreateMap<Location, UpdateLocationDto>().ReverseMap();
        CreateMap<Location, GetLocationsDto>().ReverseMap();

        CreateMap<Category, AddCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        CreateMap<Category, GetCategoriesDto>().ReverseMap();
        

        CreateMap<Product, AddProductDto>().ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();
        CreateMap<Product, GetProductDto>().ReverseMap();

        CreateMap<PriceHistory, AddPriceHistoryDto>().ReverseMap();
        CreateMap<PriceHistory, UpdatePriceHistoryDto>().ReverseMap();
        CreateMap<PriceHistory, GetPriceHistoryDto>().ReverseMap();
   
    }
}

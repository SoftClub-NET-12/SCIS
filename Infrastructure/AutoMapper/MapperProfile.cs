using AutoMapper;
using Domain.DTOs.CategoryDto;
using Domain.DTOs.LocationDto;
using Domain.DTOs.PriceHistoryDto;
using Domain.DTOs.ProductDto;
using Domain.DTOs.ProductSupplierDto;
using Domain.DTOs.PurchaseDto;
using Domain.DTOs.SaleDto;
using Domain.DTOs.StockDto;
using Domain.DTOs.StockProductDto;
using Domain.DTOs.SupplierDto;
using Domain.DTOs.UserDto;
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
        CreateMap<Category, GetCategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();

        CreateMap<Location, GetLocationDto>().ReverseMap();
        CreateMap<Location, CreateLocationDto>().ReverseMap();
        CreateMap<Location, UpdateLocationDto>().ReverseMap();

        CreateMap<PriceHistory, GetPriceHistoryDto>().ReverseMap();
        CreateMap<PriceHistory, CreatePriceHistoryDto>().ReverseMap();
        CreateMap<PriceHistory, UpdatePriceHistoryDto>().ReverseMap();
        
        CreateMap<Product, GetProductDto>().ReverseMap();
        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();

        CreateMap<ProductSupplier, GetProductSupplierDto>().ReverseMap();
        CreateMap<ProductSupplier, CreateProductSupplierDto>().ReverseMap();
        CreateMap<ProductSupplier, UpdateProductSupplierDto>().ReverseMap();

        CreateMap<Purchase, GetPurchaseDto>().ReverseMap();
        CreateMap<Purchase, CreatePurchaseDto>().ReverseMap();
        CreateMap<Purchase, UpdatePurchaseDto>().ReverseMap();

        CreateMap<Sale, GetSaleDto>().ReverseMap();
        CreateMap<Sale, CreateSaleDto>().ReverseMap();
        CreateMap<Sale, UpdateSaleDto>().ReverseMap();

        CreateMap<Stock, GetStockDto>().ReverseMap();
        CreateMap<Stock, CreateStockDto>().ReverseMap();
        CreateMap<Stock, UpdateStockDto>().ReverseMap();

        CreateMap<StockProduct, GetStockProductDto>().ReverseMap();
        CreateMap<StockProduct, CreateStockProductDto>().ReverseMap();
        CreateMap<StockProduct, UpdateStockProductDto>().ReverseMap();

        CreateMap<Supplier, GetSupplierDto>().ReverseMap();
        CreateMap<Supplier, CreateSupplierDto>().ReverseMap();
        CreateMap<Supplier, UpdateSupplierDto>().ReverseMap();

        CreateMap<User, GetUserDto>().ReverseMap();
        CreateMap<User, CreateUserDto>().ReverseMap();
        CreateMap<User, UpdateUserDto>().ReverseMap();
  
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

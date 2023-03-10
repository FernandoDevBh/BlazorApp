using Models;
using AutoMapper;
using DataAccess;
using DataAccess.ViewModel;

namespace Business.Mapper;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Category, CategoryDTO>().ReverseMap();
		CreateMap<Product, ProductDTO>().ReverseMap();
		CreateMap<ProductPrice, ProductPriceDTO>().ReverseMap();
        CreateMap<OrderHeader, OrderHeaderDTO>().ReverseMap();
        CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
        CreateMap<Order, OrderDTO>().ReverseMap();
    }
}

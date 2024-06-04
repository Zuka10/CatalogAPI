using AutoMapper;
using Ecommerce.DTO;

namespace Catalog.gRPCService;

public static class MapperConfig
{
    public static Mapper InitializeAutoMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Category, CategoryDTO>();
            cfg.CreateMap<CategoryDTO, Category>()
                .ForMember(c => c.Products, option => option.Ignore());
        });

        var mapper = new Mapper(config);
        return mapper;
    }
}
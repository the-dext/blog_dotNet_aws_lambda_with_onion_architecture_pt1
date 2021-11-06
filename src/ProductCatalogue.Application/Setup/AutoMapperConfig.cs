using AutoMapper;
using ProductCatalogue.Domain.Products;

namespace ProductCatalogue.Application.Setup
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            this.CreateMap<Product, Dtos.ProductDto>()
                .ForMember(x => x.Price, cfg => cfg.MapFrom(s => s.Price.Value));
        }
    }
}

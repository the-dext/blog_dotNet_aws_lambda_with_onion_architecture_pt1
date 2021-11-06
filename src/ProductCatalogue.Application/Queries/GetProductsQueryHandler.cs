using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using ProductCatalogue.Application.Dtos;
using ProductCatalogue.Domain.Repositories;

namespace ProductCatalogue.Application.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductsRepository productsRepository, IMapper mapper)
        {
            this._productsRepository = Guard.Against.Null(productsRepository, nameof(productsRepository));
            this._mapper = Guard.Against.Null(mapper, nameof(mapper));
        }
        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request, nameof(request));

            var products = await this._productsRepository.GetAllAsync(request.TenantId, cancellationToken);
            return this._mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}

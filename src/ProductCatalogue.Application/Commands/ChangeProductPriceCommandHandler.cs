using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using ProductCatalogue.Domain.Products;
using ProductCatalogue.Domain.Repositories;

namespace ProductCatalogue.Application.Commands
{
    public class ChangeProductPriceCommandHandler : IRequestHandler<ChangeProductPriceCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductsRepository _productsRepository;

        public ChangeProductPriceCommandHandler(IProductsRepository productsRepository, IUnitOfWork unitOfWork)
        {
            _productsRepository = Guard.Against.Null(productsRepository, nameof(productsRepository));
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(ChangeProductPriceCommand cmd, CancellationToken cancellationToken)
        {
            Guard.Against.Null(cmd, nameof(cmd));

            var product = await _productsRepository.GetBySkuAsync(cmd.TenantId, cmd.Sku, cancellationToken);
            _ = product ?? throw new NotFoundException(cmd.Sku, nameof(product));

            product.ChangePrice(new Price(cmd.NewPrice));
            _productsRepository.Save(product);
            _unitOfWork.Commit(product); // Not really how it would be done in a real app. Change tracker would be used but this is fine for an example.
            return true;
        }
    }
}

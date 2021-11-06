using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProductCatalogue.Domain.Products;

namespace ProductCatalogue.Domain.Repositories
{
    public interface IProductsRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllAsync(Guid tenantId, CancellationToken cancellationToken);

        Task<Product> GetBySkuAsync(Guid tenantId, string sku, CancellationToken cancellationToken);
    }
}

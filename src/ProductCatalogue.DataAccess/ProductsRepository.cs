using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using ProductCatalogue.Domain.Products;
using ProductCatalogue.Domain.Repositories;

namespace ProductCatalogue.Persistence
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly List<Product> _products;

        public ProductsRepository()
        {
            // fake database..
            _products = new List<Product>()
            {
                Product.ListNewProduct(Guid.Parse("743872ea-7e68-421b-9f98-e09f35d76117"), "HOU/PP/1", "Plant pot", "Medium sized plat pot", 10.00m),
                Product.ListNewProduct(Guid.Parse("743872ea-7e68-421b-9f98-e09f35d76117"), "HOU/IN/82", "Book shelves", "150cm x 180cm Book shelf unit", 125.00m),
                Product.ListNewProduct(Guid.Parse("743872ea-7e68-421b-9f98-e09f35d76117"), "HOU/PL/8", "House plant", "small cactus", 1m),
                Product.ListNewProduct(Guid.Parse("743872ea-7e68-421b-9f98-e09f35d76117"), "KIT/CH/1A", "Dining chairs", "A set of 4 dining chairs", 90m),
                Product.ListNewProduct(Guid.Parse("743872ea-7e68-421b-9f98-e09f35d76117"), "KIT/DT/8B", "Dining table", "A small ", 90m),
            };
        }

        public Task<IEnumerable<Product>> GetAllAsync(Guid tenantId, CancellationToken cancellationToken)
        {
            Guard.Against.Default(tenantId, nameof(tenantId));
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(this._products.AsEnumerable());
        }

        public Task<Product> GetBySkuAsync(Guid tenantId, string sku, CancellationToken cancellationToken)
        {
            Guard.Against.Default(tenantId, nameof(tenantId));
            Guard.Against.NullOrWhiteSpace(sku, nameof(sku));
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(this._products.SingleOrDefault(x => x.TenantId == tenantId && x.Sku == sku));
        }

        public void Save(Product aggregate)
        {
            var productIndex = _products.FindIndex(x => x.Sku == aggregate.Sku);
            if (productIndex > -1)
            {
                _products[productIndex] = aggregate;
            }
        }
    }
}

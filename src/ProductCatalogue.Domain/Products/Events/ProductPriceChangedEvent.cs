using Ardalis.GuardClauses;
using ProductCatalogue.Domain.BaseTypes;

namespace ProductCatalogue.Domain.Products.Events
{
    public class ProductPriceChangedEvent : IDomainEvent
    {
        public Price OldPrice { get; }
        public Product Product { get; }

        public ProductPriceChangedEvent(Product product, Price oldPrice)
        {
            OldPrice = oldPrice;
            Product = Guard.Against.Null(product, nameof(product));
        }
    }
}

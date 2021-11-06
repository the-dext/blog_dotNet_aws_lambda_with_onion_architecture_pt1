using System;
using Ardalis.GuardClauses;
using MediatR;

namespace ProductCatalogue.Application.Commands
{
    public class ChangeProductPriceCommand : IRequest<bool>
    {
        public ChangeProductPriceCommand()
        {
        }

        public ChangeProductPriceCommand(Guid tenantId, string sku, decimal newPrice)
        {
            this.TenantId = Guard.Against.Default(tenantId, nameof(tenantId));
            this.Sku = Guard.Against.NullOrWhiteSpace(sku, nameof(sku));
            this.NewPrice = Guard.Against.Negative(newPrice, nameof(newPrice));
        }

        public string Sku { get; set; }

        public Guid TenantId { get; set; }

        public decimal NewPrice { get; set; }
    }
}

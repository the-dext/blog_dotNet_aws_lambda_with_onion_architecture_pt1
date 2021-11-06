using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using MediatR;
using ProductCatalogue.Application.Dtos;

namespace ProductCatalogue.Application.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
        public GetProductsQuery(Guid tenantId)
        {
            this.TenantId = Guard.Against.Default(tenantId, nameof(tenantId));
        }

        public Guid TenantId { get; private set; }
    }
}

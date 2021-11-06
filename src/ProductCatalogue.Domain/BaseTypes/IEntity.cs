using System;

namespace ProductCatalogue.Domain.BaseTypes
{
    public interface IEntity
    {
        Guid Id { get; }
        Guid TenantId { get; }
    }
}
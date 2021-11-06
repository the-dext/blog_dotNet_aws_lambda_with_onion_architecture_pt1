using System;

namespace ProductCatalogue.Application.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }
    }
}

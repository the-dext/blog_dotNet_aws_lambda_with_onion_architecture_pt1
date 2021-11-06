using System.Collections.Generic;
using Ardalis.GuardClauses;
using ProductCatalogue.Domain.BaseTypes;

namespace ProductCatalogue.Domain.Products
{
    public class Price : ValueObject
    {
        public Price(decimal price, string langCode = "gb")
        {
            this.Value = Guard.Against.NegativeOrZero(price, nameof(price));
            this.LanguageCode = Guard.Against.NullOrWhiteSpace(langCode, nameof(langCode));
        }

        public string LanguageCode { get; }

        public decimal Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Value;
            yield return this.LanguageCode;
        }
    }
}

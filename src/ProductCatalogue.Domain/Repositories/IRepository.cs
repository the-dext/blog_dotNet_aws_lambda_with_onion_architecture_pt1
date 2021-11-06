using ProductCatalogue.Domain.BaseTypes;

namespace ProductCatalogue.Domain.Repositories
{
    // enforces repositories to only return aggregate roots
    public interface IRepository<T> where T : IAggregateRoot
    {
        public void Save(T aggregate);
    }
}

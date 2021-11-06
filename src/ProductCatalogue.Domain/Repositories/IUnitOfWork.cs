using ProductCatalogue.Domain.BaseTypes;

namespace ProductCatalogue.Domain.Repositories
{
    /// <summary>
    /// This wouldn't be implemented this way, it would use change tracking and not require the aggregate to be explicitly passed in.
    /// </summary>
    public interface IUnitOfWork
    {
        public void Commit(IAggregateRoot aggregate);
    }
}

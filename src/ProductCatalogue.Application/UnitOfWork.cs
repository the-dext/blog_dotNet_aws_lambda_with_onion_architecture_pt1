using MediatR;
using ProductCatalogue.Domain.BaseTypes;
using ProductCatalogue.Domain.Repositories;

namespace ProductCatalogue.Application
{
    /// <summary>
    /// Basic unit of work, a real one would not be implemented this way in that it wouldn't need the aggregate explicitly passing in.
    /// It would use change tracking or something like that to dispatch the pending events.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMediator _mediator;

        public UnitOfWork(IMediator mediator)
        {
            _mediator = mediator;
        }
        public void Commit(IAggregateRoot aggregate)
        {
            foreach (var evnt in aggregate.DeQueueEvents())
            {
                _mediator.Publish(evnt);
            }
        }
    }
}

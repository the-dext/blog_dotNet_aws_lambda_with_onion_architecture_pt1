using System;
using System.Collections.Generic;

namespace ProductCatalogue.Domain.BaseTypes
{
    public class AggregateRoot : IAggregateRoot
    {
        private Queue<IDomainEvent> _pendingEvents;

        public AggregateRoot()
        {
            _pendingEvents = new Queue<IDomainEvent>();
        }


        public Guid Id { get; }
        public Guid TenantId { get; }
        public void QueueEvent(IDomainEvent evnt)
        {
            _pendingEvents.Enqueue(evnt);
        }

        public IEnumerable<IDomainEvent> DeQueueEvents()
        {
            while (_pendingEvents.Count > 0)
            {
                yield return _pendingEvents.Dequeue();
            };
        }
    }
}
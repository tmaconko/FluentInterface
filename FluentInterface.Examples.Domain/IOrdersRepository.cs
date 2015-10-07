using System.Collections.Generic;

namespace FluentInterface.Examples.Domain
{
    public interface IOrdersRepository
    {
        Order GetById(int orderId);
        void Store(Order order);
        IEnumerable<Order> ListInitiated();
        IEnumerable<Order> ListPaid();
        IEnumerable<Order> ListProcessed();
        IEnumerable<Order> ListFinished();
    }
}
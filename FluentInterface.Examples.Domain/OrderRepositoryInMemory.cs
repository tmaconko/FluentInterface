using System.Collections.Generic;
using System.Linq;

namespace FluentInterface.Examples.Domain
{
    public class OrderRepositoryInMemory : IOrdersRepository
    {
        public IList<Order> Orders;

        public OrderRepositoryInMemory()
        {
            Orders = new List<Order>();
        }

        public Order GetById(int orderId)
        {
            return Orders.First(p => p.Id == orderId);
        }

        public void Store(Order order)
        {
            var storedOrder = Orders.First(p => p.Id == order.Id);
            Orders.Remove(storedOrder);
            Orders.Add(order);
        }

        public IEnumerable<Order> ListInitiated()
        {
            return Orders.Where(p => p.Status == OrderStatus.Initiated);
        }

        public IEnumerable<Order> ListPaid()
        {
            return Orders.Where(p => p.Status == OrderStatus.Paid);
        }

        public IEnumerable<Order> ListProcessed()
        {
            return Orders.Where(p => p.Status == OrderStatus.Processed);
        }

        public IEnumerable<Order> ListFinished()
        {
            return Orders.Where(p => p.Status == OrderStatus.Finished);
        }
    }
}
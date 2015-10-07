using System;
using System.Collections.Generic;

namespace FluentInterface.Examples.Domain
{
    public class Order
    {
        public int Id;
        public DateTime CreationDate;
        public DateTime? FinishDate;
        public IList<OrderLine> OrderLines;
        public string Notes;
        public Shop Shop;
        public Client Initiator;
        public OrderStatus Status;
    }
}

using System;
using System.Collections.Generic;
using FluentInterface.Examples.Domain;

namespace FluentInterface.Examples.Tests.DomainBuilders
{
    public static class PatchingOrderBuilder
    {
        static PatchingOrderBuilder()
        {
            OrderRepository = new OrderRepositoryInMemory();
            ClientsRepository = new ClientsRepositoryInMemory();
            StorageItemsRepository = new StorageItemsRepositoryInMemory();
        }

        public static readonly OrderRepositoryInMemory OrderRepository;
        public static readonly ClientsRepositoryInMemory ClientsRepository;
        public static readonly StorageItemsRepositoryInMemory StorageItemsRepository;

        public static INewOrderPart CreateOrder(bool storeInRepository = true)
        {
            int id = new Random().Next();

            return new OrderParts(id, storeInRepository);
        }

        private class OrderParts : INewOrderPart, IInitiatedOrderPart, IOrderWithClientPart, IOrderWithShopPart
        {
            private readonly int _id;
            private readonly bool _hasToHasToStoreInRepository;

            private DateTime _creationDate;
            private DateTime? _finishDate;
            private Client _client;
            private Shop _shop;
            private OrderStatus _status;

            private readonly List<OrderLine> _orderLines;
            
            public OrderParts(int id, bool hasToStoreInRepository)
            {
                _id = id;
                _hasToHasToStoreInRepository = hasToStoreInRepository;

                _orderLines = new List<OrderLine>();
            }

            public IInitiatedOrderPart InitiatedOn(DateTime date)
            {  
                _status = OrderStatus.Initiated;
                _creationDate = date;

                return this;
            }

            public IOrderWithClientPart By(Client client, bool storeInRepository = true)
            {
                _client = client;

                if (storeInRepository)
                {
                    ClientsRepository.Clients.Add(client);
                }

                return this;
            }

            public IOrderWithClientPart ByDummyClient(bool storeInRepository = true)
            {
                var address = new Address
                {
                    AddressLine = "RandomStreet 1",
                    City = "RandomCity",
                    PostalCode = "RandomPostalCode",
                    IsDefault = true
                };

                var email = new Email
                {
                    EmailString = "random@email.com",
                    IsDefault = true
                };

                var client = new Client
                {
                    Addresses = new List<Address> { address },
                    Balance = 100,
                    Emails = new List<Email> { email },
                    FirstName = "RandomFirstName",
                    Id = new Random().Next(),
                    LastName = "RandomLastName",
                    MiddleName = "RandomMiddleName"
                };

                return By(client, storeInRepository);
            }

            public IOrderWithShopPart In(Shop shop)
            {
                _shop = shop;

                return this;
            }

            public IOrderWithShopPart InDummyShop()
            {
                var shopAddress = new Address
                {
                    AddressLine = "RandomStreet 1",
                    City = "RandomCity",
                    PostalCode = "RandomPostalCode",
                    IsDefault = true
                };

                var shop = new Shop
                {
                    CompanyNumber = "00001",
                    Address = shopAddress,
                    Title = "RandomTitle"
                };

                return In(shop);
            }

            public IOrderWithShopPart WithOrderLine(OrderLine orderLine, int? storedQuantity = null)
            {
                if (storedQuantity != null)
                {
                    StorageItemsRepository.StorageItems.Add(new StorageItem
                    {
                        Id = new Random().Next(),
                        Product = orderLine.Product,
                        Quantity = storedQuantity.Value
                    });
                }

                _orderLines.Add(orderLine);

                return this;
            }

            public IOrderWithShopPart WithOrderLine(Product product, int orderedQuantity, int? storedQuantity = null)
            {
                return WithOrderLine(new OrderLine
                {
                    Product = product,
                    Quantity = orderedQuantity
                }, storedQuantity);
            }

            public IFinishedOrderPath FinishedBy(DateTime finishDate)
            {
                _finishDate = finishDate;

                return this;
            }

            public IFinishedOrderPath OfStatus(OrderStatus status)
            {
                _status = status;

                return this;
            }

            public Order Build()
            {
                var order = new Order
                {
                    Status = _status,
                    CreationDate = _creationDate,
                    FinishDate = _finishDate,
                    Id = _id,
                    Initiator = _client,
                    Notes = string.Empty,
                    Shop = _shop,
                    OrderLines = _orderLines
                };

                if (_hasToHasToStoreInRepository)
                    OrderRepository.Orders.Add(order);

                return order;
            }
        }
    }

    public interface INewOrderPart
    {
        IInitiatedOrderPart InitiatedOn(DateTime date);
    }

    public interface IInitiatedOrderPart
    {
        IOrderWithClientPart By(Client client, bool storeInRepository = true);
        IOrderWithClientPart ByDummyClient(bool storeInRepository = true);
    }

    public interface IOrderWithClientPart
    {
        IOrderWithShopPart In(Shop shop);
        IOrderWithShopPart InDummyShop();
    }

    public interface IOrderWithShopPart : IFinishedOrderPath
    {
        IOrderWithShopPart WithOrderLine(OrderLine orderLine, int? storedQuantity = null);
        IOrderWithShopPart WithOrderLine(Product product, int orderedQuantity, int? storedQuantity = null);

        IFinishedOrderPath FinishedBy(DateTime finishDate);
        IFinishedOrderPath OfStatus(OrderStatus status);
    }

    public interface IFinishedOrderPath
    {
        Order Build();
    }    
}

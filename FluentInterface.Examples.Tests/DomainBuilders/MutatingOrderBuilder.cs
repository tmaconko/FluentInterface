using System;
using System.Collections.Generic;
using FluentInterface.Examples.Domain;

namespace FluentInterface.Examples.Tests.DomainBuilders
{
    public class MutatingOrderBuilder
    {
        static MutatingOrderBuilder()
        {
            OrderRepository = new OrderRepositoryInMemory();
            ClientsRepository = new ClientsRepositoryInMemory();
            StorageItemsRepository = new StorageItemsRepositoryInMemory();
        }

        private MutatingOrderBuilder()
        {
            _orderLines = new List<OrderLine>();
        }

        public static readonly OrderRepositoryInMemory OrderRepository;
        public static readonly ClientsRepositoryInMemory ClientsRepository;
        public static readonly StorageItemsRepositoryInMemory StorageItemsRepository;

        private int _id;
        private DateTime _creationDate;
        private DateTime? _finishDate;
        private Client _client;
        private Shop _shop;
        private OrderStatus _status;

        private readonly List<OrderLine> _orderLines;

        private bool _hasToSaveInMemory;

        public static MutatingOrderBuilder NewOrder(bool storeInRepository = true)
        {
            return new MutatingOrderBuilder
            {
                _id = new Random().Next(),
                _hasToSaveInMemory = storeInRepository
            };
        }

        public MutatingOrderBuilder InitiatedOn(DateTime createdDate)
        {
            _creationDate = createdDate;
            _status = OrderStatus.Initiated;

            return this;
        }

        public MutatingOrderBuilder FinishedBy(DateTime finishDate)
        {
            _finishDate = finishDate;
            _status = OrderStatus.Finished;

            return this;
        }

        public MutatingOrderBuilder By(Client client, bool storeInRepository = true)
        {
            _client = client;

            if (storeInRepository)
            {
                ClientsRepository.Clients.Add(client);
            }

            return this;
        }

        public MutatingOrderBuilder ByDummyClient(bool storeInRepository = true)
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

        public MutatingOrderBuilder In(Shop shop)
        {
            _shop = shop;

            return this;
        }

        public MutatingOrderBuilder InDummyShop()
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

        public MutatingOrderBuilder WithOrderLine(OrderLine orderLine, int? storedQuantity = null)
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

        public MutatingOrderBuilder WithOrderLine(Product product, int orderedQuantity, int? storedQuantity = null)
        {
            return WithOrderLine(new OrderLine
            {
                Product = product,
                Quantity = orderedQuantity
            }, storedQuantity);
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

            if (_hasToSaveInMemory)
            {
                OrderRepository.Orders.Add(order);
            }

            return order;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using FluentInterface.Examples.Domain;
using FluentInterface.Examples.Tests.DomainBuilders;
using NUnit.Framework;

namespace FluentInterface.Examples.Tests
{
    [TestFixture]
    public class OrdersServiceTests
    {
        [Test]
        public void OrdersService_PayOrder_WithInitiatedOrderUsingStandardMethod_StatusChangedToPaid()
        {
            //ARRANGE
            var clientsRepository = new ClientsRepositoryInMemory();
            var ordersRepository = new OrderRepositoryInMemory();
            var storageItemsRepository = new StorageItemsRepositoryInMemory();
            var mailService = new MailService();

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
                Addresses = new List<Address>{ address },
                Balance = 100,
                Emails = new List<Email>{ email },
                FirstName = "RandomFirstName",
                Id = 1,
                LastName = "RandomLastName",
                MiddleName = "RandomMiddleName"
            };

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

            var product = new Product
            {
                Id = 1,
                Price = 10,
                Title = "RandomTitle1",
            };

            var storageItem = new StorageItem
            {
                Id = 1,
                Product = product,
                Quantity = 2
            };

            var orderLine = new OrderLine
            {
                Product = product,
                Quantity = 2
            };

            var order = new Order
            {
                Status = OrderStatus.Initiated,
                CreationDate = DateTime.Now,
                FinishDate = null,
                Id = 1,
                Initiator = client,
                Notes = string.Empty,
                Shop = shop,
                OrderLines = new List<OrderLine> {orderLine}
            };

            ordersRepository.Orders = new List<Order> {order};
            clientsRepository.Clients = new List<Client> {client};
            storageItemsRepository.StorageItems = new List<StorageItem> {storageItem};

            //ACT
            var ordersService = new OrdersService(ordersRepository, storageItemsRepository, clientsRepository, mailService);
            ordersService.PayOrder(order.Id, client.Id);

            //ASSERT
            order = ordersRepository.Orders.First(p => p.Id == order.Id);
            client = clientsRepository.Clients.First(p => p.Id == client.Id);

            Assert.That(order.Status, Is.EqualTo(OrderStatus.Paid));
            Assert.That(client.Balance, Is.EqualTo(80m));
        }

        [Test]
        public void OrdersService_PayOrder_WithInitiatedOrderUsingMutatingMethod_StatusChangedToPaid()
        {
            //ARRANGE
            var mailService = new MailService();

            var order = MutatingOrderBuilder
                .NewOrder()
                .InitiatedOn(DateTime.Now)
                .ByDummyClient()
                .InDummyShop()
                .WithOrderLine(new Product { Id = 1, Price = 10, Title = "asd" }, 2, 2)
                .Build();

            //ACT
            var ordersService = new OrdersService(
                MutatingOrderBuilder.OrderRepository,
                MutatingOrderBuilder.StorageItemsRepository,
                MutatingOrderBuilder.ClientsRepository,
                mailService);

            ordersService.PayOrder(order.Id, order.Initiator.Id);

            //ASSERT
            var storedOrder = MutatingOrderBuilder.OrderRepository.Orders.First(p => p.Id == order.Id);
            var storedClient = MutatingOrderBuilder.ClientsRepository.Clients.First(p => p.Id == storedOrder.Initiator.Id);

            Assert.That(storedOrder.Status, Is.EqualTo(OrderStatus.Paid));
            Assert.That(storedClient.Balance, Is.EqualTo(80m));
        }

        [Test]
        public void OrdersService_PayOrder_WithInitiatedOrderUsingPatchingMethod_StatusChangedToPaid()
        {
            //ARRANGE
            var mailService = new MailService();

            var order = PatchingOrderBuilder
                .CreateOrder()
                .InitiatedOn(DateTime.Now)
                .ByDummyClient()
                .InDummyShop()
                .WithOrderLine(new Product { Id = 1, Price = 10, Title = "asd" }, 2, 2)
                .Build();

            //ACT
            var ordersService = new OrdersService(
                PatchingOrderBuilder.OrderRepository,
                PatchingOrderBuilder.StorageItemsRepository,
                PatchingOrderBuilder.ClientsRepository,
                mailService);

            ordersService.PayOrder(order.Id, order.Initiator.Id);

            //ASSERT
            var storedOrder = PatchingOrderBuilder.OrderRepository.Orders.First(p => p.Id == order.Id);
            var storedClient = PatchingOrderBuilder.ClientsRepository.Clients.First(p => p.Id == storedOrder.Initiator.Id);

            Assert.That(storedOrder.Status, Is.EqualTo(OrderStatus.Paid));
            Assert.That(storedClient.Balance, Is.EqualTo(80m));
        }
    }
}

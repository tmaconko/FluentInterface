using System;
using System.Linq;
using System.Text;

namespace FluentInterface.Examples.Domain
{
    public class OrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IStorageItemsRepository _storageItemsRepository;
        private readonly IClientsRepository _clientsRepository;
        private readonly IMailService _mailService;

        public OrdersService(IOrdersRepository ordersRepository, IStorageItemsRepository storageItemsRepository, IClientsRepository clientsRepository, IMailService mailService)
        {
            _ordersRepository = ordersRepository;
            _storageItemsRepository = storageItemsRepository;
            _clientsRepository = clientsRepository;
            _mailService = mailService;
        }

        public void PayOrder(int orderId, int clientId)
        {
            var order = _ordersRepository.GetById(orderId);

            if (order.Status != OrderStatus.Initiated)
                throw new InvalidOperationException();

            if (order.CreationDate < DateTime.Now.AddDays(-7))
            {
                order.Status = OrderStatus.Cancelled;
                _ordersRepository.Store(order);

                throw new InvalidOperationException();
            }
            
            var client = _clientsRepository.GetById(clientId);

            if (client.Id != order.Initiator.Id)
                throw new InvalidOperationException();

            if (client.Addresses.First(p => p.IsDefault).City != order.Shop.Address.City)
                throw new InvalidOperationException();

            decimal sum = order.OrderLines.Sum(p => p.Product.Price * p.Quantity);
            if (client.Balance < sum)
                throw new InvalidOperationException();

            foreach (var orderLine in order.OrderLines)
            {
                var storageItem = _storageItemsRepository.GetByProductId(orderLine.Product.Id);
                if (storageItem.Quantity < orderLine.Quantity)
                    throw new InvalidOperationException();
            }

            client.Balance -= sum;

            foreach (var orderLine in order.OrderLines)
            {
                var storageItem = _storageItemsRepository.GetByProductId(orderLine.Product.Id);
                storageItem.Quantity -= orderLine.Quantity;

                _storageItemsRepository.Store(storageItem);
            }
            
            var notesBuilder = new StringBuilder();

            notesBuilder.AppendLine(order.Shop.Title);
            notesBuilder.AppendLine(order.Status.ToString());
            foreach (var orderLine in order.OrderLines)
                notesBuilder.AppendLine(
                    $"{orderLine.Product.Title} * {orderLine.Quantity} = {orderLine.Quantity*orderLine.Product.Price}");
            notesBuilder.AppendLine($"{client.FirstName} {client.MiddleName} {client.LastName}");

            order.Notes = notesBuilder.ToString();
            order.Status = OrderStatus.Paid;

            _ordersRepository.Store(order);
            _clientsRepository.Store(client);

            var email = new EmailMessage
            {
                Content = notesBuilder.ToString(),
                Recipient = client.Emails.First(p => p.IsDefault).EmailString,
                Subject = order.Id.ToString()
            };

            _mailService.SendMessage(email);

        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace FluentInterface.Examples.Domain
{
    public class ClientsRepositoryInMemory : IClientsRepository
    {
        public IList<Client> Clients;

        public ClientsRepositoryInMemory()
        {
            Clients = new List<Client>();
        }

        public Client GetById(int id)
        {
            return Clients.First(p => p.Id == id);
        }

        public void Store(Client client)
        {
            var storedClient = Clients.First(p => p.Id == client.Id);
            Clients.Remove(storedClient);
            Clients.Add(storedClient);
        }
    }
}
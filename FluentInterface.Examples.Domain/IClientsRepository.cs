namespace FluentInterface.Examples.Domain
{
    public interface IClientsRepository
    {
        Client GetById(int id);
        void Store(Client client);
    }
}
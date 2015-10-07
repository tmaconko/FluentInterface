namespace FluentInterface.Examples.Domain
{
    public interface IMailService
    {
        void SendMessage(EmailMessage message);
    }
}
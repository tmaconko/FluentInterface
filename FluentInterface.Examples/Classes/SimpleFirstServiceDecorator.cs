namespace FluentInterface.Examples.Classes
{
    internal class SimpleFirstServiceDecorator : IFirstService
    {
        public SimpleFirstServiceDecorator(IFirstService firstService)
        {

        }
    }
}
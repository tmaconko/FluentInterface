namespace FluentInterface.Examples.Classes
{
    internal class ComplexFirstServiceDecorator : IFirstService
    {
        public ComplexFirstServiceDecorator(IFirstService firstService)
        {

        }
    }
}
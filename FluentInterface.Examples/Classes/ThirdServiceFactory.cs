namespace FluentInterface.Examples.Classes
{
    internal class ThirdServiceFactory
    {
        public static IThirdService CreateThirdServiceForSpecificCase()
        {
            return new ThirdService();
        }
    }
}
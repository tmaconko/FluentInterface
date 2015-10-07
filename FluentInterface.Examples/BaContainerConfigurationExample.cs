using BalticAmadeus.Container;
using FluentInterface.Examples.Classes;

namespace FluentInterface.Examples
{
    public static class ContainerConfiguration
    {
        public static ContainerBuilder CreateContainerConfiguration()
        {

            var cfg = new ContainerBuilder();

            cfg.For<IFirstService>()
                .Use<FirstService>()
                .DecorateWith<SimpleFirstServiceDecorator>()
                .DecorateWith<AdvancedFirstServiceDecorator>()
                .DecorateWith<ComplexFirstServiceDecorator>()
                .DecorateWithProxy<LoggingProxy>();

            cfg.For<IThirdService>()
                .Use(ThirdServiceFactory.CreateThirdServiceForSpecificCase)
                .DecorateWithProxy<LoggingProxy>();
            
            cfg.For<IForthService>()
                .Use<ForthService>()
                .WithParameter("NeededData")
                .OnRelease(instance => { instance.DestroyMe(); });
            
            cfg.For<IFifthService>()
                .Use<IFifthService>(() => new FifthService())
                .AsSingleton()
                .DecorateWithProxy<LoggingProxy>();

            return cfg;

        }
    }
}

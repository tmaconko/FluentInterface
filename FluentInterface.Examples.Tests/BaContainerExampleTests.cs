using BalticAmadeus.Container;
using FluentInterface.Examples.Classes;
using NUnit.Framework;

namespace FluentInterface.Examples.Tests
{
    [TestFixture]
    public class ContainerTests
    {
        private IContainer _container;

        [SetUp]
        public void SetUp()
        {
            _container = ContainerConfiguration.CreateContainerConfiguration().Build();
        }

        [Test]
        public void Resolve_WithRegisteredServiceInterface_ReturnsDecoratedService()
        {
            //ARRANGE
            //ACT
            //ASSERT
            Assert.That(_container.Resolve<IFirstService>(), Is.InstanceOf<SimpleFirstServiceDecorator>());
        }


        [Test]
        public void Resolve_WithRegisteredServiceInterface_ReturnsServiceImplementation()
        {
            //ARRANGE
            //ACT
            //ASSERT
            Assert.That(_container.Resolve<IForthService>(), Is.InstanceOf<ForthService>());
        }
    }
}
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace FluentInterface.Examples.Tests
{
    [TestFixture]
    public class CustomerRepositoryTests
    {
        private readonly CustomerRepository _customerRepository = new CustomerRepository();

        [Test]
        public void Store_WithInvalidSocialSecurityNumberFormat_ThrowsException()
        {
            //ARRANGE
            var customer = new Customer
            {
                Accounts = new List<Customer.Account>(),
                BirthDate = DateTime.Now,
                CompanyTitle = "Baltic Amadeus",
                Name = "Vardenis",
                LastName = "Pavardenis",
                SocialSecurityNumber = "20000000000"
            };

            //ACT
            //ASSERT
            Assert.That(() => _customerRepository.Store(customer), Throws.Exception.Message.Contains("Social security number is in bad format!"));
            Assert.That(customer.SocialSecurityNumber, Is.EqualTo("20000000000"));
        }

        [Test]
        public void Store_WithEmptySocialSecurityNumber_ThrowsException()
        {
            //ARRANGE
            var customer = new Customer
            {
                Accounts = new List<Customer.Account>(),
                BirthDate = DateTime.Now,
                CompanyTitle = "Baltic Amadeus",
                Name = "Vardenis",
                LastName = "Pavardenis",
                SocialSecurityNumber = ""
            };

            //ACT
            //ASSERT

            Assert.That(() => _customerRepository.Store(customer), 
                Throws.Exception.InstanceOf<Exception>()
                .And.Message.Contains("Social security number is required to perform action!")
                .And.InnerException.Null);

            var exception = Assert.Throws<Exception>(() => _customerRepository.Store(customer));
            Assert.IsTrue(exception.Message.Contains("Social security number is required to perform action!"));
            Assert.IsNull(exception.InnerException);
            

            Assert.That(customer.SocialSecurityNumber, Is.EqualTo(""));
        }
    }
}
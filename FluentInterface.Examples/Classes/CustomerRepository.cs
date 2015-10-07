using System;
using System.Linq;

namespace FluentInterface.Examples.Classes
{
    public class CustomerRepository
    {
        public void Store(Customer customer)
        {
            var customerValidator = new CustomerValidator();
            var validationResult = customerValidator.Validate(customer);

            if (!validationResult.IsValid)
                throw new Exception(string.Join(" ",validationResult.Errors.Select(p => p.ErrorMessage)));

            //Store into database
        }
    }
}
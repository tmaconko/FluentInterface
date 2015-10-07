using System;
using System.Collections.Generic;

namespace FluentInterface.Examples.Classes
{
    public class Customer
    {
        public string CompanyTitle { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public IEnumerable<Account> Accounts { get; set; }

        public class Account
        {
            public double BalanceDelta { get; set; }
            public double SavedBalance { get; set; }
            public double CurrentBalance { get; set; }
        }
    }
}
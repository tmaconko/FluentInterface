using System.Collections.Generic;

namespace FluentInterface.Examples.Domain
{
    public class Client
    {
        public int Id;
        public string FirstName;
        public string LastName;
        public string MiddleName;
        public IList<Address> Addresses;
        public IList<Email> Emails;
        public decimal Balance;
    }
}
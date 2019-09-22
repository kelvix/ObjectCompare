using System.Collections.Generic;

namespace ObjectCompare.Test.Model
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Phone Phone { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
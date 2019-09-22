using System;
using System.Collections.Generic;

namespace ObjectCompare.Test
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Phone Phone { get; set; }
        public List<Address> Addresses { get; set; }
    }

    public class Phone
    {
        public string PhoneType { get; set; }
        public string Number { get; set; }
        public string Extension { get; set; }
    }
}
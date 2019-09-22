using System.Collections.Generic;
using NUnit.Framework;

namespace ObjectCompare.Test
{
    public class ObjectComparatorTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Matches_BothNull_Match()
        {
            Assert.True(Comparator.Matches(null, null));
        }

        [Test]
        public void Matches_OneNullOneNotNull_DoNotMatch()
        {
            Assert.False(Comparator.Matches("", null));
        }

        [Test]
        public void Matches_WithEqualSimpleTypes_Match()
        {
            var firstPerson = new Person
            {
                FirstName = "Demo",
                LastName = "Last"
            };
            
            var secondPerson = new Person
            {
                FirstName = "Demo",
                LastName = "Last"
            };
            
            Assert.True(Comparator.Matches(firstPerson, secondPerson));
        }

        [Test]
        public void Matches_WithComplexTypes_Match()
        {
            var firstPerson = new Person
            {
                FirstName = "Demo",
                LastName = "Last",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        AddressLine1 = "123 Michael St",
                        City = "Kansas City",
                        State = "Missouri",
                        ZipCode = "64083"
                    }
                }
            };
            
            var secondPerson = new Person
            {
                FirstName = "Demo",
                LastName = "Last",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        AddressLine1 = "123 Michael St",
                        City = "Kansas City",
                        State = "Missouri",
                        ZipCode = "64083"
                    }
                }
            };
            
            Assert.True(Comparator.Matches(firstPerson, secondPerson));
        }
        
        [Test]
        public void Matches_WithComplexTypes_DoNotMatch()
        {
            var firstPerson = new Person
            {
                FirstName = "Demo",
                LastName = "Last",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        AddressLine1 = "123 Michael St",
                        City = "Kansas City",
                        State = "Missouri",
                        ZipCode = "64083"
                    }
                }
            };
            
            var secondPerson = new Person
            {
                FirstName = "Demo",
                LastName = "Last",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        AddressLine1 = "179 Michael St",
                        City = "Kansas City",
                        State = "Missouri",
                        ZipCode = "64083"
                    }
                }
            };
            
            Assert.False(Comparator.Matches(firstPerson, secondPerson));
        }
    }
}
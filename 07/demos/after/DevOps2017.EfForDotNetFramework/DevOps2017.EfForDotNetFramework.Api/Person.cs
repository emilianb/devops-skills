using System;
using System.Collections.Generic;
using System.Linq;

namespace DevOps2017.EfForDotNetFramework.Api
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<Phone> PhoneNumbers { get; set; }
        public string Status { get; set; }
    }
}

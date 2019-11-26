using System;
using System.Collections.Generic;

namespace DevOps2017.EfForDotNetCore.Api
{
        public class Phone
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string PhoneNumber { get; set; }
    }
}

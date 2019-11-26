using System;
using System.Linq;

namespace DevOps2017.EfForDotNetFramework.Api
{
    public class Phone
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string PhoneNumber { get; set; }
    }
}

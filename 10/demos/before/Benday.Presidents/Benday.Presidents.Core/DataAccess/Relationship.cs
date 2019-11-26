using Benday.Presidents.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Benday.Presidents.Core.DataAccess
{
    public class Relationship : Int32Identity
    {

        public Relationship()
        {

        }

        public int FromPersonId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("FromPersonId")]
        public Person FromPerson { get; set; }

        public int ToPersonId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ToPersonId")]
        public Person ToPerson { get; set; }

        public string RelationshipType { get; set; }
    }
}

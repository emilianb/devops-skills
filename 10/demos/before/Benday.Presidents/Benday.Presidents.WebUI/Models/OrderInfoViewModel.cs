using Benday.Presidents.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Benday.Presidents.WebUI.Models
{
    public class OrderInfoViewModel : Int32Identity
    {
        public string ShipToName { get; set; }
        public string ShipToAddress { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToState { get; set; }
        public string ShipToPostalCode { get; set; }

        public bool IsInternalOrderInformationVisible { get; set; }
        public string InternalOrderNumber { get; set; }
        public string ApprovedBy { get; set; }
        public double ProfitAmount { get; set; }
    }
}
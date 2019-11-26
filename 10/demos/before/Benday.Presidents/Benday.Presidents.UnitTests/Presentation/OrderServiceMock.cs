using System;
using Benday.Presidents.Core.Services;
using Benday.Presidents.Core.Models;

namespace Benday.Presidents.UnitTests.Presentation
{
    public class OrderServiceMock : IOrderService
    {

        public OrderServiceMock()
        {

        }

        public Order GetByIdReturnValue { get; set; }

        public Order GetById(int orderId)
        {
            return GetByIdReturnValue;
        }
    }
}

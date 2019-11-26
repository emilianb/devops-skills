using Benday.Presidents.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benday.Presidents.Core.Services
{
    public interface IOrderService
    {
        Order GetById(int orderId);
    }
}

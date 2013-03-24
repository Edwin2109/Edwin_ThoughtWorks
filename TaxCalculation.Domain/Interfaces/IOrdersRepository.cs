using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxCalculation.Domain.Interfaces
{
    public interface IOrdersRepository
    {
        List<Order> ReadOrders(string textFilePath);
    }
}

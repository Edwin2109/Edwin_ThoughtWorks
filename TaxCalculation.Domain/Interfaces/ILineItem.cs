using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxCalculation.Domain.Interfaces
{
    public interface ILineItem
    {
        string Name { get; }
        decimal GetPrice();
        int GetQuantity();
        decimal GetLineTax();
        decimal GetTotal();
    }
}

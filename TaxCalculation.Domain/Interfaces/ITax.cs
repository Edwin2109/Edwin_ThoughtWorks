using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxCalculation.Domain.Interfaces
{
    public interface ITax
    {
        IRounding Rounding { get; }
        decimal Rate { get; }
        decimal CalculateTax(decimal itemPrice);
    }
}

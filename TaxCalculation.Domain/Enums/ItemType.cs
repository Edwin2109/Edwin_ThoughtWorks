using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxCalculation.Domain.Enums
{
    [Flags] 
    public enum ItemType
    {
        Exempt = 0,
        Basic = 1,
        Import = 2
    }
}

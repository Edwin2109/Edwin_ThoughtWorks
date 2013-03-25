using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxCalculation.Domain.Enums;
using TaxCalculation.Domain.Interfaces;

namespace TaxCalculation.Domain.Factories
{
    /// <summary>
    /// Factory class to build a based on the input parameters
    /// </summary>
    public static class LineItemFactory
    {
        private static readonly Rounding ROUNDING = new Rounding(0.05M);
        private static readonly ITax BASICTAX = new Tax(0.1M, ROUNDING);
        private static readonly ITax IMPORTTAX = new Tax(0.05M, ROUNDING);
        private static readonly ITax EXEMPTTAX = new Tax(0.0M, ROUNDING);

        private static readonly Dictionary<ItemType, ITax> itemTaxLookup = new Dictionary<ItemType, ITax>()
        {
            { ItemType.Basic, BASICTAX },
            { ItemType.Import, IMPORTTAX },
            { ItemType.Exempt, EXEMPTTAX }
        };

        public static ILineItem GetLineItem(string name, decimal price, int quantity, ItemType itemType)
        {
            ILineItem item = new LineItem(name, price, quantity);

            foreach (int flag in Enum.GetValues(typeof(ItemType)))
            {
                if ((flag & (int)itemType) == flag)
                {
                    item = (ILineItem)Activator.CreateInstance(typeof(LineItemTaxDecorator), new object[] { item, itemTaxLookup[(ItemType)flag] });
                }
            }
            
            return item;
        }

        public static ILineItem GetLineItem(string name, decimal price, int quantity)
        {
            return new LineItem(name, price, quantity);
        }
    }
}

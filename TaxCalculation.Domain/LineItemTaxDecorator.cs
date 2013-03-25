using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxCalculation.Domain.Interfaces;

namespace TaxCalculation.Domain
{
    /// <summary>
    /// Decorator class to extend the LineItem functionality
    /// </summary>
    public class LineItemTaxDecorator: ILineItem
    {
        protected ILineItem _decoratedLineItem;
        protected ITax _lineTax;

        public LineItemTaxDecorator(ILineItem lineItem, ITax lineTax)
        {
            this._decoratedLineItem = lineItem;
            this._lineTax = lineTax;
        }

        #region ILineItem Members

        public string Name
        {
            get { return _decoratedLineItem.Name; }
        }

        public virtual decimal GetLineTax()
        {
            return this._decoratedLineItem.GetLineTax() + _lineTax.CalculateTax(this._decoratedLineItem.GetPrice());
        }

        public virtual decimal GetPrice()
        {
            return this._decoratedLineItem.GetPrice();
        }

        public virtual decimal GetTotal()
        {
            return this.GetPrice() + this.GetLineTax();
        }

        public int GetQuantity()
        {
            return this._decoratedLineItem.GetQuantity();
        }

        #endregion
    }
}

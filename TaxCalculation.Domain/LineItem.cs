using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxCalculation.Domain.Interfaces;

namespace TaxCalculation.Domain
{
    public class LineItem : ILineItem
    {
        private string _name;
        private decimal _price;
        private int _quantity;

        public LineItem(string name, decimal price, int quantity)
        {
            #region Parameter Checking
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("name");
            if(price < 0)
                throw new ArgumentException("price");
            #endregion

            this._name = name;
            this._price = price;
            this._quantity = quantity;

        }

        #region ILineItem Members

        public string Name
        {
            get { return this._name; }
        }

        public decimal GetPrice()
        {
            return this._price;
        }

        public int GetQuantity()
        {
            return this._quantity;
        }

        public decimal GetLineTax()
        {
            return 0.0M;
        }

        public decimal GetTotal()
        {
            return this.GetPrice() + this.GetLineTax();
        }

        #endregion
    }
}

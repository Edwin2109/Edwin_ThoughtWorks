using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using TaxCalculation.Domain.Interfaces;

namespace TaxCalculation.Domain
{
    public class Order
    {   
        private Collection<ILineItem> _items = new Collection<ILineItem>();
        public string OrderNumber { get; set; }
        public List<ILineItem> LineItems { get { return _items.ToList(); } }
        public Order()
        {

        }

        public void AddItem(ILineItem item)
        {
            this._items.Add(item);
        }

        public decimal GetOrderPrice()
        {
            decimal total = 0.0M;
            foreach (var item in this._items)
            {
                total += item.GetPrice();
            }
            return total;
        }

        public decimal GetOrderLineTax()
        {
            decimal total = 0.0M;
            foreach (var item in this._items)
            {
                total += item.GetLineTax();
            }
            return total;
        }

        public decimal GetOrderTotal()
        {
            return this.GetOrderPrice() + this.GetOrderLineTax();
        }
    }
}

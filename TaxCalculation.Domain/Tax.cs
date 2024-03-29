﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxCalculation.Domain.Interfaces;

namespace TaxCalculation.Domain
{
    public class Tax : ITax
    {
        private decimal _rate;
        private IRounding _rounding;

        public Tax(decimal rate, IRounding rounding)
        {
            this._rate = rate;
            this._rounding = rounding;
        }

        #region ILineTax Members

        public decimal Rate
        {
            get { return this._rate; }
        }

        public IRounding Rounding
        {
            get { return this._rounding; }
        }

        public virtual decimal CalculateTax(decimal itemPrice)
        {
            decimal tempTax = itemPrice * this.Rate;
            decimal roundedTax = Rounding.Round(tempTax);
            return tempTax;
        }

        #endregion
    }
}

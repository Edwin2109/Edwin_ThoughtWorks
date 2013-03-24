using System;
namespace TaxCalculation.Domain.Interfaces
{
    public interface IRounding
    {
        decimal Round(decimal valToRound);
    }
}

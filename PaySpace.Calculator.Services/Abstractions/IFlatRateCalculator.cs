using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Abstractions
{
    public interface IFlatRateCalculator
    {
        public Task<CalculateResult> CalculateFlatRateTax(decimal income);
    }
}
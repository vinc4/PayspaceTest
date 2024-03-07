using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class FlatRateCalculator : IFlatRateCalculator
    {
        private readonly CalculatorContext _context;
        public FlatRateCalculator(CalculatorContext context)
        {
            _context = context;
        }

        public Task<CalculateResult> CalculateFlatRateTax(decimal income)
        {
            var setting = _context.CalculatorSetting
                               .FirstOrDefault(c => c.Calculator == CalculatorType.FlatRate);

            // Calculate tax based on the setting rate
            decimal tax = setting != null ? (income * setting.Rate/100) : 0;

            // Create a CalculateResult object with the calculated tax and return it directly
            return Task.FromResult(new CalculateResult { Tax = tax , Calculator = CalculatorType.FlatRate });
        }
    }
}
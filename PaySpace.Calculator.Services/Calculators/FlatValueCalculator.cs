using Microsoft.EntityFrameworkCore;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class FlatValueCalculator : IFlatValueCalculator
    {
        private readonly CalculatorContext _context;
        public FlatValueCalculator(CalculatorContext context)
        {
            _context = context;
        }
        public async Task<CalculateResult> CalculateFlatValueTax(decimal income)
        {
            // Retrieve the flat value tax settings from the database
            var settings = await _context.CalculatorSetting
                                .Where(c => c.Calculator == CalculatorType.FlatValue)
                                .ToListAsync();

            // Find the appropriate setting based on the income
            var setting = settings.FirstOrDefault(s => income >= s.From && (s.To == null || income < s.To));

            if (setting != null)
            {
                decimal tax;
                if (setting.RateType == RateType.Percentage)
                {
                    // Calculate tax based on percentage rate
                    tax = income * (setting.Rate / 100); // Convert percentage to decimal
                }
                else
                {
                    // Apply flat value tax
                    tax = setting.Rate;
                }

                // Create a CalculateResult object with the calculated tax
                CalculateResult result = new CalculateResult { Tax = tax, Calculator = CalculatorType.FlatValue };
                return result;
            }
            else
            {
                // If the setting is not found, return a default CalculateResult with zero tax
                CalculateResult result = new CalculateResult { Tax = 0, Calculator = CalculatorType.FlatValue };
                return result;
            }
        }

    }
}
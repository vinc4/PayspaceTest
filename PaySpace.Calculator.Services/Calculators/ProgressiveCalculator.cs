using Microsoft.EntityFrameworkCore;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class ProgressiveCalculator : IProgressiveCalculator
    {
        private readonly CalculatorContext _context;
        public ProgressiveCalculator(CalculatorContext context)
        {
            _context = context;
        }

        public async Task<CalculateResult> CalculateAsync(decimal income)
        {
            decimal tax = 0;
            var brackets = await _context.CalculatorSetting
                .Where(c => c.Calculator == CalculatorType.Progressive)
                .ToListAsync(); // Materialize the query to a list

            brackets = brackets.OrderBy(c => c.From).ToList(); // Order the list on the client side

            foreach (var bracket in brackets)
            {
                if (income <= bracket.To)
                {
                    // If income falls within the bracket, calculate tax based on the income
                    tax += income * bracket.Rate / 100; // Convert percentage to decimal
                    break; // Exit the loop since the income falls within this bracket
                }
                else
                {
                    // If income exceeds the bracket, calculate tax based on the taxable amount within the bracket
                    decimal taxableAmount = Math.Min(bracket.To ?? income, income) - bracket.From;
                    tax += taxableAmount * bracket.Rate / 100; // Convert percentage to decimal
                }
            }

            // Create a CalculateResult object with the calculated tax
            CalculateResult result = new CalculateResult { Tax = tax, Calculator = CalculatorType.Progressive };

            return result;
        }
    }
}
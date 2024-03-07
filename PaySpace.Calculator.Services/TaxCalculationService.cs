using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Calculators;
using PaySpace.Calculator.Services.Exceptions;
using PaySpace.Calculator.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculator.Services
{
    public class TaxCalculationService: ITaxCalculationService
    {
        private readonly IProgressiveCalculator _progressiveCalculator;
        private readonly IFlatValueCalculator _flatValueCalculator;
        private readonly IFlatRateCalculator _flatRateCalculator;
        private readonly IPostalCodeService _postalCodeService;

        public TaxCalculationService(
         IProgressiveCalculator progressiveCalculator,
         IFlatValueCalculator flatValueCalculator,
         IFlatRateCalculator flatRateCalculator,
         IPostalCodeService postalCodeService)
        {
            _progressiveCalculator = progressiveCalculator;
            _flatValueCalculator = flatValueCalculator;
            _flatRateCalculator = flatRateCalculator;
            _postalCodeService = postalCodeService;
            _postalCodeService = postalCodeService;
        }

        public async Task<CalculateResult> CalculateTaxAsync(decimal income, string postalCode)
        {
            var calculatorType = await _postalCodeService.CalculatorTypeAsync(postalCode);

            if (calculatorType == null)
            {
                throw new CalculatorException();
            }

             switch (calculatorType)
            {
                case CalculatorType.Progressive:
                    return await _progressiveCalculator.CalculateAsync(income);
                case CalculatorType.FlatValue:
                    return await _flatValueCalculator.CalculateFlatValueTax(income);
                case CalculatorType.FlatRate:
                    return await _flatRateCalculator.CalculateFlatRateTax(income);
                default:
                    throw  new CalculatorException();
            }

        }

    }
}

using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculator.Services.Abstractions
{
    public interface ITaxCalculationService
    {
        Task<CalculateResult> CalculateTaxAsync(decimal income, string postalCode);
    }
}

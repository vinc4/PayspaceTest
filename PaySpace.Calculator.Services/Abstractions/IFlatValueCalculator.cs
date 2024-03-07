﻿using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Abstractions
{
    public interface IFlatValueCalculator
    {
        public Task<CalculateResult> CalculateFlatValueTax(decimal income);
    }
}
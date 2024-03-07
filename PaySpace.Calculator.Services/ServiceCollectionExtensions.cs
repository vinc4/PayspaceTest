﻿using Microsoft.Extensions.DependencyInjection;

using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Calculators;

namespace PaySpace.Calculator.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCalculatorServices(this IServiceCollection services)
        {
            services.AddScoped<IPostalCodeService, PostalCodeService>();
            services.AddScoped<IHistoryService, HistoryService>();
            services.AddScoped<ICalculatorSettingsService, CalculatorSettingsService>();
            services.AddScoped<ITaxCalculationService, TaxCalculationService>();

            services.AddScoped<IFlatRateCalculator, FlatRateCalculator>();
            services.AddScoped<IFlatValueCalculator, FlatValueCalculator>();
            services.AddScoped<IProgressiveCalculator, ProgressiveCalculator>();
            services.AddScoped<IProgressiveCalculator, ProgressiveCalculator>();


            services.AddMemoryCache();
        }
    }
}

using IncomeTaxCalculator.TaxRatesRepository.Contracts;
using Microsoft.Extensions.Logging;

namespace IncomeTaxCalculator.Calculation
{
    /// <inheritdoc/>
    public class IncomeTaxCalculationStrategy : IIncomeTaxCalculationStrategy
    {
        private readonly ILogger<IIncomeTaxCalculationStrategy> _logger;
        private readonly ITaxRatesRepository _taxRatesRepository;

        public IncomeTaxCalculationStrategy(ILogger<IIncomeTaxCalculationStrategy> logger, ITaxRatesRepository taxRatesRepository)
        {
            _logger = logger;
            _taxRatesRepository = taxRatesRepository;
        }

        /// <inheritdoc/>
        public IncomeTax? CalculateIncomeTax(double salary, int year)
        {
            double? taxRate = GetTaxRate(salary, year);
            if (taxRate is null)
            {
                _logger.LogError($"Tax rate not found for salary {salary} in the year of {year}");
                return null;
            }

            var incomeTax = new IncomeTax(year, salary);
            incomeTax.CalculateTotalIncomeTax(taxRate.Value);
            return incomeTax;
        }

        /// <summary>
        /// Get the tax rate for given salary and year from a <see cref="ITaxRatesRepository"/>.
        /// </summary>
        /// <param name="salary">The salary.</param>
        /// <param name="year">The year.</param>
        /// <returns>The tax rate the salary lands on the given year or <c>null</c> if no rate was found.</returns>
        private double? GetTaxRate(double salary, int year)
        {
            var taxRates = _taxRatesRepository.GetTaxRates();
            foreach (var taxRate in taxRates)
            {
                if (taxRate.Year != year)
                {
                    continue;
                }

                if ((taxRate.RangeEnd is null && taxRate.RangeStart < salary) ||
                    (taxRate.RangeEnd is not null && taxRate.RangeStart <= salary && taxRate.RangeEnd > salary))
                {
                    return taxRate.Rate;
                }
            }
            return null;
        }
    }
}

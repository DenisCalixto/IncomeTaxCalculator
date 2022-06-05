using IncomeTaxCalculator.Models;
using IncomeTaxCalculator.TaxRatesProviderContracts;

namespace IncomeTaxCalculator.Calculation
{
    public class IncomeTaxCalculationStrategy : IIncomeTaxCalculationStrategy
    {
        private readonly ILogger<IncomeTaxCalculationStrategy> _logger;
        private readonly ITaxRatesRepository _taxRatesRepository;

        public IncomeTaxCalculationStrategy(ILogger<IncomeTaxCalculationStrategy> logger, ITaxRatesRepository taxRatesRepository)
        {
            _logger = logger;
            _taxRatesRepository = taxRatesRepository;
        }

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
        /// 
        /// </summary>
        /// <param name="salary">The salary.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public double? GetTaxRate(double salary, int year)
        {
            var taxRates = _taxRatesRepository.GetTaxRates();
            foreach (var taxRate in taxRates)
            {
                if (taxRate.Year != year)
                {
                    continue;
                }

                if ((taxRate.RangeStart is null && taxRate.RangeEnd <= salary) ||
                    (taxRate.RangeEnd is null && taxRate.RangeStart > salary) ||
                    (taxRate.RangeStart is not null && taxRate.RangeEnd is not null))
                {
                    return taxRate.Rate;
                }
            }
            return null;
        }
    }
}

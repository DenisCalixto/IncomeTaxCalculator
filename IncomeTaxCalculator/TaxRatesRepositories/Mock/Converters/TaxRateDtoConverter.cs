using IncomeTaxCalculator.TaxRatesRepositories.Mock.DTOs;
using IncomeTaxCalculator.TaxRatesRepositoryContracts.DTOs;

namespace IncomeTaxCalculator.TaxRatesRepositories.Mock.Converters
{
    public class TaxRateDtoConverter
    {
        public static GenericTaxRateDto Convert(TaxRateDto taxRate)
        {
            return new GenericTaxRateDto { RangeStart = taxRate.Start, RangeEnd = taxRate.End, Year = taxRate.Year, Rate = taxRate.Rate };
        }
    }
}

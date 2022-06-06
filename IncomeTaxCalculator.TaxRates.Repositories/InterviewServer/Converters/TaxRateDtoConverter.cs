using IncomeTaxCalculator.TaxRatesRepositories.InterviewServer.DTOs;
using IncomeTaxCalculator.TaxRatesRepository.Contracts.DTOs;

namespace IncomeTaxCalculator.TaxRatesRepositories.InterviewServer.Converters
{
    public class TaxRateDtoConverter
    {
        public static GenericTaxRateDto Convert(TaxRateDto taxRate)
        {
            return new GenericTaxRateDto() { RangeStart = taxRate.Min, RangeEnd = taxRate.Max, Rate = taxRate.Rate };
        }
    }
}

using IncomeTaxCalculator.TaxRatesRepositories.InterviewServer.DTOs;
using IncomeTaxCalculator.TaxRatesRepository.Contracts.DTOs;

namespace IncomeTaxCalculator.TaxRatesRepositories.InterviewServer.Converters
{
    public class TaxBracketDtoConverter
    {
        public static GenericTaxBracketDto Convert(TaxBracketDto taxRate)
        {
            return new GenericTaxBracketDto() { RangeStart = taxRate.Min, RangeEnd = taxRate.Max, Rate = taxRate.Rate };
        }
    }
}

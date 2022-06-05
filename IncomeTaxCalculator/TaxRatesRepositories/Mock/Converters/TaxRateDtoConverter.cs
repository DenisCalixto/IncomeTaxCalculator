using IncomeTaxCalculator.TaxRatesRepositoryContracts.DTOs;

namespace IncomeTaxCalculator.TaxRatesRepositories.Mock.Converters
{
    public class TaxRateDtoConverter
    {
        public static GenericTaxRateDto Convert(TaxRateDtoConverter taxRateDto)
        {
            return new GenericTaxRateDto();
        }
    }
}

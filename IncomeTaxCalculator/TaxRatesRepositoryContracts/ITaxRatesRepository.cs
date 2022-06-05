using IncomeTaxCalculator.TaxRatesRepositoryContracts.DTOs;

namespace IncomeTaxCalculator.TaxRatesProviderContracts
{
    public interface ITaxRatesRepository
    {
        public IEnumerable<GenericTaxRateDto> GetTaxRates();
    }
}

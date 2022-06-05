using IncomeTaxCalculator.TaxRatesProviderContracts;
using IncomeTaxCalculator.TaxRatesRepositoryContracts.DTOs;

namespace IncomeTaxCalculator.TaxRatesRepositories.Mock
{
    public class MockTaxRatesRepository : ITaxRatesRepository
    {
        public IEnumerable<GenericTaxRateDto> GetTaxRates()
        {
            throw new NotImplementedException();
        }
    }
}

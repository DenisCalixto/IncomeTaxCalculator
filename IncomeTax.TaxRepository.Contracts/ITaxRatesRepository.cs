using IncomeTaxCalculator.TaxRatesRepository.Contracts.DTOs;

namespace IncomeTaxCalculator.TaxRatesRepository.Contracts
{
    public interface ITaxRatesRepository
    {
        public IEnumerable<GenericTaxRateDto> GetTaxRates();
    }
}

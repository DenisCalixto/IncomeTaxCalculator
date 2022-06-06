using IncomeTaxCalculator.TaxRatesRepository.Contracts.DTOs;

namespace IncomeTaxCalculator.TaxRatesRepository.Contracts
{
    public interface ITaxRatesRepository
    {
        public IEnumerable<GenericTaxBracketDto> GetTaxRates(int year);
    }
}

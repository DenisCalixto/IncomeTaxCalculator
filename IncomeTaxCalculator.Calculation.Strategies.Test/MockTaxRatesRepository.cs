using IncomeTaxCalculator.TaxRatesRepository.Contracts;
using IncomeTaxCalculator.TaxRatesRepository.Contracts.DTOs;
using Microsoft.Extensions.Logging;

public class MockTaxRatesRepository : ITaxRatesRepository
{
    private readonly ILogger<ITaxRatesRepository> _logger;

    public MockTaxRatesRepository(ILogger<ITaxRatesRepository> logger)
    {
        _logger = logger;
    }

    public IEnumerable<GenericTaxBracketDto> GetTaxRates(int year)
    {
        var range1_1 = CreateExternalTaxRateDto(2019, 0, 49020, 15);
        var range1_2 = CreateExternalTaxRateDto(2019, 49020, 211511, 29);
        var range1_3 = CreateExternalTaxRateDto(2019, 211511, null, 33);
        var range2_1 = CreateExternalTaxRateDto(2020, 0, 49021, 20);
        var range2_2 = CreateExternalTaxRateDto(2020, 49021, 211515, 34);
        var range2_3 = CreateExternalTaxRateDto(2020, 211511, null, 37);

        return new GenericTaxBracketDto[] {
                range1_1,
                range1_2,
                range1_3,
                range2_1,
                range2_2,
                range2_3,
            }.Where(taxRate => taxRate.Year ==  year);

        static GenericTaxBracketDto CreateExternalTaxRateDto(int year, double start, double? end, double rate)
        {
            var obj = new GenericTaxBracketDto();
            obj.Year = year;
            obj.RangeStart = start;
            obj.RangeEnd = end;
            obj.Rate = rate;
            return obj;
        }
    }
}

using IncomeTaxCalculator.TaxRatesRepository.Contracts;
using IncomeTaxCalculator.TaxRatesRepositories.Mock.DTOs;
using IncomeTaxCalculator.TaxRatesRepository.Contracts.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IncomeTaxCalculator.TaxRatesRepositories.Mock
{
    public class MockTaxRatesRepository : ITaxRatesRepository
    {
        private readonly ILogger<MockTaxRatesRepository> _logger;

        public MockTaxRatesRepository(ILogger<MockTaxRatesRepository> logger)
        {
            _logger = logger;
        }

        public IEnumerable<GenericTaxRateDto> GetTaxRates()
        {
            try
            {
                var providerTaxRates = GetTaxRatesFromProvider();
                return providerTaxRates.Select(taxRate => TaxRatesRepositories.Mock.Converters.TaxRateDtoConverter.Convert(taxRate));
            }
            catch (Exception)
            {
                _logger.LogCritical($"Error trying to fetch data for {this.GetType().Name}", DateTime.UtcNow.ToLongTimeString());
                throw;
            }
        }

        //async Task<object> ITaxRatesProvider.GetTaxRatesFromProvider()
        private TaxRateDto[] GetTaxRatesFromProvider()
        {
            var range1 = CreateExternalTaxRateDto(2019, 0, 49020, 15);
            var range2 = CreateExternalTaxRateDto(2019, 49020, 98040, 20.5);
            var range3 = CreateExternalTaxRateDto(2019, 98040, 151978, 26);
            var range4 = CreateExternalTaxRateDto(2019, 151978, 211511, 29);
            var range5 = CreateExternalTaxRateDto(2019, 211511, null, 33);
            var range11 = CreateExternalTaxRateDto(2020, 0, 49021, 16);
            var range12 = CreateExternalTaxRateDto(2020, 49021, 98041, 21.5);
            var range13 = CreateExternalTaxRateDto(2020, 98041, 151980, 27);
            var range14 = CreateExternalTaxRateDto(2020, 151980, 211515, 30);
            var range15 = CreateExternalTaxRateDto(2020, 211511, null, 35);

            return new TaxRateDto[] {
                range1,
                range2,
                range3,
                range4,
                range5,
                range11,
                range12,
                range13,
                range14,
                range15,
            };

            static TaxRateDto CreateExternalTaxRateDto(int year, double? start, double? end, double rate)
            {
                dynamic obj = new JObject();
                obj.Year = year;
                obj.Start = start;
                obj.End = end;
                obj.Rate = rate;
                return System.Text.Json.JsonSerializer.Deserialize<TaxRateDto>(JsonConvert.SerializeObject(obj));
            }
        }
    }
}

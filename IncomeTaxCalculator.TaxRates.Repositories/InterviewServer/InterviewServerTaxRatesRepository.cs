using IncomeTaxCalculator.TaxRatesRepository.Contracts;
using IncomeTaxCalculator.TaxRatesRepository.Contracts.DTOs;
using IncomeTaxCalculator.TaxRatesRepositories.InterviewServer.Converters;
using IncomeTaxCalculator.TaxRatesRepositories.InterviewServer.DTOs;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Nodes;

namespace IncomeTaxCalculator.TaxRates.Repositories.InterviewServer
{
    public class InterviewServerTaxRatesRepository : ITaxRatesRepository
    {
        readonly ILogger<ITaxRatesRepository> _logger;
        readonly IConfiguration Configuration;

        /// <summary>
        /// Memcache of tax brackets.
        /// </summary>
        static IDictionary<int, GenericTaxBracketDto[]> TaxBracketsLookup;

        public InterviewServerTaxRatesRepository(ILogger<ITaxRatesRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
            if (TaxBracketsLookup is null)
            {
                TaxBracketsLookup = new Dictionary<int, GenericTaxBracketDto[]>(0);
            }
        }

        public IEnumerable<GenericTaxBracketDto> GetTaxRates(int year)
        {
            try
            {
                if (!TaxBracketsLookup.ContainsKey(year))
                {
                    var taxRates = GetTaxRatesFromProvider(year);
                    var genericTaxRates = taxRates.Select(taxRate => TaxBracketDtoConverter.Convert(taxRate));
                    TaxBracketsLookup.Add(year, genericTaxRates.ToArray<GenericTaxBracketDto>());
                }
                return TaxBracketsLookup[year];
            }
            catch (Exception)
            {
                throw;
            }
        }

        private TaxBracketDto[] GetTaxRatesFromProvider(int year)
        {
            HttpClient client = new HttpClient();
            try
            {
                var taxServiceUrl = Configuration["TaxRateServiceUrl"];
                client.BaseAddress = new Uri(taxServiceUrl + year);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("").Result;
                if (response.IsSuccessStatusCode)
                {
                    var taxBracketsDto = response.Content.ReadAsAsync<TaxBracketsDto>()?.Result;
                    return taxBracketsDto is not null ? taxBracketsDto.Value.Tax_Brackets : Array.Empty<TaxBracketDto>();
                }

                if (response.StatusCode >=  System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new HttpRequestException($"Error trying to fetch tax brackets from {taxServiceUrl} for the year {year}. Please try again later.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return Array.Empty<TaxBracketDto>();
                }
                else
                {
                    throw new Exception($"Error building tax brackets list after fetching for the year {year}. Please contact administrator.");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                throw;
            }
            finally
            {
                client.Dispose();
            }

            return Array.Empty<TaxBracketDto>();
        }
    }
}

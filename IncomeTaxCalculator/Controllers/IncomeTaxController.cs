using IncomeTaxCalculator.Calculation;
using IncomeTaxCalculator.TaxRatesRepository.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IncomeTaxCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeTaxController : ControllerBase
    {
        private readonly ILogger<IncomeTaxController> _logger;
        private readonly IIncomeTaxCalculationStrategy _incomeTaxCalculationStrategy;
        private readonly ITaxRatesRepository _taxRatesRepository;

        public IncomeTaxController(ILogger<IncomeTaxController> logger, IIncomeTaxCalculationStrategy incomeTaxCalculationStrategy, ITaxRatesRepository taxRatesRepository)
        {
            _logger = logger;
            _incomeTaxCalculationStrategy = incomeTaxCalculationStrategy;
            _taxRatesRepository = taxRatesRepository;
        }

        // POST api/<IncomeTaxController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(double))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Calculate(double salary, int year)
        {
            if (Double.IsNaN(salary) || Single.IsNaN(year))
            {
                return BadRequest();
            }
            try
            {
                var calculatedTax = _incomeTaxCalculationStrategy.CalculateIncomeTax(salary, year);
                if (calculatedTax?.TotalIncomeTax is null)
                {
                    return NotFound();
                }
                return Ok(calculatedTax?.TotalIncomeTax);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

using IncomeTaxCalculator.Calculation;
using IncomeTaxCalculator.DTOs;
using IncomeTaxCalculator.TaxRatesRepository.Contracts;
using IncomeTaxCalculator.ObjectModel;
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
        public async Task<IActionResult> Calculate([FromBody] IncomeTaxDto incomeTaxDto)
        {
            if (Double.IsNaN(incomeTaxDto.Salary) || Single.IsNaN(incomeTaxDto.Year))
            {
                return BadRequest();
            }
            try
            {
                var calculationTask = new Task<IncomeTax?>(() => _incomeTaxCalculationStrategy.CalculateIncomeTax(incomeTaxDto.Salary, incomeTaxDto.Year));
                calculationTask.Start();
                var calculatedTax = await calculationTask;

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

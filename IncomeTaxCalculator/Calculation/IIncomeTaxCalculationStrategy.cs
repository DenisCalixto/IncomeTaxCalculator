using IncomeTaxCalculator.Models;

namespace IncomeTaxCalculator.Calculation
{
    public interface IIncomeTaxCalculationStrategy
    {
        public IncomeTax? CalculateIncomeTax(double salary, int year);
    }
}

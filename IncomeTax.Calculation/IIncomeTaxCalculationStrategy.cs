namespace IncomeTaxCalculator.Calculation
{
    /// <summary>
    /// Strategy for calculating an income tax.
    /// </summary>
    public interface IIncomeTaxCalculationStrategy
    {
        /// <summary>
        /// Calculates the income tax for given salary and year.
        /// </summary>
        /// <param name="annualSalary">The salary.</param>
        /// <param name="year">The year.</param>
        /// <returns>A <see cref="IncomeTax"/> object with a calculated income tax or <c>null</c> if no rate was found.</returns>
        public IncomeTax? CalculateIncomeTax(double salary, int year);
    }
}

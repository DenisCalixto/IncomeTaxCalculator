using IncomeTaxCalculator.Calculation;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

public class IncomeTaxCalculationStrategyTest
{
    [Theory]
    [InlineData(2019, 150)]
    [InlineData(2020, 200)]
    public void CalculateIncomeTax_YearInRange_FirstElementInYearRange(int year, double expectedValue)
    {
        var calculatorLogger = new Mock<ILogger<IncomeTaxCalculationStrategy>>();
        var repoLogger = new Mock<ILogger<MockTaxRatesRepository>>();
        var taxRatesRepo = new MockTaxRatesRepository(repoLogger.Object);
        var calculator = new IncomeTaxCalculationStrategy(calculatorLogger.Object, taxRatesRepo);

        var incomeTax = calculator.CalculateIncomeTax(1000, year);
        Assert.NotNull(incomeTax);
        Assert.Equal(expectedValue, incomeTax!.TotalIncomeTax);
    }

    [Theory]
    [InlineData(2019, 99000)]
    [InlineData(2020, 111000)]
    public void CalculateIncomeTax_YearInRange_LastElementInYearRange(int year, double expectedValue)
    {
        var calculatorLogger = new Mock<ILogger<IncomeTaxCalculationStrategy>>();
        var repoLogger = new Mock<ILogger<MockTaxRatesRepository>>();
        var taxRatesRepo = new MockTaxRatesRepository(repoLogger.Object);
        var calculator = new IncomeTaxCalculationStrategy(calculatorLogger.Object, taxRatesRepo);

        var incomeTax = calculator.CalculateIncomeTax(300000, year);
        Assert.NotNull(incomeTax);
        Assert.Equal(expectedValue, incomeTax!.TotalIncomeTax);
    }

    [Fact]
    public void CalculateIncomeTax_NoYearInRange()
    {
        var calculatorLogger = new Mock<ILogger<IncomeTaxCalculationStrategy>>();
        var repoLogger = new Mock<ILogger<MockTaxRatesRepository>>();
        var taxRatesRepo = new MockTaxRatesRepository(repoLogger.Object);
        var calculator = new IncomeTaxCalculationStrategy(calculatorLogger.Object, taxRatesRepo);

        var incomeTax = calculator.CalculateIncomeTax(10000, 2030);
        Assert.Null(incomeTax);

        incomeTax = calculator.CalculateIncomeTax(300000, 2030);
        Assert.Null(incomeTax);
    }
}
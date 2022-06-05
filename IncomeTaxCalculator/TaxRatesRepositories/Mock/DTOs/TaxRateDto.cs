namespace IncomeTaxCalculator.TaxRatesRepositories.Mock.DTOs
{
    public struct TaxRateDto
    {
        public int Year { get; set; }

        public double? Start { get; set; }

        public double? End { get; set; }

        public double Rate { get; set; }
    }
}

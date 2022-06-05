namespace IncomeTaxCalculator.TaxRatesRepositoryContracts.DTOs
{
    public struct GenericTaxRateDto
    {
        public int Year { get; set; }

        public double? RangeStart { get; set; }

        public double? RangeEnd { get; set; }

        public double Rate { get; set; }
    }
}

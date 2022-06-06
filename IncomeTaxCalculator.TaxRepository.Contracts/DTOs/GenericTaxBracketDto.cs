namespace IncomeTaxCalculator.TaxRatesRepository.Contracts.DTOs
{
    public struct GenericTaxBracketDto
    {
        public int Year { get; set; }

        public double RangeStart { get; set; }

        public double? RangeEnd { get; set; }

        public double Rate { get; set; }
    }
}

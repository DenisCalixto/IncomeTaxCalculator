namespace IncomeTaxCalculator.TaxRatesRepositories.InterviewServer.DTOs
{
    public struct TaxBracketsDto
    {        
        public TaxRateDto[] Tax_Brackets { get; set; }
    }

    public struct TaxRateDto
    {
        public double? Max { get; set; }

        public double Min { get; set; }

        public double Rate { get; set; }
    }
}

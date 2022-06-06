namespace IncomeTaxCalculator.TaxRatesRepositories.InterviewServer.DTOs
{
    public struct TaxBracketsDto
    {        
        public TaxBracketDto[] Tax_Brackets { get; set; }
    }

    public struct TaxBracketDto
    {
        public double? Max { get; set; }

        public double Min { get; set; }

        public double Rate { get; set; }
    }
}

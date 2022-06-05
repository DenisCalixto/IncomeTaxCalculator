namespace IncomeTaxCalculator.Models
{
    public class IncomeTax
    {
        public double totalIncomeTax;

        public int Year { get; init; }

        public double Salary { get; init; }

        public double? TotalIncomeTax => totalIncomeTax;

        public IncomeTax(int year, double salary)
        {
            Year = year;
            Salary = salary;
        }

        public void CalculateTotalIncomeTax(double taxRate)
        {
            totalIncomeTax = Salary * 100 / taxRate;
        }
    }
}

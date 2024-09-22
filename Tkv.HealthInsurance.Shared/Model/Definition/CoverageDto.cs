namespace Tkv.HealthInsurance.Shared.Model.Definition
{
    public class CoverageDto
    {
        public long Id { get; set; }
        public int InternationalCode { get; set; }
        public string Title { get; set; } = string.Empty;
        public long MinimumDeposit { get; set; } 
        public long MaximumDeposit { get; set; } 
        public double PriceMultiplier { get; set; } 
        public DateTime InsertDateTime { get; set; } 
        public DateTime LastModifiedDateTime { get; set; } 
    }
}

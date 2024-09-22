using Tkv.HealthInsurance.Domain.Domains.RequestLog;

namespace Tkv.HealthInsurance.Domain.Domains.Definition
{
    public class Coverage : DomainBase
    {
        public int InternationalCode { get; set; }
        public string Title { get; set; } = string.Empty;
        public long MinimumDeposit { get; set; } 
        public long MaximumDeposit { get; set; } 
        public double PriceMultiplier { get; set; } 
        public virtual ICollection<RequestLogDetail>? RequestLogDetails { get; set; }
    }
}

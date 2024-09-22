using Tkv.HealthInsurance.Domain.Domains.Definition;

namespace Tkv.HealthInsurance.Domain.Domains.RequestLog
{
    public class RequestLogDetail : DomainBase
    {
        public long RequestLogMasterId { get; set; }
        public long CoverageId { get; set; }
        public long DepositAmount { get; set; }
        public double CalculatedValue { get; set; }
        public virtual RequestLogMaster? RequestLogMaster { get; set; }
        public virtual Coverage? Coverage { get; set; }
    }
}

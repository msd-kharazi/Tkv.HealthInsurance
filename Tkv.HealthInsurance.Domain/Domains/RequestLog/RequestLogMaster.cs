namespace Tkv.HealthInsurance.Domain.Domains.RequestLog
{
    public class RequestLogMaster : DomainBase
    {
        public string Title { get; set; } = string.Empty;
        public virtual ICollection<RequestLogDetail> RequestLogDetails { get; set; } = [];
    }
}

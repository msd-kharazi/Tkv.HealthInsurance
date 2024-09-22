using Tkv.HealthInsurance.Shared.Model.Request.SubmitNewRequest;

namespace Tkv.HealthInsurance.Shared.Model.Request.GetRequestLogs
{
    public class GetRequestLogsMasterResponse
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime InsertDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        public IEnumerable<GetRequestLogsDetailResponse>? RequestLogDetails { get; set; }
    }
}

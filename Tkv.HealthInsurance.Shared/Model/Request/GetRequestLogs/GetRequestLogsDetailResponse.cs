namespace Tkv.HealthInsurance.Shared.Model.Request.GetRequestLogs
{
    public class GetRequestLogsDetailResponse
    {
        public long Id { get; set; }
        public string CoverageTitle { get; set; } = string.Empty;
        public int CoverageInternationalCode { get; set; }
        public long DepositAmount { get; set; }
        public double CalculatedValue { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Tkv.HealthInsurance.Shared.Model.Request.SubmitNewRequest
{
    public class SubmitNewRequestDetailRequest
    {
        [Display(Name = "InternationalCode")]
        [Range(100, 300, ErrorMessage = "{0} must be between '{1}' and '{2}'.")]
        public int InternationalCode { get; set; }

        [Display(Name = "DepositAmount")]
        [Range(2000, 500000000, ErrorMessage = "{0} must be between '{1}' and '{2}'.")]
        public long DepositAmount { get; set; }
    }
}

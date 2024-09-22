using System.ComponentModel.DataAnnotations;
using Tkv.HealthInsurance.Shared.Model.Definition;

namespace Tkv.HealthInsurance.Shared.Model.Request.SubmitNewRequest
{
    public class SubmitNewRequestMasterRequest
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0}'s length should be between {2} and {1}.")]
        public string Title { get; set; } = string.Empty;
        public IEnumerable<SubmitNewRequestDetailRequest> RequestDetails { get; set; } = [];
    }
}

using System.ComponentModel.DataAnnotations;
using Tkv.HealthInsurance.Shared.Model.Definition;

namespace Tkv.HealthInsurance.Shared.Model.Request.SubmitNewRequest
{
    public class SubmitNewRequestResponse
    {
        public int InternationalCode { get; set; }

        public double CalculatedValue { get; set; }
    }
}

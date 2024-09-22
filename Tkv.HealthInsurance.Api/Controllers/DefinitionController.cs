using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tkv.HealthInsurance.Domain;
using Tkv.HealthInsurance.Shared.Model.Definition;

namespace Tkv.HealthInsurance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DefinitionController : ControllerBase
    {



        #region [Properties]


        private readonly HealthInsuranceDbContext _db;

        //Usually I use Serilog, But false for this 1 hour sample.
        private readonly ILogger<DefinitionController> _logger;


        #endregion



        #region [ctor]


        public DefinitionController(HealthInsuranceDbContext db, ILogger<DefinitionController> logger)
        {
            _db = db;
            _logger = logger;
        }


        #endregion



        #region [Methods]


        [HttpGet(Name = "GetCoveragesList")]
        public IEnumerable<CoverageDto> GetCoveragesList()
        {
            try
            {
                var allCoverages = _db.Coverage.Where(x => x.DeletedDateTime == null)
                    .OrderBy(x => x.LastModifiedDateTime)
                    .Select(x=>new CoverageDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        InternationalCode = x.InternationalCode,
                        MinimumDeposit = x.MinimumDeposit,
                        MaximumDeposit = x.MaximumDeposit,
                        PriceMultiplier = x.PriceMultiplier,
                        InsertDateTime = x.InsertedDateTime,
                        LastModifiedDateTime = x.LastModifiedDateTime
                    }).AsEnumerable();

                return allCoverages;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debug.WriteLine(e);
                _logger.Log(LogLevel.Error,e.Message);
                throw;
            }
        }



        #endregion



    }
}

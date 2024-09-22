using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tkv.HealthInsurance.Domain;
using Tkv.HealthInsurance.Domain.Domains.RequestLog;
using Tkv.HealthInsurance.Shared.Model.Request.GetRequestLogs;
using Tkv.HealthInsurance.Shared.Model.Request.SubmitNewRequest;

namespace Tkv.HealthInsurance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController : ControllerBase
    {



        #region [Properties]


        private readonly HealthInsuranceDbContext _db;

        //Usually I use Serilog, But false for this 1 hour sample.
        private readonly ILogger<RequestController> _logger;


        #endregion



        #region [ctor]


        public RequestController(HealthInsuranceDbContext db, ILogger<RequestController> logger)
        {
            _db = db;
            _logger = logger;
        }


        #endregion



        #region [Methods]


        [HttpPost(Name = "SubmitNewRequest")]
        public async Task<IEnumerable<SubmitNewRequestResponse>> SubmitNewRequest([FromBody] SubmitNewRequestMasterRequest model)
        {
            try
            {



                #region [Validate request]


                if (!ModelState.IsValid)
                {
                    //TODO: Get all error messages from ModelState and throw
                    throw new Exception("Bad request");
                }

                if (!model.RequestDetails.Any())
                {
                    throw new Exception("You must enter at least one coverage request");
                }


                var allRequestedCoverageCodes = model.RequestDetails.Select(x => x.InternationalCode);

                var allCoverages = await _db.Coverage.Where(x => allRequestedCoverageCodes.Contains(x.InternationalCode)
                                                                 && x.DeletedDateTime == null).ToListAsync();

                var notExistedCodes = model.RequestDetails.Where(x =>
                    !allCoverages.Select(y => y.InternationalCode).Contains(x.InternationalCode)).ToList();


                if (notExistedCodes.Any())
                {
                    throw new Exception(
                        $"Coverage(s) with international codes '{string.Join(", ", notExistedCodes)}' do not exist!");
                }


                #endregion


                var now = DateTime.Now;


                #region [Create response & db model]

                var newRequestLogMaster = new RequestLogMaster
                {
                    Title = model.Title,
                    RequestLogDetails = new List<RequestLogDetail>(),
                    InsertedDateTime = now,
                    LastModifiedDateTime = now
                };




                var responseModel = new List<SubmitNewRequestResponse>();

                foreach (var submitNewRequestDetailRequest in model.RequestDetails)
                {
                    var coverage = allCoverages.First(y =>
                        y.InternationalCode == submitNewRequestDetailRequest.InternationalCode);

                    var detailResponse = new SubmitNewRequestResponse
                    {
                        InternationalCode = submitNewRequestDetailRequest.InternationalCode,
                        CalculatedValue = coverage.PriceMultiplier * submitNewRequestDetailRequest.DepositAmount,
                    };

                    responseModel.Add(detailResponse);

                    var dbDetail = new RequestLogDetail
                    {
                        CoverageId = coverage.Id,
                        DepositAmount = submitNewRequestDetailRequest.DepositAmount,
                        CalculatedValue = detailResponse.CalculatedValue,
                        InsertedDateTime = now,
                        LastModifiedDateTime = now
                    };
                    newRequestLogMaster.RequestLogDetails.Add(dbDetail); 
                }

                #endregion


                #region [Log in db]
                 
                await _db.RequestLogMaster.AddAsync(newRequestLogMaster);

                await _db.SaveChangesAsync();

                #endregion

                 

                return responseModel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debug.WriteLine(e);
                _logger.Log(LogLevel.Error,e.Message);
                throw;
            }
        }


        [HttpGet(Name = "GetRequestLogs")]
        public async Task<IEnumerable<GetRequestLogsMasterResponse>> GetRequestLogs()
        {
            try
            {


                var responseModel = await _db.RequestLogMaster
                    .Include(x => x.RequestLogDetails)
                    .ThenInclude(x => x.Coverage)
                    .Where(x => x.DeletedDateTime == null) 
                    .Select(x => new GetRequestLogsMasterResponse
                    {
                        Id = x.Id,
                        Title = x.Title,
                        InsertDateTime = x.InsertedDateTime,
                        LastModifiedDateTime = x.LastModifiedDateTime,
                        RequestLogDetails =  x.RequestLogDetails.Select(y => new GetRequestLogsDetailResponse
                            {
                                Id = y.Id,
                                CoverageTitle = y.Coverage == null ? string.Empty : y.Coverage.Title,
                                CoverageInternationalCode = y.Coverage == null ? default : y.Coverage.InternationalCode,
                                DepositAmount = y.DepositAmount,
                                CalculatedValue = y.CalculatedValue
                            }),
                    })
                    .OrderByDescending(x => x.LastModifiedDateTime)
                    .ToListAsync();
                 

                return responseModel;
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

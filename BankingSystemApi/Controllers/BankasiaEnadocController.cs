using AppData.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystemApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BankasiaEnadocController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public BankasiaEnadocController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetReportAsync([FromQuery] string type, [FromQuery] string branch, [FromQuery] string user, [FromQuery] string? fromDate, [FromQuery] string? toDate)
        {
            try
            {
                var reports = await _dataRepository.GetBankasiaEnadoc(type, branch, user, fromDate, toDate);
                
                if (reports is null)
                {
                    return NotFound();
                }
                return Ok(reports);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }

        }


        [HttpGet]
        public async Task<IActionResult> GetAllBranchesAsync([FromQuery] int documentIndex)
        {
            try
            {
                var allBranches = await _dataRepository.GetAllBranches(documentIndex);
                if (allBranches is null)
                {
                    return NotFound();
                }
                return Ok(allBranches);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }


        }


    }
}

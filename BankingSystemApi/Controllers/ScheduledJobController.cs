using AppData.Repository;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystemApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScheduledJobController : ControllerBase
    {
        private readonly IUserCbsRepository _userCbsRepository;
        public ScheduledJobController(IUserCbsRepository userCbsRepository)
        {
            _userCbsRepository = userCbsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> ClearCbsInfoViewTbl()
        {
            //var schediledDateTime = DateTime.UtcNow.AddDays(1);
            var schediledDateTime = DateTime.UtcNow.AddSeconds(5);

            var datetimeOffset = new DateTimeOffset(schediledDateTime);

            //await _userCbsRepository.ClearTableRecords("CustomerCbsInfoViewTbl");

            BackgroundJob.Schedule<UserCbsRepository>(x => x.ClearTableRecords("CustomerCbsInfoViewTbl"), datetimeOffset);

            //BackgroundJob.Schedule(() => Console.WriteLine("Records Cleared.........."), datetimeOffset);

            return Ok();

        }
        
        [HttpPost]
        public ActionResult CreateScheduledJob() 
        {
            var schediledDateTime = DateTime.UtcNow.AddDays(1);

            var datetimeOffset  = new DateTimeOffset(schediledDateTime);
            BackgroundJob.Schedule<UserCbsRepository>(x => x.ClearTableRecords("CustomerCbsInfoViewTbl"), datetimeOffset);
            //BackgroundJob.Schedule(() => _userCbsRepository.ClearTableRecords("CustomerCbsInfoViewTbl"), datetimeOffset);

            return Ok(); 
        }  

    }
}

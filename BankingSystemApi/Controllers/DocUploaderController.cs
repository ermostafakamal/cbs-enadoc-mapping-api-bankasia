//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace BankingSystemApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class DocUploaderController : ControllerBase
//    {
//    }
//}

//=======================
using AppData.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystemApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DocUploaderController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public DocUploaderController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }




        [HttpGet]
        public async Task<IActionResult> GetAllDocUploadersAsync()
        {
            try
            {
                var allUploaders = await _dataRepository.GetAllUploaders();
                if (allUploaders is null)
                {
                    return NotFound();
                }
                return Ok(allUploaders);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }


        }


    }
}

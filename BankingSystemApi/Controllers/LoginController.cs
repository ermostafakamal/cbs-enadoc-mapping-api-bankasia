using AppData.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BankingSystemApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginDBRepository _dataRepository;

        public LoginController(ILoginDBRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public async Task<bool> IsAuthenticAsync([FromQuery] string username, [FromQuery] string password)
        {
            
            try
            {                
                var login = await _dataRepository.GetReportLoginPerson(username, password);                

                if (login is null || login.Count() == 0 )
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                //log error
                return false;
            }
            

        }
    }
}

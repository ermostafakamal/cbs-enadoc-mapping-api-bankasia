using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using AppData.Models;
using System.Text.Json;
using BankingSystemApi.Services;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using Microsoft.IdentityModel.Tokens;
using Azure;
using Azure.Core;
using AppData.Repository;
using Microsoft.AspNetCore.Authentication;
using System;
//using Newtonsoft.Json;

namespace BankingSystemApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccessTokenController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUserCbsRepository _userCbsRepository;
        //private readonly IHttpClientFactory _clientFactory;
        public AccessTokenController(IConfiguration configuration, IUserCbsRepository userCbsRepository)
        {
            _configuration = configuration;
            _userCbsRepository = userCbsRepository;
            //_clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccessTokenAsync()
        {  
            try
            {
                using var client = new HttpClient();
                //client.BaseAddress = new Uri("https://sandbox.bankasia-bd.com/internal/int/v1/auth/token");
                client.BaseAddress = new Uri(_configuration.GetValue<string>("TokenGenApi"));
                var credentialModel = new CbsAccessToken();

                credentialModel.username = _configuration.GetSection("BankApiConfig").GetValue<string>("username");
                var userPass = _configuration.GetSection("BankApiConfig").GetValue<string>("password");

                var instaAES = new AESEncryptDecrypt();
                credentialModel.password = instaAES.Decrypt(userPass);

                HttpContent body = new StringContent(JsonSerializer.Serialize(credentialModel), Encoding.UTF8, "application/json");
                var response = client.PostAsync("", body).Result;
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        public string GetAccToken()
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(_configuration.GetValue<string>("TokenGenApi"));
                var credentialModel = new CbsAccessToken();

                credentialModel.username = _configuration.GetSection("BankApiConfig").GetValue<string>("username");
                var userPass = _configuration.GetSection("BankApiConfig").GetValue<string>("password");

                var instaAES = new AESEncryptDecrypt();
                credentialModel.password = instaAES.Decrypt(userPass);

                HttpContent body = new StringContent(JsonSerializer.Serialize(credentialModel), Encoding.UTF8, "application/json");

                var allResponses = client.PostAsync("", body).Result;
                var responses = (allResponses.Content.ReadAsStringAsync()).Result;

                if (responses != null)
                {
                    return responses.ToString();
                }

                return null;

            }
            catch (Exception ex)
            {
                return null;
            }

        }


        [HttpGet]
        public async Task<IActionResult> GetCustomerCbsDataAsync([FromQuery] string accountNo)
        {
            try
            {                
                var resultContent = GetAccToken();  
                TokenResponse tokenResponse = JsonSerializer.Deserialize<TokenResponse>(resultContent);             

                if (tokenResponse.accessToken != null)
                {                    
                    var cbsClientHandler = new HttpClientHandler();
                    using var cbsClient = new HttpClient(cbsClientHandler);
                    cbsClient.DefaultRequestHeaders.Add("userid", "DMS");

                    cbsClient.BaseAddress = new Uri(_configuration.GetValue<string>("CbsDataApi"));
                    
                    cbsClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    cbsClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", tokenResponse.accessToken);
                    cbsClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    

                    var _cbsApiObj = new { ACC_NO = accountNo, AUTH_DATA = "", REF_NO = "" };
                    var _cbsApiBodyObj = new { ACCOUNT_INFO = _cbsApiObj };                   

                    HttpContent bodyWithToken = new StringContent(JsonSerializer.Serialize(_cbsApiBodyObj), Encoding.UTF8, "application/json");


                    var cbsResponses = cbsClient.PostAsync("", bodyWithToken);
                    
                    var cbsResponse = (cbsResponses.Result.Content.ReadAsStringAsync()).Result;

                    return Ok(cbsResponse);

                }
                else {
                    return NotFound();
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }



        [HttpPost]
        public async Task<IActionResult> CreateCbsDataAsync([FromQuery] string accountNo)
        {
            try
            {
                var cbsData = await GetCustomerCbsDataAsync(accountNo);
                var okResult = cbsData as OkObjectResult;                

                var newBatch = okResult.Value;

                ActionResultResponse obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ActionResultResponse>(newBatch.ToString());

                                
                    if (obj.RESPONSE_CODE == "422" )
                    {
                        
                        return NotFound();
                    }
                    else {
                    //CustomerCbsData resMsgObj = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerCbsData>(obj.RESPONSE_MSG.ToString());

                    TypedActionResultResponse _customerCbsData = (Newtonsoft.Json.JsonConvert.DeserializeObject<TypedActionResultResponse>(newBatch.ToString()));

                        var _cbsToDmsData = new CustomerCbsData()
                        {
                            ACC_NO = accountNo,
                            AC_TITLE = _customerCbsData.RESPONSE_MSG.AC_TITLE,
                            AC_TYPE = _customerCbsData.RESPONSE_MSG.AC_TYPE,
                            BIRTH_REGISTRATION_NO = _customerCbsData.RESPONSE_MSG.BIRTH_REGISTRATION_NO,
                            CUSTOMER_CODE = _customerCbsData.RESPONSE_MSG.CUSTOMER_CODE,
                            MOBILE_NO = _customerCbsData.RESPONSE_MSG.MOBILE_NO,
                            NID_NO = _customerCbsData.RESPONSE_MSG.NID_NO,
                            PASSPORT_NO = _customerCbsData.RESPONSE_MSG.PASSPORT_NO,
                            TIN_NO = _customerCbsData.RESPONSE_MSG.TIN_NO,
                            TRADE_LICENSE_NO = _customerCbsData.RESPONSE_MSG.TRADE_LICENSE_NO,
                        };

                        var createdCbsInfo = await _userCbsRepository.SaveCustomerCbsData(_cbsToDmsData);

                        return Ok(createdCbsInfo);
                    }                                

            }
            catch (Exception ex)
            {
                //log error
                //return BadRequest(ex);
                return BadRequest(new { StatusMessage = "Data Save Failed !", StatusCode = 400 });
            }

        }
        
    }


}

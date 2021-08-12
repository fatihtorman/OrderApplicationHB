using Entity;
using Entity.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiGatewayController : ControllerBase
    {


        private const string CustomerUrl = "https://localhost:5004/api/customer";
        private const string OrderUrl = "https://localhost:5006/api/orders";
        private const string AuthUrl = "https://localhost:5001/api/auth";

        private readonly ILogger<ApiGatewayController> _logger;
        private HttpClient _client = new HttpClient();


        public ApiGatewayController(ILogger<ApiGatewayController> logger)
        {
            _logger = logger;
        }

        private string GetTokenUrl(string user, string pass)
        {
            return AuthUrl + "/?name=" + user + "&pwd=" + pass;/*+ "?"
                   + "access_key=" + "ApiKey"
                   + "&query=" + parameter;*/
        }

        private string GetCustomerUrl(string parameter)
        {
            return CustomerUrl + "/" + parameter;
        }

        private string ValidateCustomerUrl(string parameter)
        {
            return CustomerUrl + "/validate/" + parameter;
        }

        private string CreateCustomerUrl()
        {
            return CustomerUrl;
        }

        private string GetOrderUrl(string parameter)
        {
            return OrderUrl + "/" + parameter;
        }

        private string CreateOrderUrl()
        {
            return OrderUrl;
        }



        // GET: api/GetToken
        [HttpGet("GetToken")]
        public async Task<IActionResult> GetToken(string name, string pwd)
        {
            try
            {
                _client = new HttpClient();
                var responseStream = await _client.GetStringAsync(GetTokenUrl(name, pwd));
                var accessToken = (string)responseStream;

                return Ok(accessToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong!" + e.Message);
                return null;
            }
        }

        /*CustomerMethods*/

        [HttpGet("GetCustomer")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                Customer customers = new Customer();
                var response = await _client.GetAsync(GetCustomerUrl(id.ToString()));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                customers = JsonConvert.DeserializeObject<Customer>(responseBody);

                return Ok(customers);

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong!" + e.Message);
                return null;
            }
        }

        [HttpPut("UpdateCustomer")]
        public async void UpdateCustomer(int id, Customer customer)
        {
            try
            {

                var myContent = JsonConvert.SerializeObject(customer);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                using var responseStream = await _client.PutAsync(GetCustomerUrl(id.ToString()), byteContent);

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong!" + e.Message);

            }
        }

        [HttpDelete("DeleteCustomer")]
        public async void DeleteCustomer(int id)
        {
            try
            {
                using var responseStream = await _client.DeleteAsync(GetCustomerUrl(id.ToString()));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong!" + e.Message);
            }
        }

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            try
            {
                var myContent = JsonConvert.SerializeObject(customer);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                //_client.DefaultRequestHeaders.Add("access-token", _token);
                using var responseStream = await _client.PostAsync(CreateCustomerUrl(), byteContent);
                if (responseStream != null)
                {
                    var jsonString = await responseStream.Content.ReadAsStringAsync();

                    // var result = JsonConvert.DeserializeObject<Customer>(jsonString);
                    return Ok(jsonString);
                }
                else
                {
                    return BadRequest();
                }


            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong!" + e.Message);
                return null;

            }
        }

        [HttpGet("ValidateCustomer")]
        public async Task<IActionResult> ValidateCustomer(int id)
        {
            try
            {
                using var responseStream = await _client.GetAsync(ValidateCustomerUrl(id.ToString()));
                if (responseStream != null)
                {
                    if (responseStream.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return Ok();
                    }
                }
                return NotFound();

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong!" + e.Message);
                throw new Exception(e.Message);
            }
        }



        /*OrderMethods*/

        [HttpGet("GetOrder")]
        public async Task<IActionResult> GetOrder(int id)
        {
            try
            {
                Order orders = new Order();
                var response = await _client.GetAsync(GetOrderUrl(id.ToString()));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                orders = JsonConvert.DeserializeObject<Order>(responseBody);

                return Ok(orders);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong!" + e.Message);
                return null;
            }
        }


        [HttpPut("UpdateOrder")]
        public async void UpdateOrder(int id, Order order)
        {
            try
            {
                var myContent = JsonConvert.SerializeObject(order);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                using var responseStream = await _client.PutAsync(GetOrderUrl(id.ToString()), byteContent);

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong!" + e.Message);

            }
        }

        [HttpDelete("DeleteOrder")]
        public async void DeleteOrder(int id)
        {
            try
            {
                using var responseStream = await _client.DeleteAsync(GetOrderUrl(id.ToString()));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong!" + e.Message);
            }
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            try
            {
                var myContent = JsonConvert.SerializeObject(order);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                //_client.DefaultRequestHeaders.Add("access-token", _token);
                using var responseStream = await _client.PostAsync(CreateOrderUrl(), byteContent);
                if (responseStream != null)
                {
                    var responseString = await responseStream.Content.ReadAsStringAsync();

                    // var result = JsonConvert.DeserializeObject<Customer>(jsonString);
                    return Ok(responseString);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong!" + e.Message);
                return null;

            }
        }

        [HttpPut("ChangeStatusOrder")]
        public async void ChangeStatusOrder(int id, string status)
        {
            try
            {
                var buffer = System.Text.Encoding.UTF8.GetBytes(status);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                using var responseStream = await _client.PatchAsync(GetOrderUrl(id.ToString()), byteContent);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong!" + e.Message);
            }
        }

    }
}

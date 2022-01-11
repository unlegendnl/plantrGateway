using authService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;

namespace Plantr.Gateway.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IConfiguration _configuration;

        public AuthenticationController(ILogger<AuthenticationController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public string Get()
        {
            return "GET endpoint";
        }

        [HttpPost]
        [Route("/login")]
        public async Task<string> Login([FromBody] User login)
        {
            string authUri = _configuration.GetValue<string>("Microservices:AuthService");
            string responseBody = string.Empty;

            using(var client = new HttpClient())
            {
                //var response = await client.PostAsync($"{authUri}/api/authentication", new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json"));
                var response = await client.PostAsync("plantr-auth-service/api/authentication", new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json"));
                responseBody = await response.Content.ReadAsStringAsync();
            }

            return responseBody;
        }

        [HttpPost]
        [Route("/register")]

        public async Task<string> Register([FromBody] User login)
        {
            string authUri = _configuration.GetValue<string>("Microservices:AuthService");
            string responseBody = string.Empty;

            using (var client = new HttpClient())
            {
                //var response = await client.PostAsync($"{authUri}/api/user", new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json"));
                var response = await client.PostAsync("plantr-auth-service/api/user", new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json"));
                responseBody = await response.Content.ReadAsStringAsync();
            }

            return responseBody;
        }
        
        [HttpPost]
        [Route("/profile")]

        public async Task<string> Profile([FromBody] User login)
        {
            string authUri = _configuration.GetValue<string>("Microservices:ProfileService");
            string responseBody = string.Empty;

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync("plantr-auth-service/api/profile", new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json"));
                responseBody = await response.Content.ReadAsStringAsync();
            }

            return responseBody;
        }




    }
}

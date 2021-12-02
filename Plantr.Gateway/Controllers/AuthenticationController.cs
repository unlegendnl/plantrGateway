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

namespace Plantr.Gateway.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        [Route("/login")]
        public async Task<string> Login([FromBody] User login)
        {
            var json = JsonConvert.SerializeObject(User);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://httpbin.org/post";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            var respBody = await response.Content.ReadAsStringAsync();

            return respBody;
        }

        [HttpPost]
        [Route("/register")]

        public IActionResult Register([FromBody] User login)
        {


            return null;
        }




    }
}

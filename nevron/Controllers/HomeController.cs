using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nevron.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace nevron.Controllers
{
    public class HomeController : Controller
    {
        private const int MIN_NUMBER = 1;
        private const int MAX_NUMBER = 100;
        private const string NUMBERS = "Numbers";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }            

        [HttpPost("/clearNumbers")]
        public IActionResult ClearNumbers()
        {
            Numbers = new List<int>();
            return Ok();
        }

        [HttpPost("/addNumber")]
        public IActionResult AddNumber()
        {
            var numbers = HttpContext.Session.Get<List<int>>(NUMBERS) ?? new List<int>();
            var rand = new Random();
            numbers.Add(rand.Next(MIN_NUMBER, MAX_NUMBER));
            HttpContext.Session.Set(NUMBERS, numbers);
            return Ok(numbers.Count());
        }

        [HttpGet("/sumNumbers")]
        public IActionResult SumNumbers()
        {
            int sum = 0;
            foreach (int num in Numbers)
            {
                sum += num;
            }
            return Ok(new { sum });
        }

        [HttpGet("/getNumbers")]
        public IActionResult GetNumbers()
        {
            return Ok(new { numbers = Numbers });
        }

        private List<int> Numbers
        {
            get
            {
                if (HttpContext.Session.TryGetValue(NUMBERS, out byte[] value))
                {
                    return System.Text.Json.JsonSerializer.Deserialize<List<int>>(value);
                }
                else
                {
                    var numbers = new List<int>();
                    HttpContext.Session.Set(NUMBERS, System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(numbers));
                    return numbers;
                }
            }
            set
            {
                HttpContext.Session.Set(NUMBERS, System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(value));
            }
        }
    }
}

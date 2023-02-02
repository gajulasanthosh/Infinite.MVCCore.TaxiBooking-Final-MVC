using Infinite.MVCCore.TaxiBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infinite.MVCCore.TaxiBooking.Controllers
{
    public class FeedbackController : Controller
    {
        public readonly IConfiguration _configuration;
        public FeedbackController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            List<FeedbackViewModel> feedBacks = new();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync("Feedback/GetAllFeedBacks");
                if (result.IsSuccessStatusCode)
                {
                    feedBacks = await result.Content.ReadAsAsync<List<FeedbackViewModel>>();
                    
                }
            }
            return View(feedBacks);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            FeedbackViewModel feedBack = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"Feedback/GetFeedBackById/{id}");
                if (result.IsSuccessStatusCode)
                {
                    feedBack = await result.Content.ReadAsAsync<FeedbackViewModel>();
                }
            }
            return View(feedBack);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FeedbackViewModel feedBacks)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                    var result = await client.PostAsJsonAsync("Feedback/CreateFeedBack", feedBacks);
                    if (result.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return RedirectToAction("Landing","Customers");
                    }
                }
            }
            return View(feedBacks);
        }
    }
}


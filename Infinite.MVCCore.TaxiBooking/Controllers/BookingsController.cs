using Infinite.MVCCore.TaxiBooking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infinite.MVCCore.TaxiBooking.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IConfiguration _configuration;

        public BookingsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            List<BookingViewModel> bookings = new();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync("Bookings/GetAllBookings");
                if (result.IsSuccessStatusCode)
                {
                    bookings = await result.Content.ReadAsAsync<List<BookingViewModel>>();
                }
            }
            return View(bookings);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            BookingViewModel booking = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"Bookings/GetBookingById/{id}");
                if (result.IsSuccessStatusCode)
                {
                    booking = await result.Content.ReadAsAsync<BookingViewModel>();
                }
            }
            return View(booking);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            BookingViewModel viewModel = new BookingViewModel
            {
                Taxis = await this.GetTaxi(),
                //TaxiTypes = await this.GetTaxiTypes()
            };
            return View(viewModel);
            

        }
        [HttpPost]
        public async Task<IActionResult> Create(BookingViewModel booking)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
                    
                    var result = await client.PostAsJsonAsync("Bookings/CreateBooking", booking);
                    
                    if (result.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return RedirectToAction("Create","Feedback");
                    }
                    else
                    {
                        return RedirectToAction("Create", "Customers");
                    }
                }
            }
            BookingViewModel viewModel = new BookingViewModel
            {
                Taxis = await this.GetTaxi()
            };
            //return View(viewModel);
            //BookingViewModel viewModel = new BookingViewModel
            //{
            //    TaxiModels = await this.GetTaxiModels()
            //};
            //return View(viewModel);
            return View(booking);
        }


        public async Task<IActionResult> ApproveBooking(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]); var res = await client.PutAsync($"Employees/ApproveBooking/{id}", null);
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Bookings");
                }
                return BadRequest();
            }
        }

        
        public async Task<IActionResult> RejectBooking(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]); var res = await client.PutAsync($"Employees/RejectBookings/{id}", null);
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Bookings");
                }
                return BadRequest();
            }
        }


        [NonAction]
        public async Task<List<TaxiVM>> GetTaxi()
        {
            List<TaxiVM> taxis = new();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync("Taxi/GetAllTaxis");
                if (result.IsSuccessStatusCode)
                {
                    taxis = await result.Content.ReadAsAsync<List<TaxiVM>>();
                }

            }
            return taxis;
        }
    }
}
    

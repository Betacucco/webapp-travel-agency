using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelAgency.Models;

namespace TravelAgency.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public IActionResult TravelsDetails(int id)
        {
            return View("TravelsDetails", id);
        }
    }
}
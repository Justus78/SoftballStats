using Microsoft.AspNetCore.Mvc;
using SoftballStats.Models;
using System.Diagnostics;

namespace SoftballStats.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        } // end logger

        public IActionResult Index()
        {
            return View();
        } // end index get

       
    } // end controller
} // end namespace

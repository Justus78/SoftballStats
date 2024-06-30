using Microsoft.AspNetCore.Mvc;

namespace SoftballStats.Controllers
{
    public class TeamsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        } // end index get
    } // controller
} // end namespace
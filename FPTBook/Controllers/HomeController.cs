using FPTBook.Areas.Identity.Data;
using FPTBook.Data;
using FPTBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FPTBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly FPTBookContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<FPTBookUser> _userManager;
        private readonly int _recordsPerPage = 8;
        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender, UserManager<FPTBookUser> userManager, FPTBookContext context)
        {
            _context = context;
            _logger = logger;
            _emailSender = emailSender;
            _userManager = userManager;
        }      

        public async Task<IActionResult> Index()
        {
           
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
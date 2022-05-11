#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPTBook.Data;
using FPTBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FPTBook.Areas.Identity.Data;

namespace FPTBook.Controllers
{   
    public class OrderDetailsController : Controller
    {
        private readonly FPTBookContext _context;
        private readonly UserManager<FPTBookUser> _userManager;
        public OrderDetailsController(FPTBookContext context, UserManager<FPTBookUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        [Authorize(Roles = "Seller")]
        // GET: OrderDetails
        public async Task<IActionResult> Index(int id)
        {
            var userContext = _context.OrderDetail.Where(o => o.OrderId == id).Include(o => o.Book).Include(o => o.Order).Include(o => o.Order.User).Include(o => o.Book.Store);
            return View(await userContext.ToListAsync());
        }
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ManageOfOrder(int id)
        {
            string thisUserId = _userManager.GetUserId(HttpContext.User);
            var userContext = _context.OrderDetail.Where(o => o.Order.UId == thisUserId && o.OrderId == id).Include(o => o.Book).Include(o => o.Order).Include(o => o.Order.User).Include(o => o.Book.Store);
            return View(await userContext.ToListAsync());
        }
    }
}

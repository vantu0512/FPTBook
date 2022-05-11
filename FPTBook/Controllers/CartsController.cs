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
using Microsoft.AspNetCore.Identity;
using FPTBook.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace FPTBook.Controllers
{
    public class CartsController : Controller
    {
        private readonly FPTBookContext _context;
        private readonly UserManager<FPTBookUser> _userManager;

        public CartsController(FPTBookContext context,UserManager<FPTBookUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            //var fPTBookContext = _context.Cart.Include(c => c.Book).Include(c => c.User);
            // return View(await fPTBookContext.ToListAsync());
            string thisUserId = _userManager.GetUserId(HttpContext.User);
            return View(_context.Cart.Where(c => c.UId == thisUserId).Include(c => c.Book));
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.Book)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.UId == id);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["BookIsbn"] = new SelectList(_context.Book, "Isbn", "Isbn");
            ViewData["UId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UId,BookIsbn")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookIsbn"] = new SelectList(_context.Book, "Isbn", "Isbn", cart.BookIsbn);
            ViewData["UId"] = new SelectList(_context.Users, "Id", "Id", cart.UId);
            return View(cart);
        }
        public async Task<IActionResult> UpdateCart(string isbn)
        {
            string thisUserId = _userManager.GetUserId(HttpContext.User);
            Cart fromDb = _context.Cart.FirstOrDefault(c => c.UId == thisUserId && c.BookIsbn == isbn);

            fromDb.Quantity++;
            _context.Update(fromDb);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> removeItem(string isbn)
        {
            string thisUserId = _userManager.GetUserId(HttpContext.User);
            Cart fromDb = _context.Cart.FirstOrDefault(c => c.UId == thisUserId && c.BookIsbn == isbn);

            fromDb.Quantity--;
            while (fromDb.Quantity != 0)
            {
                _context.Update(fromDb);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Remove(string isbn)
        {
            string thisUserId = _userManager.GetUserId(HttpContext.User);
            Cart fromDb = _context.Cart.FirstOrDefault(c => c.UId == thisUserId && c.BookIsbn == isbn);
            _context.Remove(fromDb);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

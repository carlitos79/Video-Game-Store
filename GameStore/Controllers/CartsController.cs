using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;
using System.Collections.Generic;

namespace GameStore.Controllers
{
    public class CartsController : Controller
    {
        private readonly GameStoreContext _context;

        public CartsController(GameStoreContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var gameStoreContext = _context.Cart.Include(c => c.GameInfo);
            return View(await gameStoreContext.ToListAsync());
        }

        public IActionResult SendBackToStore()
        {
            return RedirectToAction("Index", "Games");
        }
        public IActionResult SendToSuggestion()
        {
            return RedirectToAction("ShowSuggestion", "Games");
        }

        public IActionResult SendToCreateOrder()
        {
            return RedirectToAction("Create", "Orders");
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.GameInfo)
                .FirstOrDefaultAsync(m => m.ShoppingCartId == id);

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        public async Task<IActionResult> ShowCarts()
        {
            var cart = CartLogic.GetCart(_context, HttpContext);
            var thisCart = await cart.GetCartItems();

            if (!thisCart.Any())
            {
                return RedirectToAction(nameof(SendBackToStore));
            }

            return View(thisCart);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var gameToAdd = _context.Game.SingleOrDefault(g => g.ID == id);
            var cartToAddTo = CartLogic.GetCart(_context, HttpContext);

            await cartToAddTo.AddToCart(gameToAdd);
            //gameToAdd.UnitsInStock--;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ShowCarts));
        }

        public async Task<IActionResult> EmptyCart()
        {
            var cartToEmpty = CartLogic.GetCart(_context, HttpContext);
            await cartToEmpty.RestoreStock();
            await cartToEmpty.EmptyCart();
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ShowCarts));
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Game, "ID", "ID");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,ShoppingCartId,GameId,GameTitle,UnitPrice,Quantity,Total,OrderDate")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Game, "ID", "ID", cart.GameId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.SingleOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Game, "ID", "ID", cart.GameId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,GameId,GameTitle,UnitPrice,Quantity,Total,OrderDate")] Cart cart)
        {
            if (id != cart.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.CartId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Game, "ID", "ID", cart.GameId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.Include(c => c.GameInfo).SingleOrDefaultAsync(m => m.CartId == id);

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = CartLogic.GetCart(_context, HttpContext);

            cart.RemoveFromCart(id);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ShowCarts));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.CartId == id);
        }
    }
}

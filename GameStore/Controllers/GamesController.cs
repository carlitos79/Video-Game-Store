using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;

namespace GameStore.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameStoreContext _context;

        public GamesController(GameStoreContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index(string searchString)
        {
            var game = from m in _context.Game
                       select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                game = game.Where(s => s.Title.Contains(searchString));
            }

            return View(await game.ToListAsync());
        }

        public IActionResult ShowSuggestion()
        {
            var cart = CartLogic.GetCart(_context, HttpContext);
            var suggestion = cart.GetSuggestion();

            if (suggestion == null)
            {
                return NotFound();
            }

            int length = suggestion.Count();

            Random rand = new Random();
            int randIndex = rand.Next(0, length);

            return View(suggestion[randIndex]);
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .SingleOrDefaultAsync(m => m.ID == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        public IActionResult SendToCreateCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = _context.Game.SingleOrDefault(g => g.ID == id);

            if (game == null)
            {
                return NotFound();
            }

            return RedirectToAction("AddToCart", "Carts", game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,GameImage,Title,Genre,Price,Description,UnitsInStock")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.SingleOrDefaultAsync(m => m.ID == id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,GameImage,Title,Genre,Price,Description,UnitsInStock")] Game game)
        {
            if (id != game.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.ID))
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
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.SingleOrDefaultAsync(m => m.ID == id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Game.SingleOrDefaultAsync(m => m.ID == id);
            _context.Game.Remove(game);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.ID == id);
        }
    }
}

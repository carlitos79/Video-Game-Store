﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;

namespace GameStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly GameStoreContext _context;

        public OrdersController(GameStoreContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Order.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Order order)
        {
            if (order == null)
            {
                return NotFound();
            }

            var orderId = await _context.Order.SingleOrDefaultAsync(m => m.OrderId == order.OrderId);

            if (orderId == null)
            {
                return NotFound();
            }

            return View(orderId);
        }      

        public IActionResult ShowPreviousOrder(Order prevOrder)
        {
            var order = _context.Order.SingleOrDefault(o => o.OrderId == prevOrder.OrderId);

            OrderCartViewModel viewModel = null;

            if (order != null)
            {
                viewModel = new OrderCartViewModel
                {
                    ReceiverFirstName = order.FirstName,
                    ReceiverLastname = order.LastName,
                    OrderCreationDate = order.OrderCreationDate,
                    PreviousOrderId = order.OrderShoppingCartId,
                    Total = order.Total
                };
            }

            return View(viewModel);
        }

        public async Task<IActionResult> PreviousOrder(int id)
        {            
            var order = from o in _context.Order
                        select o;

            order = order.Where(o => o.OrderId == id);

            var prevOrder = _context.Order.SingleOrDefault(o => o.OrderId == id);            

            if (id > 0)
            {
                return RedirectToAction(nameof(ShowPreviousOrder), prevOrder);
            }

            return View(await order.ToListAsync());
        }        

        public IActionResult SendBackToStore()
        {            
            return RedirectToAction("Index", "Games");
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,FirstName,LastName,IdentityNo,Address,PostalCode,City,Email,Phone,Total,OrderCreationDate")] Order order)
        {
            var cart = CartLogic.GetCart(_context, HttpContext);
            await cart.SetOrderIdNo(order);

            if (ModelState.IsValid)
            {
                _context.Add(order);
                await cart.EmptyCart();
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,FirstName,LastName,IdentityNo,Address,PostalCode,City,Email,Phone,Total,OrderCreationDate")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(m => m.OrderId == id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}

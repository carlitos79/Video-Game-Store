using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Models
{
    public class CartLogic
    {
        private readonly GameStoreContext _context;
        private readonly string _shoppingCartId;

        private CartLogic(GameStoreContext context, string id)
        {
            _context = context;
            _shoppingCartId = id;
        }

        public static CartLogic GetCart(GameStoreContext db, HttpContext context)
            => GetCart(db, GetCartId(context));

        public static CartLogic GetCart(GameStoreContext db, string cartId)
            => new CartLogic(db, cartId);

        public async Task AddToCart(Game game)
        {           
            var cartItem = await _context.Cart.SingleOrDefaultAsync(c => c.ShoppingCartId == _shoppingCartId && c.GameId == game.ID);

            if (game.UnitsInStock > 0)
            {
                if (cartItem == null)
                {
                    cartItem = new Cart
                    {
                        ShoppingCartId = _shoppingCartId,
                        GameId = game.ID,
                        UnitPrice = game.Price,
                        GameTitle = game.Title,
                        Quantity = 1,
                        Count = 0,
                        OrderDate = DateTime.Now
                    };

                    _context.Cart.Add(cartItem);
                }
                else
                {
                    cartItem.Quantity++;
                }
                cartItem.Total = game.Price + await GetPreliminaryTotal(cartItem.CartId);
                cartItem.FinalTotal = game.Price + await GetTotal();
                cartItem.Count++;
                cartItem.CanAdd = true;
                game.UnitsInStock--;
            }
            else if (game.UnitsInStock == 0)
            {
                cartItem.CanAdd = false;                
            }
        }

        public void RemoveFromCart(int id)
        {            
            var cartItem = _context.Cart.SingleOrDefault(c => c.ShoppingCartId == _shoppingCartId && c.CartId == id);
            var game = _context.Game.SingleOrDefault(g => g.ID == cartItem.GameId);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    cartItem.Total -= cartItem.UnitPrice;
                    cartItem.CanAdd = true;
                }
                else
                {
                    _context.Cart.Remove(cartItem);
                }                
            }
            game.UnitsInStock++;
            cartItem.Count--;
        }

        public async Task RestoreStock()
        {
            var cartItem = await _context.Cart.Where(c => c.ShoppingCartId == _shoppingCartId).ToArrayAsync();
            var games = (from g in _context.Game select g).ToList();
            var count = await _context.Cart.FirstOrDefaultAsync(c => c.ShoppingCartId == _shoppingCartId);

            List <Game> gameList = new List<Game>();

            foreach (var cart in cartItem)
            {
                foreach (var game in games)
                {
                    if (game.ID == cart.GameId)
                    {
                        gameList.Add(game);
                    }
                }
            }

            foreach (var game in gameList)
            {
                game.UnitsInStock += count.Count;
            }
        }

        public async Task EmptyCart()
        {
            var cartItem = await _context.Cart.Where(c => c.ShoppingCartId == _shoppingCartId).ToArrayAsync();
           
            _context.Cart.RemoveRange(cartItem);
        }

        public Task<decimal> GetTotal()
        {
            var items = from t in _context.Cart
                        where t.ShoppingCartId == _shoppingCartId
                        select t.UnitPrice * t.Quantity;            

            return items.SumAsync();
        }

        public Task<decimal> GetPreliminaryTotal(int id)
        {
            var prelTotal = from p in _context.Cart
                            where p.CartId == id
                            select p.UnitPrice * p.Quantity;

            return prelTotal.SumAsync();
        }

        public async Task SetOrderIdNo(Order order)
        {
            order.OrderShoppingCartId = _shoppingCartId;
            order.Total = await GetTotal();
            order.OrderCreationDate = DateTime.Now;
        }

        public Task<List<Cart>> GetCartItems()
        {
            return _context.Cart.Where(c => c.ShoppingCartId == _shoppingCartId).ToListAsync();
        }       

        public static string GetCartId(HttpContext context)
        {
            var cartId = context.Session.GetString("Session");

            if (cartId == null)
            {               
                cartId = Guid.NewGuid().ToString();
                
                context.Session.SetString("Session", cartId);
            }

            return cartId;
        }

        public List<Game> GetSuggestion()
        {
            var cart = _context.Cart.Where(c => c.ShoppingCartId == _shoppingCartId).ToArray();
            var games = (from g in _context.Game select g);

            List<Game> gameList = new List<Game>();

            foreach (var item in cart)
            {
                foreach (var game in games)
                {
                    if (item.GameId != game.ID)
                    {
                        gameList.Add(game);
                    }
                }
            }

            return gameList;
        }
    }
}

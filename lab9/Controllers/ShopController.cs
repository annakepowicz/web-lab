using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using lab9.Models;

namespace lab9.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopDbContext _context;

        public ShopController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: Shop/Index?categoryId=5
        public async Task<IActionResult> Index(int? categoryId)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            
            ViewBag.CurrentCategoryId = categoryId;

            var articlesQuery = _context.Articles.Include(c => c.Category).AsQueryable();

            if (categoryId.HasValue)
            {
                articlesQuery = articlesQuery.Where(a => a.CategoryId == categoryId);
            }

            var articles = await articlesQuery.ToListAsync();

            return View(articles);
        }

        [Authorize(Policy = "NotAdmin")]
        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            string cookieKey = "basket_" + id;
            int count = 1;

            if (Request.Cookies.ContainsKey(cookieKey))
            {
                if (int.TryParse(Request.Cookies[cookieKey], out int existingCount))
                {
                    count = existingCount + 1; 
                }
            }

            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true, 
                IsEssential = true
            };

            Response.Cookies.Append(cookieKey, count.ToString(), options);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "NotAdmin")]
        public async Task<IActionResult> Cart()
        {
            var cartItems = new List<CartItem>();
            
            foreach (var cookie in Request.Cookies)
            {
                if (cookie.Key.StartsWith("basket_"))
                {
                    if (int.TryParse(cookie.Key.Replace("basket_", ""), out int articleId))
                    {
                        var article = await _context.Articles
                            .Include(a => a.Category)
                            .FirstOrDefaultAsync(a => a.Id == articleId);

                        if (article != null)
                        {
                            int quantity = int.Parse(cookie.Value);
                            cartItems.Add(new CartItem { Article = article, Quantity = quantity });
                        }
                    }
                }
            }

            return View(cartItems);
        }

        [Authorize] // Wymaga zalogowania (Punkt 3)
        public async Task<IActionResult> Order()
        {
            var cartItems = new List<CartItem>();
            double total = 0;

            // Pobieranie danych z ciasteczek (podobnie jak w akcji Cart)
            foreach (var cookie in Request.Cookies)
            {
                if (cookie.Key.StartsWith("basket_"))
                {
                    if (int.TryParse(cookie.Key.Replace("basket_", ""), out int articleId))
                    {
                        var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == articleId);
                        if (article != null)
                        {
                            int quantity = int.Parse(cookie.Value);
                            cartItems.Add(new CartItem { Article = article, Quantity = quantity });
                            total += article.Price * quantity;
                        }
                    }
                }
            }

            if (!cartItems.Any()) return RedirectToAction("Index");

            var model = new OrderViewModel { 
                CartItems = cartItems, 
                TotalPrice = total 
            };
            return View(model);
        }

        // POST: Shop/ConfirmOrder
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmOrder(OrderViewModel model)
        {
            // Tutaj można dodać zapis do bazy danych (dla chętnych)

            // Punkt 6: Czyszczenie koszyka (usuwanie ciasteczek)
            foreach (var cookie in Request.Cookies.Keys)
            {
                if (cookie.StartsWith("basket_"))
                {
                    Response.Cookies.Delete(cookie);
                }
            }

            // Przekazujemy dane do widoku potwierdzenia
            return View("OrderConfirmation", model);
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int id, int delta)
        {
            string cookieKey = "basket_" + id;

            if (Request.Cookies.ContainsKey(cookieKey))
            {
                if (int.TryParse(Request.Cookies[cookieKey], out int currentCount))
                {
                    int newCount = currentCount + delta;

                    if (newCount <= 0)
                    {
                        // Usuwamy ciasteczko, jeśli ilość to 0 lub mniej
                        Response.Cookies.Delete(cookieKey);
                    }
                    else
                    {
                        // Aktualizujemy ciasteczko na kolejne 7 dni
                        Response.Cookies.Append(cookieKey, newCount.ToString(), new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(7),
                            HttpOnly = true,
                            IsEssential = true
                        });
                    }
                }
            }

            return RedirectToAction(nameof(Cart));
        }
    }
}
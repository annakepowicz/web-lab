using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}
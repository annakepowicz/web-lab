using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab11.Models;

namespace lab11.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly ShopDbContext _context;

        public IndexModel(ShopDbContext context)
        {
            _context = context;
        }

        public IList<Article> Articles { get; set; } = default!;
        public IList<Category> Categories { get; set; } = default!;
        public int? CurrentCategoryId { get; set; }

        public async Task OnGetAsync(int? categoryId)
        {
            // Pobieramy listę kategorii do bocznego menu
            Categories = await _context.Categories.ToListAsync();
            CurrentCategoryId = categoryId;

            // Przygotowujemy zapytanie o artykuły
            var articlesQuery = _context.Articles.Include(a => a.Category).AsQueryable();

            // Jeśli wybrano kategorię, filtrujemy wyniki
            if (categoryId.HasValue)
            {
                articlesQuery = articlesQuery.Where(a => a.CategoryId == categoryId);
            }

            Articles = await articlesQuery.ToListAsync();
        }
    }
}
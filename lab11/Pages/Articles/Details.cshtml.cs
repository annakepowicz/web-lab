using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab11.Models;

namespace lab11.Pages.Articles
{
    public class DetailsModel : PageModel
    {
        private readonly ShopDbContext _context;

        public DetailsModel(ShopDbContext context)
        {
            _context = context;
        }

        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Pobieramy artykuł wraz z kategorią (Eager Loading)
            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article == null)
            {
                return NotFound();
            }
            
            Article = article;
            return Page();
        }
    }
}
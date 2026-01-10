using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab11.Models; // Upewnij się, że namespace pasuje do lab11

namespace lab11.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ShopDbContext _context;

        public IndexModel(ShopDbContext context)
        {
            _context = context;
        }

        // Lista kategorii, którą wyświetlimy w HTML
        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            // Pobieramy dane z bazy dokładnie jak w kontrolerze
            Category = await _context.Categories.ToListAsync();
        }
    }
}
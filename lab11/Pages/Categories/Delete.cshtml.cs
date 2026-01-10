using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab11.Models;

namespace lab11.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ShopDbContext _context;

        public DeleteModel(ShopDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            // Pobieramy kategorię do wyświetlenia użytkownikowi
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (category == null) return NotFound();
            
            Category = category;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            // Szukamy kategorii i usuwamy ją z bazy
            var category = await _context.Categories.FindAsync(id);

            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
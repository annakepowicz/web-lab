using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab11.Models;
using System.IO;

namespace lab11.Pages.Articles
{
    public class DeleteModel : PageModel
    {
        private readonly ShopDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DeleteModel(ShopDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        // GET: Pobiera dane do wyświetlenia przed usunięciem
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article == null) return NotFound();

            Article = article;
            return Page();
        }

        // POST: Wykonuje usuwanie po potwierdzeniu
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var article = await _context.Articles.FindAsync(id);

            if (article != null)
            {
                // Logika usuwania pliku zdjęcia z dysku
                if (!string.IsNullOrEmpty(article.ImageName))
                {
                    string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "upload");
                    string filePath = Path.Combine(uploadFolder, article.ImageName);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using lab11.Models;
using System.IO;

namespace lab11.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly ShopDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CreateModel(ShopDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // BindProperty pozwala na automatyczne powiązanie danych z formularza
        [BindProperty]
        public Article Article { get; set; } = default!;

        public IActionResult OnGet()
        {
            // Przygotowanie listy kategorii do dropdowna
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", Article.CategoryId);
                return Page();
            }

            // Logika przesyłania pliku przeniesiona z ArticleController
            if (Article.FormFile != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "upload");
                
                // Tworzenie unikalnej nazwy pliku
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Article.FormFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Kopiowanie pliku na serwer
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Article.FormFile.CopyToAsync(fileStream);
                }

                // Zapisujemy nazwę pliku w modelu
                Article.ImageName = uniqueFileName;
            }

            _context.Articles.Add(Article);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
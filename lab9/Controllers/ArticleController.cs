using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab9.Models;
using Microsoft.AspNetCore.Hosting; 
using System.IO; 
using System;
using System.Linq;
using System.Threading.Tasks;

namespace lab9.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticleController(ShopDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Article
        public async Task<IActionResult> Index()
        {
            // Pobieramy artykuły razem z ich kategorią (Eager Loading)
            var articles = _context.Articles.Include(a => a.Category);
            
            // Obliczamy sumę cen (jeśli potrzebne do widoku)
            ViewBag.TotalPrice = await articles.SumAsync(a => a.Price);

            return View(await articles.ToListAsync());
        }

        // GET: Article/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Article/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Article/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Dodajemy parametr IFormFile do przesyłania pliku [cite: 38]
        public async Task<IActionResult> Create([Bind("Id,Title,Price,ExpirationDate,CategoryId,FormFile")] Article article)
        {
            if (ModelState.IsValid)
            {
                // Obsługa przesyłania pliku
                if (article.FormFile != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "upload");
                    
                    // Tworzenie unikalnej nazwy pliku 
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + article.FormFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Kopiowanie pliku na serwer 
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await article.FormFile.CopyToAsync(fileStream);
                    }

                    // Zapisujemy nazwę pliku w bazie danych
                    article.ImageName = uniqueFileName;
                }

                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            // Jeśli walidacja nie przejdzie, musimy odnowić listę kategorii
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
            return View(article);
        }

        // GET: Article/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
            return View(article);
        }

        // POST: Article/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Price,ExpirationDate,CategoryId,ImageName")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
            return View(article);
        }

        // GET: Article/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                // Usuwanie pliku z dysku, jeśli istnieje 
                if (!string.IsNullOrEmpty(article.ImageName))
                {
                    string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "upload", article.ImageName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab11.Models;

namespace lab11.Pages.Articles
{
    public class IndexModel : PageModel
    {
        private readonly ShopDbContext _context;

        public IndexModel(ShopDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;
        public double TotalPrice { get; set; }

        public async Task OnGetAsync()
        {
            var articles = _context.Articles.Include(a => a.Category);
            
            TotalPrice = await articles.SumAsync(a => a.Price);
            Article = await articles.ToListAsync();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab9.Models;
using Microsoft.AspNetCore.Authorization;

namespace lab9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesApiController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public ArticlesApiController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: api/ArticlesApi?skip=0&take=6&categoryId=1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetArticles(
            [FromQuery] int skip = 0, 
            [FromQuery] int take = 6, 
            [FromQuery] int? categoryId = null)
        {
            var query = _context.Articles
                .Include(a => a.Category)
                .AsQueryable();

            // Filtrowanie po kategorii jeśli podano
            if (categoryId.HasValue)
            {
                query = query.Where(a => a.CategoryId == categoryId.Value);
            }

            var articles = await query
                .OrderBy(a => a.Id)
                .Skip(skip)
                .Take(take)
                .Select(a => new
                {
                    id = a.Id,
                    title = a.Title,
                    price = a.Price,
                    imageName = a.ImageName,
                    categoryName = a.Category != null ? a.Category.Name : ""
                })
                .ToListAsync();

            return Ok(articles);
        }

        // Punkt 9: Ograniczenie dostępu tylko dla Admina (Authorize)
        // POST: api/ArticlesApi
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetArticles), new { id = article.Id }, article);
        }

        // PUT: api/ArticlesApi/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutArticle(int id, Article article)
        {
            if (id != article.Id) return BadRequest();

            _context.Entry(article).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!_context.Articles.Any(e => e.Id == id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        // DELETE: api/ArticlesApi/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return NotFound();

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
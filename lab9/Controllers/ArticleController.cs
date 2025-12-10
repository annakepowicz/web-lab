using Microsoft.AspNetCore.Mvc; 
using lab9.Services;
using lab9.Models;

namespace lab9.Controllers
{
    public class ArticleController : Controller
    {
        private IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            this._articleService = articleService;
        }

        public IActionResult Index()
        {
            var articles = _articleService.GetAllArticles();
            double sum = articles.Sum(a => a.Price);
            ViewBag.TotalPrice = sum;
            
            return View(articles);
        }

        public IActionResult Details(int id)
        {
            var article = _articleService.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }
        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                _articleService.AddArticle(article);
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }
        // GET: Edit
        public IActionResult Edit(int id) 
        {
            var article = _articleService.GetArticleById(id);
            if (article == null) return NotFound();
            return View(article);
        }

        [HttpPost]

        public IActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                _articleService.UpdateArticle(article);
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        public IActionResult Delete(int id)
        {
            _articleService.DeleteArticle(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
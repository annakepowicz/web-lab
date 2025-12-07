using System.Collections.Generic; 
using System.Linq; 
using lab9.Models;

namespace lab9.Services
{
    public interface IArticleService
    {
        IEnumerable<Article> GetAllArticles();
        Article? GetArticleById(int id);
        void AddArticle(Article article);
        void UpdateArticle(Article article);
        void DeleteArticle(int id);
    }

    public class ArticleServiceList : IArticleService
    {
        private List<Article> articles = new List<Article>();

        public IEnumerable<Article> GetAllArticles()
        {
            return articles;
        }

        public Article? GetArticleById(int id)
        {
            return articles.FirstOrDefault(a => a.Id == id);
        }

        public void AddArticle(Article article)
        {
            // auto-increment ID
            article.Id = articles.Any() ? articles.Max(a => a.Id) + 1 : 1;
            articles.Add(article);
        }

        public void UpdateArticle(Article article)
        {
            var existingArticle = GetArticleById(article.Id);
            if (existingArticle != null)
            {
                existingArticle.Title = article.Title;
                existingArticle.Price = article.Price;
                existingArticle.ExpirationDate = article.ExpirationDate; 
                existingArticle.Category = article.Category;
            }
        }

        public void DeleteArticle(int id)
        {
            var article = GetArticleById(id);
            if (article != null)
            {
                articles.Remove(article);
            }
        }
    }

    public class ArticleServiceDictionary : IArticleService
    {
        private Dictionary<int, Article> articles = new Dictionary<int, Article>();

        public IEnumerable<Article> GetAllArticles()
        {
            return articles.Values;
        }

        public Article? GetArticleById(int id)
        {
            articles.TryGetValue(id, out var article);
            return article;
        }

        public void AddArticle(Article article)
        {
            int newId = articles.Count > 0 ? articles.Keys.Max() + 1 : 1;
            article.Id = newId;
            articles[newId] = article;
        }

        public void UpdateArticle(Article article)
        {
            if (articles.ContainsKey(article.Id))
            {
                articles[article.Id] = article;
            }
        }

        public void DeleteArticle(int id)
        {
            articles.Remove(id);
        }
    }
}
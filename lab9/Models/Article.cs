using System;
namespace lab9.Models
{
    public enum Category
    {
        Electronics,
        Clothing,
        Home,
        Books,
        Other
    }

    public class Article
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public double Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Category Category { get; set; }
    }
}
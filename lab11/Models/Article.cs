using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace lab11.Models
{

    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Title must be at least 2 characters long.")]
        public string? Title { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryId{ get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public string? ImageName { get; set; }

        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile? FormFile { get; set; }
    }
}
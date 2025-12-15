using System.ComponentModel.DataAnnotations;

namespace lab9.Models
{

    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        public ICollection<Article>? Articles { get; set; }
    }
}
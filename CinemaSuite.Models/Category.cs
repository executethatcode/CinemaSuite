using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CinemaSuite.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category Name is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Category Name must be between 3 and 30 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Category Name must contain only letters.")]
        [DisplayName("Category Name")]
        public required string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}

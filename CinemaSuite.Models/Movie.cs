using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaSuite.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(3000)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Release Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; } = DateTime.Now;


        [Required(ErrorMessage = "Duration is required.")]
        [Range(0, 300, ErrorMessage = "Duration must be between 0 and 300 minutes.")]
        [Display(Name = "Duration (minutes)")]
        public int Duration { get; set; }

        [StringLength(100, ErrorMessage = "Director cannot be longer than 100 characters.")]
        public string? Director { get; set; }

        [Required(ErrorMessage = "DVD Price is required.")]
        [Range(0, 1000, ErrorMessage = "Price must be between 0 and 1000.")]
        [Display(Name = "DVD Price")]
        public decimal DvdPrice { get; set; }

        [Required(ErrorMessage = "Blu-ray Price is required.")]
        [Range(0, 1000, ErrorMessage = "Price must be between 0 and 1000.")]
        [Display(Name = "Blu-ray Price")]
        public decimal BlurayPrice { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]

        [ValidateNever]
        public string? ImageUrl { get; set; }
    }
}

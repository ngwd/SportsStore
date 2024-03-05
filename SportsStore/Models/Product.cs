using System.ComponentModel.DataAnnotations;
namespace SportsStore.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        public decimal Price { get; set; } 
        public string? Category { get; set; }
    }
}

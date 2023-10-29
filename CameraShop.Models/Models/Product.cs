using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraShop.Models.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(0, 100000)]
        public double ListPrice { get; set; }
        [Required]
        [Range(0, 100000)]
        public double Price { get; set; }
        [Required]
        [Range(0, 100000)]
        public double Price500 { get; set; }
        [Required]
        [Range(0, 100000)]
        public double Price10000 { get; set; }
        [MaxLength(500)]
        public string ImgURL { get; set; } = string.Empty;
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

    }
}

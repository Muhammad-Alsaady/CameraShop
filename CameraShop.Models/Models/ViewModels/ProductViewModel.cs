using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CameraShop.Models.Models.ViewModels
{
    public class ProductViewModel
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
        public IFormFile ImgURL { get; set; } 
        [Required]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraShop.Models.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;

        //public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}

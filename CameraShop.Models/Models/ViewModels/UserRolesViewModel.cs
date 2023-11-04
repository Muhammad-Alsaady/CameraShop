using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraShop.Models.Models.ViewModels
{
    public class UserRolesViewModel
    {
        [MaxLength(100)]
        public string UserId { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(250)]
        public string Email { get; set; }
        [Phone, MaxLength(20)]
        public string PhoneNumber { get; set; }
        public int? CompanyId { get; set; }
        public IEnumerable<string> Roles { get; set; } = new HashSet<string>();
    }
}

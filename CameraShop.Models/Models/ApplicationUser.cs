using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CameraShop.Models.Models
{
    public class ApplicationUser: IdentityUser
    {
        [MaxLength(50)]
        public string _Name { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(20)]
        public string PostalCode { get; set; }
        [NotMapped]
        public string Role { get; set; }  // this field will not be added to the DB
        public int? CompanyId { get; set; } // can be empty as not all users are in Company, some of them are individuals
        [JsonIgnore]
        public virtual Company Company { get; set; }
    }
}

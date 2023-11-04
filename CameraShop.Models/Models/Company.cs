using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraShop.Models.Models
{
    public class Company
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(20)]
        public string PostalCode { get; set; }
        [MaxLength(20)]
        [Phone]
        public string PhoneNumber { get; set; }
        public bool isAuthorizedCompany { get; set; }

    }
}

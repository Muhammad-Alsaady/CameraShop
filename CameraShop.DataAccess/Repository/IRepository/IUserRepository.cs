using CameraShop.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CameraShop.DataAccess.Repository.IRepository
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        IEnumerable<ApplicationUser> GetUserRole();
        Task Update(ApplicationUser userModel);
    }
}

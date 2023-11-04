using CameraShop.DataAccess;
using CameraShop.DataAccess.Repository;
using CameraShop.DataAccess.Repository.IRepository;
using CameraShop.Models.Models;
using CameraShop.Models.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace CameraShop.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public UserController(IUnitOfWork unitOfWork,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.context = context;
        }
        public IActionResult Index()
        {
            #region Using View model In case of that the user has more than one Role
            //var userList = userManager.Users.Include(c => c.Company).ToList();
            //List<UserRolesViewModel> userRoles = new List<UserRolesViewModel>();

            //foreach (var user in userManager.Users.ToList())
            //{
            //    var userRoleVM = new UserRolesViewModel
            //    {
            //        UserId = user.Id,
            //        Name = user._Name,
            //        Roles = await userManager.GetRolesAsync(user),
            //        Email = user.Email,
            //        PhoneNumber = user.PhoneNumber,
            //    };
            //    if (user.CompanyId is null)
            //        user.Company = new() { Name = "NA" };
            //    userRoles.Add(userRoleVM);
            //}
            //return View(userRoles); 
            #endregion

            var userList = unitOfWork.AppUser.GetUserRole();
            
            return View(userList);
        }

        #region API Calls
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    try
        //    {
        //        /// This is one way to retrieve User's role (We will never use it because we are directly accessing ApplicationDbContext)
        //        //var userList = userManager.Users.ToList();
        //        //var userRoles = context.UserRoles.ToList();
        //        //var roleList = roleManager.Roles.ToList();

        //        /// Instead we are going to use View Model
        //        var userList = unitOfWork.AppUser.GetUserRole();
        //        return Json(userList);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception for debugging purposes
        //        // You can also return a more specific error message
        //        return StatusCode(500, $"Internal Server Error: {ex.Message}");
        //    }
        //}
        //[HttpDelete]
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //        return BadRequest();
        //    var model = unitOfWork.AppUser.Get(id.GetValueOrDefault());
        //    if (model == null)
        //        return Json(new { success = false, message = "Somthing went wrong" });
        //    unitOfWork.AppUser.Remove(id.GetValueOrDefault());
        //    await unitOfWork.Save();
        //    return Json(new { success = true, message = "Successfuly Deleted" });
        //}

        [HttpPost]
        public async Task<IActionResult> LockUnlock([FromBody] string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return Json(new { success = false, Message = "Something went wrong ..." });
            if(user.LockoutEnd is not null && user.LockoutEnd > DateTime.Now)
            {
                /// User is Locked
                user.LockoutEnd = DateTime.Now;
            }
            else user.LockoutEnd = DateTime.Now.AddYears(2);
            await unitOfWork.Save();
            return Json(new { success = true, Message = "Successful" });
        }
        #endregion
    }

}

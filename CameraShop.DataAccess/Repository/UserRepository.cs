using CameraShop.DataAccess.Repository.IRepository;
using CameraShop.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CameraShop.DataAccess.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context): base(context)
        {
            this.context = context;
        }

        public IEnumerable<ApplicationUser> GetUserRole()
        {
            IEnumerable<ApplicationUser> userList = context.Users.Include(c => c.Company).ToList();
            var roleList = context.Roles.ToList();
            var userRole = context.UserRoles.ToList();
            foreach (var user in userList)
            {
                var roleId = userRole.FirstOrDefault(r => r.UserId == user.Id).RoleId;
                user.Role = roleList.FirstOrDefault(r => r.Id == roleId).Name;
                if (user.CompanyId == null)
                    user.Company = new Company { Name = "Not Assigned" };
            }
            return userList;
        }

        public async Task Update(ApplicationUser userModel)
        {
            context.Update(userModel);
            await context.SaveChangesAsync();
        }
    }
}

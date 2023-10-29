using CameraShop.DataAccess.Repository.IRepository;
using CameraShop.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CameraShop.DataAccess.Repository
{
    public class CategoryRepository: Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context): base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllList()
        {
            return await context.Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).OrderBy(c => c.Text)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task Update(Category category)
        {
            var categoryToUpdate = await context.Categories.FindAsync(category.Id);
            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = category.Name;
                await context.SaveChangesAsync();
            }
        }
    }
}

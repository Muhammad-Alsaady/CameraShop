using CameraShop.DataAccess.Repository.IRepository;
using CameraShop.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CameraShop.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext context;

        public CompanyRepository(ApplicationDbContext context): base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllList()
        {
            return await context.Companies.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).OrderBy(c => c.Text)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task Update(Company company)
        {
           context.Update(company);
           await context.SaveChangesAsync();
        }

    }
}

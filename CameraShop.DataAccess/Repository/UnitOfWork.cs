using CameraShop.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraShop.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Categories = new CategoryRepository(context);
            Products = new ProductRepository(context);
            Companies = new CompanyRepository(context);
            AppUser = new UserRepository(context);
        }

        public ICategoryRepository Categories { get; private set; }
        public ICompanyRepository Companies { get; private set; }
        public IProductRepository Products { get; private set; }
        public IUserRepository AppUser { get; private set; }


        public void Dispose()
        {
            context.Dispose();
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}

using CameraShop.DataAccess.Repository.IRepository;
using CameraShop.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraShop.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext context): base(context)
        {
            this.context = context;
        }

        public async Task Update(Product product)
        {
            var productToUpdate = await context.Products.FindAsync(product.Id);
            
            if (productToUpdate != null)
            {
                if (product.ImgURL != null)
                    productToUpdate.ImgURL = product.ImgURL;
                productToUpdate.Name = product.Name;
                productToUpdate.Description = product.Description;
                productToUpdate.CategoryId = product.CategoryId;
                productToUpdate.Price = product.Price;
                productToUpdate.Price500 = product.Price500;
                productToUpdate.Price10000 = product.Price10000;
                productToUpdate.ListPrice = product.ListPrice;
                await context.SaveChangesAsync();
            }
        }
    }
}

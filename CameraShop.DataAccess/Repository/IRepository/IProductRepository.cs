using CameraShop.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraShop.DataAccess.Repository.IRepository
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task Update(Product product);
    }
}

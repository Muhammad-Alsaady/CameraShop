using CameraShop.DataAccess.Repository;
using CameraShop.DataAccess.Repository.IRepository;
using CameraShop.Models.Models;
using CameraShop.Models.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
namespace CameraShop.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment env;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            this.unitOfWork = unitOfWork;
            this.env = env;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            ProductViewModel prdModel = new ProductViewModel()
            {
                CategoryList = await unitOfWork.Categories.GetAllList()
            };
            if (id == null)
            {
                return View(prdModel);
            }
            else
            {
                var objFromDB = await unitOfWork.Products.Get(id.GetValueOrDefault());
                if (objFromDB == null)
                {
                    return NotFound();
                }
                prdModel.Name = objFromDB.Name;
                prdModel.Description = objFromDB.Description;
                prdModel.Price500 = objFromDB.Price500;
                prdModel.ListPrice = objFromDB.ListPrice;
                prdModel.Price10000 = objFromDB.Price10000;
                prdModel.ListPrice = objFromDB.ListPrice;
                prdModel.CategoryId = objFromDB.CategoryId;
                return View(prdModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImgURL.FileName)}";

                if (model.ImgURL != null)
                {
                    if (model.Id != 0)
                    {
                        var prdToUpdate = await unitOfWork.Products.Get(model.Id);
                        var OldImagePath = Path.Combine("wwwroot/images/products/", prdToUpdate.ImgURL);
                        if (System.IO.File.Exists(OldImagePath))
                            System.IO.File.Delete(OldImagePath);
                    }
                    var FilePath = Path.Combine("wwwroot/images/products/", fileName); // ["~/images/products"]    
                    using var stream = System.IO.File.Create(FilePath);
                    await model.ImgURL.CopyToAsync(stream);
                }
                if (model.Id == 0)
                {
                    var prd = new Product()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        CategoryId = model.CategoryId,
                        Price = model.Price,
                        Price10000 = model.Price10000,
                        Price500 = model.Price500,
                        ListPrice = model.ListPrice,
                        ImgURL = fileName,
                    };
                    unitOfWork.Products.Add(prd);
                }
                else
                {
                    var prdToUpdate = await unitOfWork.Products.Get(model.Id);
                    if (prdToUpdate == null) return NotFound();
                    await unitOfWork.Products.Update(prdToUpdate);
                }
                await unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var items = unitOfWork.Products.GetAll(IncludeProperties: "Category");
                return Json(new { data = items });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var item = await unitOfWork.Products.Get(id.GetValueOrDefault());
            if(item == null)
                return Json(new { success = false, message = "Somthing went wrong" });
            var FilePath = Path.Combine("wwwroot/images/products/", item.ImgURL); // ["~/images/products"]
            if (System.IO.File.Exists(FilePath))
                System.IO.File.Delete(FilePath);
            unitOfWork.Products.Remove(item);
            await unitOfWork.Save();
            return Json(new { success = true, message = "Successfuly Deleted" });
        }
        #endregion
    }

}

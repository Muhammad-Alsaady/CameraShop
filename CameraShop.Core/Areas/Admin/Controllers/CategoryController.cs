using CameraShop.DataAccess.Repository;
using CameraShop.DataAccess.Repository.IRepository;
using CameraShop.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace CameraShop.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
           if(id == null)
            {
                Category category = new Category();
                return View(category);
            }
           var item = await unitOfWork.Categories.Get(id.GetValueOrDefault());
            if(item == null)
            {
                return NotFound(item);
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category model)
        {
            if(ModelState.IsValid)
            {
                if(model.Id == 0)
                    unitOfWork.Categories.Add(model);
                else
                    await unitOfWork.Categories.Update(model);
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
                var items = unitOfWork.Categories.GetAll();
                return Json(new { data = items });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // You can also return a more specific error message
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var model = unitOfWork.Categories.Get(id.GetValueOrDefault());
            if (model == null)
                return Json(new {success = false, message = "Somthing went wrong"});
            unitOfWork.Categories.Remove(id.GetValueOrDefault());
            await unitOfWork.Save();
            return Json(new { success = true, message = "Successfuly Deleted"});
        }
        #endregion
    }

}

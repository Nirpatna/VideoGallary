using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Video_Gallary.Areas.Admin.Models;
using Video_Gallary.Areas.Admin.Models.ViewModels;
using Video_Gallary.Respository.Contract;

namespace Video_Gallary.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategory categoryService;
        private readonly IHostingEnvironment environment;

        public CategoryController(ICategory category, IHostingEnvironment _environment)
        {
            this.categoryService = category;
            environment = _environment;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel model)
        {
            var path = environment.WebRootPath;
            var filePath = "contents/images/" + model.Image.FileName;
            var fullPath = Path.Combine(path, filePath);
            categoryService.UploadFile(model.Image, fullPath);
            var category = new Category()
            {
                Name=model.Name,
                IsActive=model.IsActive,
                Created_On=DateTime.Now,
                Image=filePath
            };
            categoryService.CreateCategory(category);
            return RedirectToAction("ShowCategory");
        }
        public IActionResult ShowCategory()
        {
            var cats = categoryService.GetCategories();
            return View(cats);
        }

        public IActionResult Delete(int id)
        {
           var result= categoryService.DeleteCategory(id);
            if (result)
            {
                return RedirectToAction("ShowCategory");
            }
            else
            {
                ViewBag.message = "category not found !";
                return   RedirectToAction("ShowCategory");
            }
        }

        public IActionResult Edit(int id)
        {
            var cats = categoryService.GetCategoryById(id);
            var cat2 = new CategoryViewModel()
            {
                Id=cats.Id,
                OldImage=cats.Image,
                IsActive=cats.IsActive,
                Name=cats.Name
            };

            return View(cat2);
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel model)
        {

            var cat = categoryService.GetCategoryById(model.Id);

            var path = environment.WebRootPath;
            var filePath = "contents/images/" + model.Image.FileName;
            var fullPath = Path.Combine(path, filePath);
            categoryService.UploadFile(model.Image, fullPath);
            if (cat != null)
            {
                //update
                cat.Name = model.Name;
                cat.IsActive = model.IsActive;
                cat.Updated_On = DateTime.Now;
                cat.Image = filePath;

                categoryService.UpdateCategory(cat);
                return RedirectToAction("ShowCategory");

            }
            else
            {
                //do not update
                ViewBag.message = "category not found! ";
                return View();
            }
            return View();
        }

    }
}

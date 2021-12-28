using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Video_Gallary.Areas.Admin.Models;
using Video_Gallary.Models;
using Video_Gallary.Respository.Contract;
using System.IO;
namespace Video_Gallary.Respository
{
    public class CategoryService : ICategory
    {
        private readonly ApplicationDbContext context;

        public CategoryService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Category CreateCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            return category;
        }

        public bool DeleteCategory(int id)
        {
            var cat = context.Categories.SingleOrDefault(e => e.Id == id);
            if (cat != null)
            {
                cat.IsActive = false;
                context.Categories.Update(cat);
                context.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }
        }

        public List<Category> GetCategories()
        {
            var cats = context.Categories.Where(e => e.IsActive == true).ToList();
            return cats;

        }

        public Category GetCategoryById(int id)
        {
            var cats = context.Categories.SingleOrDefault(e => e.IsActive == true && e.Id==id);
            return cats;
        }

        public Category UpdateCategory(Category category)
        {
            context.Categories.Update(category);
            context.SaveChanges();
            return category;
        }
        public void UploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
            
        }
    }
}

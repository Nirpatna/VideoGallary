using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Video_Gallary.Areas.Admin.Models.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string OldImage { get; set; }
       public bool IsActive { get; set; }
    }
}

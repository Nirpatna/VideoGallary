using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Video_Gallary.Areas.Admin.Models;

namespace Video_Gallary.Models
{
    public class ApplicationDbContext:IdentityDbContext<IdentityCustomeUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }

    }
}

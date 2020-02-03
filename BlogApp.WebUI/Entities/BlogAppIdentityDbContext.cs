using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.WebUI.Entities
{
    public class BlogAppIdentityDbContext: IdentityDbContext<CustomIdentityUser, CustomIdentityRole, string>
    {
        public BlogAppIdentityDbContext(DbContextOptions<BlogAppIdentityDbContext> options) : base(options)
        {

        }
    }
}

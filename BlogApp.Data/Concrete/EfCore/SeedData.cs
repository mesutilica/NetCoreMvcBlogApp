using BlogApp.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Data.Concrete.EfCore
{
   /*public static class ModelBuilderExtensions
   {
      public static void Seed(this ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<Category>().HasData(
             new Category() { Name = "William" }
         );
         modelBuilder.Entity<Blog>().HasData(
               new Blog() { Title = "Kategori 1 Blog 1", CategoryId = 1, Date = DateTime.Now, isApproved = true, Description = "", Image = "", Body = "" },
               new Blog() { Title = "Kategori 2 Blog 1", CategoryId = 2, Date = DateTime.Now, isApproved = true, Description = "", Image = "", Body = "" },
               new Blog() { Title = "Kategori 3 Blog 1", CategoryId = 3, Date = DateTime.Now, isApproved = true, Description = "", Image = "", Body = "" }
         );
      }
   }
    */
   public static class SeedData
   {
      public static void Seed(IApplicationBuilder app)
      {
         BlogContext context = app.ApplicationServices.GetRequiredService<BlogContext>();
         context.Database.Migrate();
         if (context.Categories.Any())
         {
            context.Categories.AddRange(
               new Category() { Name = "Kategori 1" },
               new Category() { Name = "Kategori 2" },
               new Category() { Name = "Kategori 3" }
               );
            var sonuc = context.SaveChanges();
         }
         if (context.Blogs.Any())
         {
            context.Blogs.AddRange(
               new Blog() { Title = "Kategori 1 Blog 1", CategoryId = 1, Date = DateTime.Now, isApproved = true, Description = "", Image = "" },
               new Blog() { Title = "Kategori 2 Blog 1", CategoryId = 2, Date = DateTime.Now, isApproved = true, Description = "", Image = "" },
               new Blog() { Title = "Kategori 3 Blog 1", CategoryId = 3, Date = DateTime.Now, isApproved = true, Description = "", Image = "" }
               );
            var sonuc = context.SaveChanges();
         }
      }
   }

}

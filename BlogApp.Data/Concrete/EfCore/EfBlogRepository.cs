using BlogApp.Data.Abstract;
using BlogApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Data.Concrete.EfCore
{
   public class EfBlogRepository : IBlogRepository
   {
      private BlogContext context;
      public EfBlogRepository(BlogContext _context)
      {
         context = _context;
      }
      public void Add(Blog entity)
      {
         context.Blogs.Add(entity);
         context.SaveChanges();
      }

      public void Delete(int blogId)
      {
         var blog = context.Blogs.Find(blogId);
         if (blog != null)
         {
            context.Blogs.Remove(blog);
            context.SaveChanges();
         }
      }

      public IQueryable<Blog> GetAll()
      {
         return context.Blogs;
      }

      public Blog GetById(int blogId)
      {
         return context.Blogs.FirstOrDefault(c => c.BlogId == blogId);
      }

      public void Update(Blog entity)
      {
         context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
         context.SaveChanges();
      }
   }
}

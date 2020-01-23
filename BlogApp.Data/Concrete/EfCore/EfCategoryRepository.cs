﻿using BlogApp.Data.Abstract;
using BlogApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Data.Concrete.EfCore
{
   public class EfCategoryRepository : ICategoryRepository
   {
      private BlogContext context;
      public EfCategoryRepository(BlogContext _context)
      {
         context = _context;
      }
      public void AddCategory(Category entity)
      {
         context.Categories.Add(entity);
         context.SaveChanges();
      }

      public void DeleteCategory(int categoryId)
      {
         var category = context.Categories.Find(categoryId);
         if (category!= null)
         {
            context.Categories.Remove(category);
            context.SaveChanges();
         }
      }

      public IQueryable<Category> GetAll()
      {
         return context.Categories;
      }

      public Category GetById(int categoryId)
      {
         return context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
      }

      public void UpdateCategory(Category entity)
      {
         context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
         context.SaveChanges();
      }
   }
}